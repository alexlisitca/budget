using Budget.Web.Mvc.Models.UserVm;
using System.Web.Mvc;
using System.Web.Security;

namespace Budget.Web.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public AuthController() { }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            else
                return View(new ApplicationUserVm());
        }

        // GET: Home
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(ApplicationUserVm model)
        {
            if ((model.Email == "test@gmail.com") && (model.Password == "123"))
            {
                FormsAuthentication.SetAuthCookie("test@gmail.com", true);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Auth/Index");
        }
    }
}