using PetShopWeb.Models;

namespace PetShopWeb.Repositories.CommentRepository
{
    public interface ICommentRepository
    {
        void AddComment(int animalId, string commentText);
        bool IsCommentExist(int animalId, string commentText);
    }
}
