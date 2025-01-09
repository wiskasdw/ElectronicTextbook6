using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicTextbook.Web.ViewModels
{
    public class LectureViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter lecture title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfFile { get; set; } // Base64 string
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public IFormFile PdfFileForm { get; set; }
        public string AuthorName { get; set; }

    }
}