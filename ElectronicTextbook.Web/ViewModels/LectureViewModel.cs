using Microsoft.AspNetCore.Http;
using System;

namespace ElectronicTextbook.Web.ViewModels
{
    public class LectureViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile PdfFileForm { get; set; } // Для загрузки файла
        public string PdfFile { get; set; } // Для отображения файла
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
