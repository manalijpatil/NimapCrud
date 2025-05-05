using Microsoft.AspNetCore.Mvc;
using NimapCrud.Models;

namespace NimapCrud.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IConfiguration configuration;
        CategoryCrud categorydb;
        public CategoryController(IConfiguration configuration)
        {
            this.configuration = configuration; 
            categorydb = new CategoryCrud(this.configuration);
        }
        public IActionResult Index()
        {
            var response = categorydb.GetCategories();
            return View(response);
        }
        public IActionResult Details(int id)
        {
            var result = categorydb.GetCategoryById(id);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            try
            {
                int result = categorydb.AddCategory(category);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong!!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;

            }
                return View();
        }
        public IActionResult Edit(int id)
        {
            var res = categorydb.GetCategoryById(id);
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            try
            {
                int res = categorydb.UpdateCategory(category);
                if (res >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong!!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;

            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var res =categorydb.DeleteCategory(id);
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                int response = categorydb.DeleteCategory(id);
                if(response >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something Went wrong";

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErroMsg = ex.Message;

            }
            return View();
        }
    }
}
