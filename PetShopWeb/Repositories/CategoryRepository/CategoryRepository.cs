using PetShopWeb.Data;
using PetShopWeb.Models;

namespace PetShopWeb.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PetShopContext context;

        public CategoryRepository(PetShopContext context)
        {
            this.context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            return context.Categories!;
        }

        public IEnumerable<Animal> GetAnimalsByCategory(int categoryId)
        {
            var category = context.Categories!.Find(categoryId);
            if (category is not null)
                return category.Animals!;
            return context.Animals!;
        }
    }
}
