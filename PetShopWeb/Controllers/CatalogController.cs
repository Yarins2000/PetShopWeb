using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Models;
using PetShopWeb.Repositories.AnimalRepository;
using PetShopWeb.Repositories.CategoryRepository;

namespace PetShopWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CatalogController(IAnimalRepository animalRepository, ICategoryRepository categoryRepository)
        {
            _animalRepository = animalRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult ShowCatalog(int id)
        {
            ViewBag.Categories = _categoryRepository.GetCategories();

            if (id is 0)
                return View(_animalRepository.GetAnimals());

            return View(_categoryRepository.GetAnimalsByCategory(id));
        }
    }
}
