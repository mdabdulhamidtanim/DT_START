using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Start.DataAccess.Data;
using Start.DataAccess.Repository;
using Start.DataAccess.Repository.IRepository;
using Start.Models;
using Start.Models.ViewModels;

namespace Start.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfwork,IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitOfwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitofwork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id==null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product=_unitofwork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
                
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            //if (obj.Name !=null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "This name is invalid");
            //}
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootPath, @"Images\Product");



                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var OldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(ProductPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                         
                    }
                    productVM.Product.ImageUrl = @"\Images\Product\" + filename;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitofwork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitofwork.Product.Update(productVM.Product);
                }



             
                _unitofwork.Save();
                TempData["SUCCESS"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            }
            return View(productVM);
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitofwork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitofwork.Product.Remove(productToBeDeleted);
            _unitofwork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}