using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //List<Category> objCategoryList = _db.Categories.ToList();
            CategoryViewData vm = new CategoryViewData();
            vm.Categories = _db.Categories.ToList();
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewData obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(new() { Id = obj.Id,Name = obj.Name,DisplayOrder = obj.DisplayOrder });
                _db.SaveChanges();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb3 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            CategoryViewData vm = new CategoryViewData();
            vm.Id = categoryFromDb.Id;
            vm.Name = categoryFromDb.Name;
            vm.DisplayOrder = categoryFromDb.DisplayOrder;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewData obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }
            //Category = 
            if (ModelState.IsValid)
            {
                _db.Categories.Update( new() { Id=obj.Id,Name=obj.Name,DisplayOrder=obj.DisplayOrder} );
                _db.SaveChanges();
                TempData["success"] = "Category Updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            CategoryViewData vm = new CategoryViewData();
            vm.Id = categoryFromDb.Id;
            vm.Name = categoryFromDb.Name;
            vm.DisplayOrder = categoryFromDb.DisplayOrder;
            return View(vm);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
