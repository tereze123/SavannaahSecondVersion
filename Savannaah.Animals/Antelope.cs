using Savannaah.Animals;
using Savannah.Common;
using Savannah.PositionOnField;

namespace Savannah.Animals
{
    public class Antelope : Animal
    {

        public Antelope(IConfiguration configuration, IPositionOnFieldValidation positionOnFieldValidation) :base(configuration, positionOnFieldValidation)
        {
            this.Name = "A";
            this.VisionRange = 1;
            this.EnemiesName = "L";
        }
    }
}
