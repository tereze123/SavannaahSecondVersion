using Savannaah.Animals;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Savannah.Animals.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        private readonly IConfiguration configuration;
        private readonly IPositionOnFieldValidation positionOnFieldValidation;
        private readonly IPositionOnFieldFactory positionOnFieldFactory;
        private readonly IRandomiserFactory randomiserFactory;

        [ImportMany(typeof(Animal))]
        private IEnumerable<Animal> animalTypes;

        public AnimalFactory(IConfiguration configuration, 
            IPositionOnFieldValidation positionOnFieldValidation,
            IPositionOnFieldFactory positionOnFieldFactory,
            IRandomiserFactory randomiserFactory)
        {
            this.configuration = configuration;
            this.positionOnFieldValidation = positionOnFieldValidation;
            this.positionOnFieldFactory = positionOnFieldFactory;
            this.randomiserFactory = randomiserFactory;
        }

        public Animal ReturnNewAnimal(string animalName)
        {
            if (animalName == configuration.GetNameOfAntelope())
            {
                return new Antelope(configuration, positionOnFieldValidation, positionOnFieldFactory, randomiserFactory);
            }
            else if (animalName == configuration.GetNameOfLion())
            {
                return new Lion(configuration, positionOnFieldValidation, positionOnFieldFactory, randomiserFactory);
            }
            else
            {
                return null;
            }
        }
    }
}
