using Microsoft.EntityFrameworkCore;
using PetShopWeb.Data;
using PetShopWeb.Models;

namespace PetShopWeb.Repositories.AnimalRepository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PetShopContext context;

        public AnimalRepository(PetShopContext context)
        {
            this.context = context;
        }

        public IEnumerable<Animal> GetAnimals()
        {
            return context.Animals!;
        }

        public Animal? GetAnimalById(int id)
        {
            return context.Animals!.Find(id);
        }

        public void AddNewAnimal(Animal newAnimal)
        {
            if (newAnimal is not null)
            {
                context.Animals!.Add(newAnimal);
                context.SaveChanges();
            }
        }
        public void UpdateAnimal(int id, Animal updatedAnimal)
        {
            var animal = context.Animals!.Find(id);
            if (animal is not null)
            {
                animal.ImagePath = "~/photos/" + updatedAnimal.ImagePath;
                animal.Name = updatedAnimal.Name;
                animal.Description = updatedAnimal.Description;
                animal.Age = updatedAnimal.Age;
                context.SaveChanges();
            }
        }
        public void DeleteAnimal(int id)
        {
            var animal = context.Animals!.Find(id);
            if (animal is not null)
            {
                context.Animals!.Remove(animal);
                context.SaveChanges();
            }
        }

        public IEnumerable<Animal> GetMostReviewedAnimals(int animalsCount)
        {
            return context.Animals!.OrderByDescending(a => a.Comments!.Count).Take(animalsCount).ToList();
        }
    }
}
