using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Models;
using PetShopWeb.Repositories.AnimalRepository;
using PetShopWeb.Repositories.CategoryRepository;

namespace PetShopWeb.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdministratorController(ICategoryRepository categoryRepository, IAnimalRepository animalRepository, IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _animalRepository = animalRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult ManageAnimals(int id)
        {
            ViewBag.Categories = _categoryRepository.GetCategories();
            if (id is 0)
                return View(_animalRepository.GetAnimals());
            return View(_categoryRepository.GetAnimalsByCategory(id));
        }

        public IActionResult AddNewAnimal()
        {
            ViewBag.Categories = _categoryRepository.GetCategories();
            ViewBag.AddOrEdit = "Add";
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAnimal(Animal newAnimal)
        {
            ViewBag.Categories = _categoryRepository.GetCategories();
            ViewBag.AddOrEdit = "Add";
            if (ModelState.IsValid)
            {
                newAnimal.ImagePath = "~/photos/" + newAnimal.ImagePath;
                _animalRepository.AddNewAnimal(newAnimal);
                return RedirectToAction("ShowCatalog", controllerName: "Catalog");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var animal = _animalRepository.GetAnimalById(id);
            if (animal is null)
                return RedirectToAction("HomePage", controllerName: "Home");
            ViewBag.Categories = _categoryRepository.GetCategories();
            ViewBag.AddOrEdit = "Edit";
            return View(animal);
        }

        [HttpPost]
        public IActionResult Edit(Animal animal)
        {
            ViewBag.AddOrEdit = "Edit";
            if (ModelState.IsValid)
            {
                _animalRepository.UpdateAnimal(animal.Id, animal);
                return RedirectToAction("ManageAnimals");
            }
            return Edit(animal.Id);
        }

        public IActionResult Delete(int id)
        {
            _animalRepository.DeleteAnimal(id);
            return RedirectToAction("ManageAnimals");
        }
    }
}
