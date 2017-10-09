using EF6CodeFirstApporach.Dal;
using System.Linq;
using System.Web.Mvc;
using EF6CodeFirstApporach.ViewModel;

namespace EF6CodeFirstApporach.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db;

        public HomeController()
        {
            db = new SchoolContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //  ViewBag.Message = "Your application description page.";
            IQueryable<EnrollmentDateGroup> enrolGrps = from student in db.Students group student by student.EnrollmentDate into dateGroup select new EnrollmentDateGroup() { EnrollmentDate = dateGroup.Key,StudentCount = dateGroup.Count() };
            return View(enrolGrps.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}