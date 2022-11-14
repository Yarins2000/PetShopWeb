using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Models;
using PetShopWeb.Repositories.AnimalRepository;
using PetShopWeb.Repositories.CategoryRepository;
using PetShopWeb.Repositories.CommentRepository;

namespace PetShopWeb.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdministratorController(ICategoryRepository categoryRepository, IAnimalRepository animalRepository)
        {
            _categoryRepository = categoryRepository;
            _animalRepository = animalRepository;
        }

        public IActionResult ManageAnimals(int categoryId)
        {
            ViewBag.Categories = _categoryRepository.GetCategories();
            if (categoryId is 0)
                return View(_animalRepository.GetAnimals());
            return View(_categoryRepository.GetAnimalsByCategory(categoryId));
        }

        public IActionResult AddNewAnimal()
        {
            ViewBag.Categories = _categoryRepository.GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAnimal(Animal newAnimal)
        {
            ViewBag.Categories = _categoryRepository.GetCategories();
            if (ModelState.IsValid)
            {
                _animalRepository.AddNewAnimal(newAnimal);
                return RedirectToAction("ShowCatalog", controllerName: "Catalog");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            //if (id is null || id is 0)
            //    return NotFound();
            //var category = _db.Categories.Find(id);
            //if (category is null)
            //    return NotFound();
            //return View(category);
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Edit(Animal animal)
        {
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot match the Name.");
            //}

            //if (ModelState.IsValid)
            //{
            //    _db.Categories.Update(category);
            //    _db.SaveChanges();
            //    TempData["success"] = "Category has been updated successfully";
            //    return RedirectToAction("Index");
            //}
            //return View(category);
            return Content("df");
        }

        public IActionResult Delete(int id)
        {
            _animalRepository.DeleteAnimal(id);
            return RedirectToAction("ManageAnimals");
        }
    }
}
