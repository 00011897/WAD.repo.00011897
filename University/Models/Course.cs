using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Coursework> Courseworks { get; set; }
    }
}
