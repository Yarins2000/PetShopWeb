using PetShopWeb.Data;
using PetShopWeb.Models;

namespace PetShopWeb.Repositories.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly PetShopContext context;

        public CommentRepository(PetShopContext context)
        {
            this.context = context;
        }

        public void AddComment(int animalId, string commentText)
        {
            Comment comment = new()
            {
               AnimalId = animalId,
               CommentText = commentText
            };
            context.Comments!.Add(comment);
            context.SaveChanges();
        }
        public bool IsCommentExist(string commentText)
        {
            return context.Comments.Any(c => c.CommentText == commentText);
        }
    }
}
