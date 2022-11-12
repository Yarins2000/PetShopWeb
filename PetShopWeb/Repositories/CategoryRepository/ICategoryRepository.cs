using PetShopWeb.Models;

namespace PetShopWeb.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Animal> GetAnimalsByCategory(int categoryId);
    }
}
