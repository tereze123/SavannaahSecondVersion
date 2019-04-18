using Savannah.PositionOnField;

namespace Savannaah.Animals
{
    public interface IAnimal
    {
        string EnemiesName { get; set; }
        string Name { get; set; }
        int VisionRange { get; set; }

        void EnemyIsInRangeMovementNextPosition(Animal[,] initialGeneration, Animal[,] nextGenerationArray, int rowPositionOfEnemy, int columnPositionOfEnemy, int rowPositionOfAnimal, int columnPositionOfAnimal);
        PositionOnField EnemysPositionOnField(Animal[,] initialGeneration, int rowPosition, int columnPosition);
        void PeaceStateMovementNextPosition(Animal[,] initialGeneration, Animal[,] nextGenerationArray, int rowPosition, int columnPosition);
    }
}