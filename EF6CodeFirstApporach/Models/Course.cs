using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF6CodeFirstApporach.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; } //this property will not be generated  by db, we have to generate it manually
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}