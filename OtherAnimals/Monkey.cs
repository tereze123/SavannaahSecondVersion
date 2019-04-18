using System.ComponentModel.Composition;
using Savannaah.Animals;
using Savannah.PositionOnField;

namespace OtherAnimals
{
    [Export(typeof(object))]
    public class Monkey
    {
        public string EnemiesName { get; set; }
        public string Name { get ; set; }
        public int VisionRange { get; set; }

        public Monkey()
        {
            this.EnemiesName = "L";
            this.Name = "M";
            this.VisionRange = int.MaxValue;
        }


        public void EnemyIsInRangeMovementNextPosition(IAnimal[,] initialGeneration, IAnimal[,] nextGenerationArray, int rowPositionOfEnemy, int columnPositionOfEnemy, int rowPositionOfAnimal, int columnPositionOfAnimal)
        {

        }

        public Savannah.PositionOnField.PositionOnField EnemysPositionOnField(IAnimal[,] initialGeneration, int rowPosition, int columnPosition)
        {
            for (int i = 0; i < initialGeneration.GetLength(0); i++)
            {
                for (int j = 0; j < initialGeneration.GetLength(1); j++)
                {
                    if (initialGeneration[i, j] != null && initialGeneration[i, j].Name == EnemiesName)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public void PeaceStateMovementNextPosition(IAnimal[,] initialGeneration, Animal[,] nextGenerationArray, int rowPosition, int columnPosition)
        {

        }
    }
}
