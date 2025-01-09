using System;

namespace ElectronicTextbook.Core.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] PdfFile { get; set; }
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
