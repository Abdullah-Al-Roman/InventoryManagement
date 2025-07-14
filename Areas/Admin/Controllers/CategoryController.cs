using InventoryManagement.Data;
using InventoryManagement.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin"), Route("Category")]
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("AddCategory")]
        public IActionResult AddCategory()
        {
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Categorys = _context.Categorys.ToList();
            return View();
        }
        [HttpPost("AddCategorySubmit")]
        public IActionResult AddCategorySubmit(Categorys model)
        {

            if (model.CategoryName != null)
            {


                bool namecheck = _context.Categorys.Where(anika => anika.CategoryName == model.CategoryName).Any();

                if (namecheck == false)
                {
                    _context.Categorys.Add(model);
                    _context.SaveChanges();
                }

                return RedirectToAction("CategoryList");
            }

            return RedirectToAction("AddCategory");
        }

        // GET: Edit Category
        [Route("EditCategory")]
        public IActionResult EditCategory(Guid id)
        {
            var data = _context.Categorys.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // POST: Edit Category
        [HttpPost("EditCategory")]
        public IActionResult EditCategory(Categorys category)
        {
            var datacheck = _context.Categorys.FirstOrDefault(x => x.CategoryID == category.CategoryID);

            if (datacheck != null)
            {
                datacheck.CategoryName = category.CategoryName;
                _context.Update(datacheck);
                _context.SaveChanges();
            }

            return RedirectToAction("CategoryList");
        }

        [Route("CategoryList")]
        public IActionResult CategoryList()
        {
            var datalist = _context.Categorys.ToList();
            return View(datalist);
        }

        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = _context.Categorys.FirstOrDefault(a => a.CategoryID == id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("CategoryList");
        }

    }
}