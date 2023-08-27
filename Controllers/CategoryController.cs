using BulkWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkWeb.Controllers
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
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Category Object</param>
        /// <returns> view of index page</returns>
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid) { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
                TempData["succsess"] = "Category Created successfully";
            }
            return RedirectToAction("Index", "Category");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> this id referes to the category on the DB</param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            //find category by ID
            Category categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Category Object</param>
        /// <returns> view of index page</returns>
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["succsess"] = "Category Updated successfully";
            }
            return RedirectToAction("Index", "Category");
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //find category by ID
            Category categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Category Object</param>
        /// <returns> view of index page</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["succsess"] = "Category Removed successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
