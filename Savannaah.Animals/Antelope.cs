using Savannaah.Animals;

namespace Savannah.Animals
{
    public class Antelope : Animal
    {

        public Antelope():base()
        {
            this.Name = "A";
            this.VisionRange = 1;
            this.EnemiesName = "L";
        }

        public override void EnemyIsInRangeMovementNextPosition(
                             Animal[,] initialGeneration,
                             Animal[,] nextGenerationArray,
                             int rowPositionOfEnemy,
                             int columnPositionOfEnemy,
                             int rowPositionOfAnimal,
                             int columnPositionOfAnimal)
        {
            if (nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] == null)
            {
                nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] = this;
            }
        }

        public override void PeaceStateMovementNextPosition(
                             Animal[,] initialGeneration,
                             Animal[,] nextGenerationArray,
                             int rowPosition,
                             int columnPosition)
        {
            if (nextGenerationArray[rowPosition, columnPosition] == null)
            {
                nextGenerationArray[rowPosition, columnPosition] = this;
            }
        }
    }
}
