using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolDbWeb.Models
{
    public class ClassStream
    {
        public int ClassStreamId { get; set; }
        [Required]
        public string ClassName { get; set; }

        // Navigation property
        public ICollection<Student> Students { get; set; }
    }
}
