using ElectronicTextbook.Core.Interfaces;
using ElectronicTextbook.Core.Models;
using ElectronicTextbook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElectronicTextbook.Web.Controllers
{
    public class LecturesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LecturesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Lectures
        public async Task<IActionResult> Index(string searchTerm = null)
        {
            try
            {
                IEnumerable<Lecture> lectures;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    lectures = await _unitOfWork.Lectures.GetAllAsync();
                }
                else
                {
                    lectures = await _unitOfWork.Lectures.SearchAsync(searchTerm);
                }
                var lectureViewModels = lectures.Select(MapToViewModel).ToList();
                return View(lectureViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        // GET: Lectures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var lecture = await _unitOfWork.Lectures.GetByIdAsync(id.Value);

                if (lecture == null)
                {
                    return NotFound();
                }
                return View(MapToViewModel(lecture));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Lectures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lectures/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LectureViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] pdfData = null;
                    if (model.PdfFileForm != null && model.PdfFileForm.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.PdfFileForm.CopyToAsync(memoryStream);
                            pdfData = memoryStream.ToArray();
                        }
                    }
                    var userId = User.FindFirstValue("UserId");
                    if (string.IsNullOrEmpty(userId))
                    {
                        return BadRequest("User id not found");
                    }

                    var lecture = new Lecture
                    {
                        Title = model.Title,
                        Description = model.Description,
                        PdfFile = pdfData,
                        DateAdded = DateTime.Now,
                        AuthorId = int.Parse(userId)

                    };

                    await _unitOfWork.Lectures.AddAsync(lecture);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error during lecture creation:" + ex.Message);
                    return View(model);
                }

            }
            return View(model);
        }

        // GET: Lectures/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var lecture = await _unitOfWork.Lectures.GetByIdAsync(id.Value);
                if (lecture == null)
                {
                    return NotFound();
                }
                return View(MapToViewModel(lecture));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        // POST: Lectures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, LectureViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] pdfData = null;
                    if (model.PdfFileForm != null && model.PdfFileForm.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.PdfFileForm.CopyToAsync(memoryStream);
                            pdfData = memoryStream.ToArray();
                        }
                    }
                    var lecture = new Lecture
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description,
                        PdfFile = pdfData,
                        DateAdded = DateTime.Now,
                        AuthorId = model.AuthorId
                    };
                    await _unitOfWork.Lectures.UpdateAsync(lecture);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error during lecture edit:" + ex.Message);
                    return View(model);
                }

            }
            return View(model);
        }

        // GET: Lectures/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var lecture = await _unitOfWork.Lectures.GetByIdAsync(id.Value);
                if (lecture == null)
                {
                    return NotFound();
                }
                return View(MapToViewModel(lecture));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.Lectures.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        private LectureViewModel MapToViewModel(Lecture lecture)
        {
            string base64Pdf = lecture.PdfFile != null ? Convert.ToBase64String(lecture.PdfFile) : null;
            return new LectureViewModel
            {
                Id = lecture.Id,
                Title = lecture.Title,
                Description = lecture.Description,
                PdfFile = base64Pdf,
                DateAdded = lecture.DateAdded,
                AuthorId = lecture.AuthorId,
                AuthorName = lecture.Author?.FirstName + " " + lecture.Author?.LastName
            };
        }
    }
}