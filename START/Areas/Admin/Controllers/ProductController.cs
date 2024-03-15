using Microsoft.AspNetCore.Mvc;
using Start.DataAccess.Data;
using Start.DataAccess.Repository.IRepository;
using Start.Models;

namespace Start.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductController(IUnitOfWork unitOfwork)
        {
            _unitofwork = unitOfwork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitofwork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            
            //if (obj.Name !=null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "This name is invalid");
            //}
            if (ModelState.IsValid)
            {
                _unitofwork.Product.Add(obj);
                _unitofwork.Save();
                TempData["SUCCESS"] = "Product Created Successfully";
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
            Product? productFromDb = _unitofwork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Product.update(obj);
                _unitofwork.Save();

                TempData["SUCCESS"] = "Product Updated Successfully";
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
            Product? productFromDb = _unitofwork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitofwork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.Product.Remove(obj);
            _unitofwork.Save();
            TempData["SUCCESS"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}