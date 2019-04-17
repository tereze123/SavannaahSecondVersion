using Savannaah.Animals;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;

namespace Savannah.Animals
{
    public class Antelope : Animal
    {
        public Antelope(IConfiguration configuration, 
            IPositionOnFieldValidation positionOnFieldValidation,
            IPositionOnFieldFactory positionOnFieldFactory,
            IRandomiserFactory randomiserFactory) 
            :base(configuration, 
                 positionOnFieldValidation, 
                 positionOnFieldFactory,
                 randomiserFactory)
        {
            this.Name = "A";
            this.VisionRange = 1;
            this.EnemiesName = "L";
        }
    }
}
