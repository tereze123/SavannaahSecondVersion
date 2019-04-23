using AccessLibraryForPlugins.Animals;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;
using System;
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

        [Import(typeof(IAnimal))]
        public IAnimal animalTypes;

        public IAnimal AnimalObjects
        {
            get { return animalTypes; }
            set { animalTypes = value; }
        }

        private List<IAnimal> typesOfAnimalAvailable;

        public AnimalFactory(IConfiguration configuration,
            IPositionOnFieldValidation positionOnFieldValidation,
            IPositionOnFieldFactory positionOnFieldFactory,
            IRandomiserFactory randomiserFactory)
        {
            this.configuration = configuration;
            this.positionOnFieldValidation = positionOnFieldValidation;
            this.positionOnFieldFactory = positionOnFieldFactory;
            this.randomiserFactory = randomiserFactory;
            typesOfAnimalAvailable = new List<IAnimal>();

        }

        public IAnimal ReturnNewAnimal(string animalName)
        {
            typesOfAnimalAvailable.Add(animalTypes);
            Antelope antelope = new Antelope(configuration, positionOnFieldValidation, positionOnFieldFactory, randomiserFactory);
            Lion lion = new Lion(configuration, positionOnFieldValidation, positionOnFieldFactory, randomiserFactory);

            typesOfAnimalAvailable.Add(antelope);
            typesOfAnimalAvailable.Add(lion);

            foreach (var animalTyppe in typesOfAnimalAvailable)
            {

                if (animalTyppe.Name == animalName)
                {
                    if (animalTyppe.Name == configuration.GetNameOfAntelope() || animalTyppe.Name == configuration.GetNameOfLion())
                    {
                        return Activator.CreateInstance(animalTyppe.GetType(), configuration,
                            positionOnFieldValidation, 
                            positionOnFieldFactory, 
                            randomiserFactory) as IAnimal;
                    }
                    else
                    {
                        return Activator.CreateInstance(animalTyppe.GetType()) as IAnimal;
                    }
                }
            }
            return null;
        }
    }
}
