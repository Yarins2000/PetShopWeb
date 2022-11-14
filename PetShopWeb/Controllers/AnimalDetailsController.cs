using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Models;
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
            ViewBag.ErrorForComment = new Comment().CommentText;
            if(ModelState.IsValid)
            {
                _commentRepository.AddComment(animalId, commentText);
            }
            return RedirectToAction("AnimalDetails", routeValues: new {animalId});
        }
    }
}
