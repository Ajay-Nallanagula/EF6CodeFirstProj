using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF6CodeFirstApporach.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string EmailAddress { get; set; }

        //Property is virtual because it helps in lazy loading
        public virtual ICollection<Enrollment> Enrollments { get; set; } //Navigation Property

    }
}