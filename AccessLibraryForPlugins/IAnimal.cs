using Savannah.PositionOnField;

namespace Savannaah.Animals
{
    public interface IAnimal
    {
        string EnemiesName { get; set; }
        string Name { get; set; }
        int VisionRange { get; set; }

        void EnemyIsInRangeMovementNextPosition(IAnimal[,] initialGeneration, IAnimal[,] nextGenerationArray, int rowPositionOfEnemy, int columnPositionOfEnemy, int rowPositionOfAnimal, int columnPositionOfAnimal);
        PositionOnField EnemysPositionOnField(IAnimal[,] initialGeneration, int rowPosition, int columnPosition);
        void PeaceStateMovementNextPosition(IAnimal[,] initialGeneration, IAnimal[,] nextGenerationArray, int rowPosition, int columnPosition);
    }
}