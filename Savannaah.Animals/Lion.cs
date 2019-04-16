using Savannaah.Animals;

namespace Savannah.Animals
{
    public class Lion :Animal
    {
        public Lion() : base()
        {
            this.Name = "L";
            this.VisionRange = 1;
            this.EnemiesName = "A";
        }
        public override void EnemyIsInRangeMovementNextPosition(
            Animal[,] initialGeneration, 
            Animal[,] nextGenerationArray, 
            int rowPositionOfEnemy, 
            int columnPositionOfEnemy, 
            int rowPositionOfAnimal, 
            int columnPositionOfAnimal)
        {
            nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] = this;
        }

        public override void PeaceStateMovementNextPosition(
            Animal[,] initialGeneration, 
            Animal[,] nextGenerationArray, 
            int rowPosition, 
            int columnPosition)
        {
            nextGenerationArray[rowPosition, columnPosition] = this;
        }
    }
}
