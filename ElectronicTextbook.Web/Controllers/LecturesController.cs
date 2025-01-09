using ElectronicTextbook.Core.Interfaces;
using ElectronicTextbook.Core.Models;
using ElectronicTextbook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            IEnumerable<Lecture> lectures;
            if (string.IsNullOrEmpty(searchTerm))
            {
                lectures = await _unitOfWork.Lectures.GetAllAsync();
            }
            else
            {
                lectures = await _unitOfWork.Lectures.SearchAsync(searchTerm);
            }
            var lectureViewModels = new List<LectureViewModel>();
            foreach (var lecture in lectures)
            {
                string base64Pdf = lecture.PdfFile != null ? Convert.ToBase64String(lecture.PdfFile) : null;
                lectureViewModels.Add(new LectureViewModel
                {
                    Id = lecture.Id,
                    Title = lecture.Title,
                    Description = lecture.Description,
                    PdfFile = base64Pdf,
                    DateAdded = lecture.DateAdded,
                    AuthorId = lecture.AuthorId,
                    AuthorName = lecture.Author?.FirstName + " " + lecture.Author?.LastName
                });
            }
            return View(lectureViewModels);

        }

        // GET: Lectures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = await _unitOfWork.Lectures.GetByIdAsync(id.Value);

            if (lecture == null)
            {
                return NotFound();
            }
            string base64Pdf = lecture.PdfFile != null ? Convert.ToBase64String(lecture.PdfFile) : null;
            var lectureViewModel = new LectureViewModel
            {
                Id = lecture.Id,
                Title = lecture.Title,
                Description = lecture.Description,
                PdfFile = base64Pdf,
                DateAdded = lecture.DateAdded,
                AuthorId = lecture.AuthorId,
                AuthorName = lecture.Author?.FirstName + " " + lecture.Author?.LastName
            };
            return View(lectureViewModel);
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
                    Title = model.Title,
                    Description = model.Description,
                    PdfFile = pdfData,
                    DateAdded = DateTime.Now,
                    AuthorId = int.Parse(User.FindFirst("UserId").Value)
                };
                await _unitOfWork.Lectures.AddAsync(lecture);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
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
            var lecture = await _unitOfWork.Lectures.GetByIdAsync(id.Value);
            if (lecture == null)
            {
                return NotFound();
            }
            string base64Pdf = lecture.PdfFile != null ? Convert.ToBase64String(lecture.PdfFile) : null;
            var lectureViewModel = new LectureViewModel
            {
                Id = lecture.Id,
                Title = lecture.Title,
                Description = lecture.Description,
                PdfFile = base64Pdf,
                DateAdded = lecture.DateAdded,
                AuthorId = lecture.AuthorId
            };
            return View(lectureViewModel);
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

            var lecture = await _unitOfWork.Lectures.GetByIdAsync(id.Value);

            if (lecture == null)
            {
                return NotFound();
            }
            string base64Pdf = lecture.PdfFile != null ? Convert.ToBase64String(lecture.PdfFile) : null;
            var lectureViewModel = new LectureViewModel
            {
                Id = lecture.Id,
                Title = lecture.Title,
                Description = lecture.Description,
                PdfFile = base64Pdf,
                DateAdded = lecture.DateAdded,
                AuthorId = lecture.AuthorId,
                AuthorName = lecture.Author?.FirstName + " " + lecture.Author?.LastName
            };
            return View(lectureViewModel);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.Lectures.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}