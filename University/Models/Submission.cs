using System;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Submission
    {
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SubmittedAt { get; set; }

        public int CourseworkId { get; set; }
        public Coursework Coursework { get; set; }
    }
}
