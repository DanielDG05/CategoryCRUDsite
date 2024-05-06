using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //Make sure DO is not equal to Name
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //Use name instead of Customerror to display under name
                ModelState.AddModelError("name", "Display Order cannot be the same as the Name.");
            }
            //Server side Validation
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully.";
                //Normally have to state controller name after
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //Make sure DO is not equal to Name
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //Use name instead of Customerror to display under name
                ModelState.AddModelError("name", "Display Order cannot be the same as the Name.");
            }
            //Server side Validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                //Normally have to state controller name after
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete (int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST action method
        //Can change name so no need for deletePOST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            //based of id, delete
            var obj= _db.Categories.Find(id);
            if (obj == null) {
                return NotFound();  
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            //Cannot have a second return
            return RedirectToAction("Index");
        }
    }
}
