using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicTextbook.Core.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; } // Should be string to match User.Id
        public User Author { get; set; }
    }

}