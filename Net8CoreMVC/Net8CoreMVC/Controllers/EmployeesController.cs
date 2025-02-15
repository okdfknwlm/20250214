using Microsoft.AspNetCore.Mvc;
using Net8CoreMVC.Models;
using Net8CoreMVC.Services;

namespace Net8CoreMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CallApiService _Api;
        public EmployeesController(CallApiService callApiService)
        {
            _Api = callApiService;
        }

        public IActionResult Index() => View(_Api.GetEmpList());

        public IActionResult CreateEmp() => PartialView("_CreateEmp", new EmpModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmpPost(EmpModel model)
        {
            try
            {
                var res = _Api.CreateEmp(model);
                if (res.Result_Code == "0000")
                    TempData["AlertMsg"] = "新增成功";

                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult EditEmp(int EmpID)
        {
            var data = _Api.GetEmp(EmpID);
            return PartialView("_EditEmp", data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmpPost(int EmpID, EmpModel model)
        {
            try
            {
                var response = _Api.UpdateEmp(EmpID, model);
                if (response.Result_Code == "0000")
                    TempData["AlertMsg"] = "修改成功";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(model);
            }
        }

        public IActionResult DeleteEmp(int EmpID)
        {
            var data = _Api.GetEmp(EmpID);
            return PartialView("_DeleteEmp", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmpPost(int EmpID)
        {
            try
            {
                var response = _Api.DeleteEmp(EmpID);
                if (response.Result_Code == "0000")
                    TempData["AlertMsg"] = "刪除成功";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
    }
}
