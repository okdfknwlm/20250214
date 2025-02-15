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

        public async Task<IActionResult> Index() => View(await _Api.GetEmpListAsync());

        public IActionResult CreateEmp() => PartialView("_CreateEmp", new EmpModel());

        [HttpPost]
        public async Task<IActionResult> CreateEmpPost(EmpModel model)
        {
            try
            {
                var response = await _Api.CreateEmpAsync(model);
                ViewBag.Msg = response;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> EditEmp(int EmpID)
        {
            var data = await _Api.GetEmpAsync(EmpID);
            return PartialView("_EditEmp", data);

        }

        [HttpPost]
        public async Task<IActionResult> EditEmpPost(int EmpID, EmpModel model)
        {
            try
            {
                var response = await _Api.UpdateEmpAsync(EmpID, model);
                ViewBag.Msg = response;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteEmp(int EmpID)
        {
            var data = await _Api.GetEmpAsync(EmpID);
            return PartialView("_DeleteEmp", data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmpPost(int EmpID)
        {
            try
            {
                var response = await _Api.DeleteEmpAsync(EmpID);
                ViewBag.Msg = response;
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
