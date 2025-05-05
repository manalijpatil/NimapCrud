using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NimapCrud.Models;

namespace NimapCrud.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        ProductCrud productdb;
        CategoryCrud categorydb;
        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
            productdb = new ProductCrud(this.configuration);
            categorydb = new CategoryCrud(this.configuration);
        }
        public IActionResult Index(int pg = 1)
        {
            var result = productdb.GetProducts();
            const int pagesize = 10;
            if (pg < 1)
            {
                pg = 1;
            }
            int rescount = result.Count();
            var pager = new Pager(rescount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var data = result.Skip(recskip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }
        public IActionResult Details(int id)
        {
            var res = productdb.GetProductById(id);
            return View(res);
        }
        public IActionResult Create()
        {
            ViewBag.categories = categorydb.GetCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            try
            {
                int result = productdb.AddProduct(product);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Sometimes went wrong";

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
            }
            ViewBag.categories = new SelectList(categorydb.GetCategories(), "CategoryId", "CategoryName");
            return View(product);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.categories = categorydb.GetCategories();
            var product = productdb.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            try
            {
                int response = productdb.UpdateProduct(product);
                if (response >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErroMsg = "something went wrong";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
       
        }
        public IActionResult Delete(int id)
        {
            var res = productdb.DeleteProduct(id);
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                int response = productdb.DeleteProduct(id);
                if(response >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
           
        }
    }
}
