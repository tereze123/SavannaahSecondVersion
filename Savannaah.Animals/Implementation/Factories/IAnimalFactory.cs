using AccessLibraryForPlugins.Animals;
using System.ComponentModel.Composition;

namespace Savannah.Animals.Factories
{
    public interface IAnimalFactory
    {
        [Import(typeof(IAnimal))]
        IAnimal AnimalObjects { get; set; }
        IAnimal ReturnNewAnimal(string animalName);
    }
}