using Savannaah.Animals;
using Savannah.Common;
using Savannah.PositionOnField;

namespace Savannah.Animals.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        private readonly IConfiguration configuration;
        private readonly IPositionOnFieldValidation positionOnFieldValidation;

        public AnimalFactory(IConfiguration configuration, IPositionOnFieldValidation positionOnFieldValidation)
        {
            this.configuration = configuration;
            this.positionOnFieldValidation = positionOnFieldValidation;
        }

        public Animal ReturnNewAnimal(string animalName)
        {
            if (animalName == configuration.GetNameOfAntelope())
            {
                return new Antelope(configuration, positionOnFieldValidation);
            }
            else if (animalName == configuration.GetNameOfLion())
            {
                return new Lion(configuration, positionOnFieldValidation);
            }
            else
            {
                return null;
            }
        }
    }
}
