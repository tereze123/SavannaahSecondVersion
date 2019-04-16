using Savannaah.Animals;

namespace Savannah.Animals.Factories
{
    public interface IAnimalFactory
    {
        Animal ReturnNewAnimal(string animalName);
    }
}