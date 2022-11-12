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
            return GetCategories().FirstOrDefault(c => c.Id == categoryId)!.Animals!;
        }
    }
}
