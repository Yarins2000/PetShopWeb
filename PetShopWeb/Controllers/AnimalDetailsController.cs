using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Repositories.AnimalRepository;
using PetShopWeb.Repositories.CommentRepository;

namespace PetShopWeb.Controllers
{
    public class AnimalDetailsController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICommentRepository _commentRepository;
        public AnimalDetailsController(IAnimalRepository animalRepository, ICommentRepository commentRepository)
        {
            _animalRepository = animalRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult AnimalDetails(int animalId)
        {
            return View(_animalRepository.GetAnimalById(animalId));
        }

        [HttpPost]
        public IActionResult AddNewComment(int animalId, string commentText)
        {
            if (ModelState.IsValid)
            {
                if (_commentRepository.IsCommentExist(animalId, commentText))
                {
                    ModelState.AddModelError("commentText", "This comment is already exist");
                    return View("AnimalDetails", model: _animalRepository.GetAnimalById(animalId));
                }
                _commentRepository.AddComment(animalId, commentText);
            }
            return RedirectToAction("AnimalDetails", routeValues: new { animalId });
        }
    }
}
