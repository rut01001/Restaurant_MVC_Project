using DalInfraContracts;
using MenueService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using uniqe_web.Models;
using RestaurantContracts;
using Microsoft.AspNetCore.Http;

namespace uniqe_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IDAL dal;
        Imenu menuService;
        IUserService userService;

        public HomeController(ILogger<HomeController> logger, Imenu menuService, IUserService userService)
        {
            _logger = logger;
            this.menuService = menuService;
            this.userService = userService;
        }
        public IActionResult Menu()
        {
            ModelData modelData = new ModelData();
            DataSet ds = menuService.GetCategories();
            if (ds != null && ds.Tables.Count > 0 &&
                ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                modelData.ListCategories = ds.Tables[0];
            }
            modelData.IsAdmin = userService.IsAdmin.ToString().ToLower();
            //modelData.NameDish = "עגבניותממממ";// (string)TempData["NameDish"];

            return View("Menu", modelData);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Home()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetDetailsDishesForCategory(int KeyCategory, bool IsAllergy)
        {
            TempData["KeyCategory"] = KeyCategory;
            DataTable dt = new DataTable();
            DataSet ds = menuService.GetDishesCategory(KeyCategory, IsAllergy);
            if (ds != null && ds.Tables.Count > 0 &&
                ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Json(new { result = json });
        }
        [HttpPost]
        public ActionResult AddDish(string NameDish, int PriceDish, string AllergyDish)
        {
            int categoryId = (int)TempData["KeyCategory"];
            bool isAllergy = false;
            if (AllergyDish == "on")
                isAllergy = true;
            menuService.InsertDish(NameDish, PriceDish, isAllergy, categoryId);
            return RedirectToAction("Menu");
        }
        [HttpPost]
        public ActionResult EditDish(string NameDish, float PriceDish, string AllergyDish)
        {
            //string dishId = (string)TempData["DishId"];
            string dishId = HttpContext.Session.GetString("DishId");
            bool isAllergy = false;
            if (AllergyDish == "on")
                isAllergy = true;
            menuService.EditDish(NameDish, PriceDish, isAllergy, dishId);
            return RedirectToAction("Menu");
        }
        [HttpPost]
        public ActionResult saveStatusOfUser(bool statusOfUser)
        {
            userService.IsAdmin = statusOfUser;
            return RedirectToAction("Home");
        }
        [HttpPost]
        public ActionResult SaveDishId(string DishId)
        {
            //TempData["DishId"] = DishId;
            HttpContext.Session.SetString("DishId", DishId);
            return RedirectToAction("Menu");
        }
        [HttpPost]
        public ActionResult deleteDish(string idDish)
        {
            menuService.deleteDish(idDish);
            return RedirectToAction("Menu");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
