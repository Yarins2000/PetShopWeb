using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Repositories.AnimalRepository;

namespace PetShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalRepository _animalRepository;

        public HomeController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public IActionResult HomePage()
        {
            return View(_animalRepository.GetMostReviewedAnimals(2));
        }
    }
}
