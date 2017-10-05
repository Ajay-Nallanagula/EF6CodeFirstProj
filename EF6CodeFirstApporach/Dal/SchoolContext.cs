using EF6CodeFirstApporach.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EF6CodeFirstApporach.Dal
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        //below two properties are optional to mention because Student class already have a reference to Enrollment, and Enrollment have areference to Course, The Entity Framework would include them implicitly
        public DbSet<Enrollment> Enrollments { get; set; } // Can be usefulf If you are using Seed method during development
        public DbSet<Course> Courses { get; set; } // Can be usefulf If you are using Seed method during development

        public SchoolContext() : base("SchoolConStr") //If Connection string name is not mentioned , Entity Framework assumes that the connection string name is the same as the class name in this case SchoolContext, optionally you can give the connection string with Databasename , EF will create the same database name
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //If you didn't do this, the generated tables in the database would be named Students, Courses, and Enrollments. Now after removing Pluralizing the name of Tables would be Student,Enrollmet,Cours
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Enrollment>().ToTable("Enrollments"); //This will Pluralize only Enrollments Table 
        }
    }
}

/*
        1) DbContext corresponds to your database (or a collection of tables and views in your database) whereas a DbSet corresponds to a table or view in your database.
*/
