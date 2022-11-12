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
        private readonly ICategoryRepository _CategoryRepository;

        public AdministratorController(ICategoryRepository categoryRepository, IAnimalRepository animalRepository)
        {
            _CategoryRepository = categoryRepository;
            _animalRepository = animalRepository;
        }

        public IActionResult ManageAnimals()
        {
            ViewBag.Categories = _CategoryRepository.GetCategories();
            return View(_animalRepository.GetAnimals());
        }

        public IActionResult AddNewAnimal()
        {
            ViewBag.Categories = _CategoryRepository.GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAnimal(Animal newAnimal)
        {
            if(ModelState.IsValid)
            {
                _animalRepository.AddNewAnimal(newAnimal);
            }
            return RedirectToAction("ShowCatalog", controllerName:"Catalog");
        }
    }
}
