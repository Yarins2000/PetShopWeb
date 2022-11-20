using PetShopWeb.Models;

namespace PetShopWeb.Repositories.AnimalRepository
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAnimals();
        Animal? GetAnimalById(int id);
        IEnumerable<Animal> GetMostReviewedAnimals(int animalsCount);
        void AddNewAnimal(Animal newAnimal);
        void UpdateAnimal(int id, Animal updatedAnimal);
        void DeleteAnimal(int id);
    }
}
