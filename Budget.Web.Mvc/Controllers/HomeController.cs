using Budget.Web.Mvc.Models.CategoryVm;
using Budget.Web.Mvc.Models.FinancialVm;
using Budget.Web.Mvc.Models.ScheduleVm;
using Budget.Web.Mvc.Models.ScoreVm;
using Budget.Web.Mvc.Models.UserVm;
using Budget.Web.Mvc.ViewModelProvider.Contract;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Budget.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScoreVmProvider _scoreProvider;
        private readonly ICategoryVmProvider _categProvider;
        private readonly IFinancialVmProvider _financProvider;
        private readonly IScheduleVmProvider _scheduleProvider;
        private readonly IBudgetProvider _budgetProvider;

        public HomeController
        (
            IScoreVmProvider scoreProvider, 
            ICategoryVmProvider categProvider, 
            IFinancialVmProvider financProvider, 
            IScheduleVmProvider scheduleProvider,
            IBudgetProvider budgetProvider
        )
        {
            _categProvider = categProvider;
            _scoreProvider = scoreProvider;
            _financProvider = financProvider;
            _scheduleProvider = scheduleProvider;
            _budgetProvider = budgetProvider;
        }

        #region Score Section
        [Authorize]
        [HttpGet]
        public ActionResult ScoreView(Guid? Id)
        {
            return View("~/Views/Home/Score/ScoreView.cshtml", _scoreProvider.GetViewModel(Id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateScore(ScoreListVm vm)
        {
            _scoreProvider.AddOrUpdateScore(vm.NewItem);
            return RedirectToAction("ScoreView");
        }

        [Authorize]
        public ActionResult DeleteScore(Guid Id)
        {
            _scoreProvider.RemoveScore(Id);
            return RedirectToAction("ScoreView");
        }
        #endregion

        #region Category Section
        [Authorize]
        [HttpGet]
        public ActionResult CategoryView(Guid? Id)
        {
            return View("~/Views/Home/Category/CategoryView.cshtml", _categProvider.GetViewModel(Id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateCategory(CategoryListVm vm)
        {
            _categProvider.AddOrUpdateCategory(vm.NewItem);
            vm = new CategoryListVm();
            return RedirectToAction("CategoryView");
        }

        [Authorize]
        public ActionResult DeleteCategory(Guid Id)
        {
            _categProvider.RemoveCategory(Id);
            return RedirectToAction("CategoryView");
        }
        #endregion

        #region Financial Section
        [Authorize]
        [HttpGet]
        public ActionResult FinancialView(Guid? Id)
        {
            return View("~/Views/Home/Financial/FinancialView.cshtml", _financProvider.GetViewModel(Id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateFinancial(FinancialListVm vm)
        {
            _financProvider.AddOrUpdateTransaction(vm.NewItem);
            vm = new FinancialListVm();
            return RedirectToAction("FinancialView");
        }

        [Authorize]
        public ActionResult DeleteFinancial(Guid Id)
        {
            _financProvider.RemoveTransaction(Id);
            return RedirectToAction("FinancialView");
        }

        #endregion

        #region Schedule Section
        [Authorize]
        [HttpGet]
        public ActionResult ScheduleView(Guid? Id)
        {
            return View("~/Views/Home/Schedule/ScheduleView.cshtml", _scheduleProvider.GetViewModel(Id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateSchedule(ScheduleListVm vm)
        {
            _scheduleProvider.AddOrUpdateSchedule(vm.NewItem);
            vm = new ScheduleListVm();
            return RedirectToAction("ScheduleView");
        }

        [Authorize]
        public ActionResult DeleteSchedule(Guid Id)
        {
            _scheduleProvider.RemoveSchedule(Id);
            return RedirectToAction("ScheduleView");
        }

        #endregion

        [Authorize]
        [HttpGet]
        public ActionResult Index(Guid? Id)
        {
            return View(_budgetProvider.GetViewModel());
        }
    }
}