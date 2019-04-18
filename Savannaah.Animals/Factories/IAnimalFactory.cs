using Savannaah.Animals;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Savannah.Animals.Factories
{
    public interface IAnimalFactory
    {
        [Import(typeof(object))]
        object AnimalObjects { get; set; }
        Animal ReturnNewAnimal(string animalName);
    }
}