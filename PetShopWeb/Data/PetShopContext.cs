using Microsoft.EntityFrameworkCore;
using PetShopWeb.Models;

namespace PetShopWeb.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Comment>? Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mammal" },
                new Category { Id = 2, Name = "Birds" },
                new Category { Id = 3, Name = "Fish" },
                new Category { Id = 4, Name = "Reptiles" },
                new Category { Id = 5, Name = "Amphibians" });

            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    Id = 1,
                    Name = "Dog",
                    Age = 5,
                    Description = "dog, domestic mammal of the family Canidae (order Carnivora). It is a subspecies of the gray wolf and is related to foxes and jackals. The dog is one of the two most ubiquitous and most popular domestic animals in the world (the cat is the other). For more than 12,000 years it has lived with humans as a hunting companion, protector, object of scorn or adoration, and friend.",
                    ImagePath = "~/photos/dog.jpg",
                    CategoryId = 1
                },
                new Animal
                {
                    Id = 2,
                    Name = "Cat",
                    Age = 3,
                    Description = "cat, also called house cat or domestic cat, domesticated member of the family Felidae, order Carnivora, and the smallest member of that family. Like all felids, domestic cats are characterized by supple low-slung bodies, finely molded heads, long tails that aid in balance, and specialized teeth and claws that adapt them admirably to a life of active hunting. Cats possess other features of their wild relatives in being basically carnivorous, remarkably agile and powerful, and finely coordinated in movement.",
                    ImagePath = "~/photos/cat.jpg",
                    CategoryId = 1
                },
                new Animal
                {
                    Id = 3,
                    Name = "Eagle",
                    Age = 8,
                    Description = "eagle, any of many large, heavy-beaked, big-footed birds of prey belonging to the family Accipitridae (order Accipitriformes).Eagles are monogamous. They mate for life and use the same nest each year. They tend to nest in inaccessible places, incubating a small clutch of eggs for six to eight weeks.",
                    ImagePath = "~/photos/eagle.jpg",
                    CategoryId = 2
                },
                new Animal
                {
                    Id = 4,
                    Name = "Ostrich",
                    Age = 11,
                    Description = "ostrich, large flightless bird found only in open country in Africa. The largest living bird, an adult male may be 2.75 metres tall—almost half of its height is neck—and weigh more than 150 kg, the female is somewhat smaller. The ostrich’s egg, averaging about 150 mm in length by 125 mm in diameter and about 1.35 kg, is also the world’s largest.",
                    ImagePath = "~/photos/ostrich.jpg",
                    CategoryId = 2
                },
                new Animal
                {
                    Id = 5,
                    Name = "White Shark",
                    Age = 7,
                    Description = "white shark, also called great white shark or white pointer, any member of the largest living species of the mackerel sharks and one of the most powerful and dangerous predatory sharks in the world.",
                    ImagePath = "~/photos/whiteshark.jpg",
                    CategoryId = 3
                },
                new Animal
                {
                    Id = 6,
                    Name = "Goldfish",
                    Age = 1,
                    Description = "goldfish, ornamental aquarium and pond fish of the carp family native to East Asia but introduced into many other areas.The goldfish is naturally greenish-brown or gray. The species, however, is variable, and numerous abnormalities occur. The goldfish is omnivorous, feeding on plants and small animals.",
                    ImagePath = "~/photos/goldfish.jpg",
                    CategoryId = 3
                },
                new Animal
                {
                    Id = 7,
                    Name = "Crocodile",
                    Age = 30,
                    Description = "A large, ponderous, amphibious animals of lizard-like appearance and carnivorous habit belonging to the reptile order Crocodylia. Crocodiles have powerful jaws with many conical teeth and short legs with clawed webbed toes. They share a unique body form that allows the eyes, ears, and nostrils to be above the water surface while most of the animal is hidden below. The tail is long and massive, and the skin is thick and plated.",
                    ImagePath = "~/photos/crocodile.jpg",
                    CategoryId = 4
                },
                new Animal
                {
                    Id = 8,
                    Name = "Anaconda",
                    Age = 13,
                    Description = "anaconda, either of two species of constricting, water-loving snakes found in tropical South America. The green anacondan is an olive-coloured snake with alternating oval-shaped black spots. The yellow, or southern, anaconda is much smaller and has pairs of overlapping spots.",
                    ImagePath = "~/photos/anaconda.jpg",
                    CategoryId = 4
                },
                new Animal
                {
                    Id = 9,
                    Name = "Frog",
                    Age = 12,
                    Description = "frog, any of various tailless amphibians belonging to the order Anura. In general, frogs have protruding eyes, no tail, and strong, webbed hind feet that are adapted for leaping and swimming. They also possess smooth, moist skins. Many are predominantly aquatic, but some live on land, in burrows, or in trees.",
                    ImagePath = "~/photos/frog.jpg",
                    CategoryId = 5
                },
                new Animal
                {
                    Id = 10,
                    Name = "Salamander",
                    Age = 10,
                    Description = "salamander, any member of a group of about 740 species of amphibians that have tails and that constitute the order Caudata. Salamanders are generally short-bodied, four-legged, moist-skinned animals, about 10 to 15 cm long. Many are camouflaged, whereas others are boldly patterned or brightly coloured.",
                    ImagePath = "~/photos/salamander.jpg",
                    CategoryId = 5
                }
                );

            modelBuilder.Entity<Comment>().HasData(
                 new Comment { Id = 1, AnimalId = 1, CommentText = "beautiful dog" },
                 new Comment { Id = 2, AnimalId = 5, CommentText = "looks eerie" },
                 new Comment { Id = 3, AnimalId = 1, CommentText = "it is so cute:)" },
                 new Comment { Id = 4, AnimalId = 10, CommentText = "slippery" }
                );
        }
    }
}
