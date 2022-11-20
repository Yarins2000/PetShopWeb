using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Repositories.AnimalRepository;

namespace PetShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalRepository animalRepository;

        public HomeController(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public IActionResult HomePage()
        {
            return View(animalRepository.GetMostReviewedAnimals(2));
        }
    }
}
