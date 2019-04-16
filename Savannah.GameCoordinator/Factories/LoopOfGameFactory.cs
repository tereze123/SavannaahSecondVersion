using Savannah.Animals.Factories;
using Savannah.Common;
using Savannah.FieldOfGame;
using Savannah.GameCoordinator.Loop;
using Savannah.PositionOnField.Factories;

namespace Savannah.GameCoordinator.Factories
{
    public class LoopOfGameFactory : ILoopOfGameFactory
    {

        private readonly IConfiguration configuration;
        private readonly IAnimalFactory animalFactory;
        private readonly IPositionOnFieldFactory positionOnFieldFactory;

        public LoopOfGameFactory(IConfiguration configuration, IAnimalFactory animalFactory, IPositionOnFieldFactory positionOnFieldFactory)
        {
            this.configuration = configuration;
            this.animalFactory = animalFactory;
            this.positionOnFieldFactory = positionOnFieldFactory;
        }

        public ILoopOfGame GetLoopOfGame(IGameField gameField)
        {
            return new LoopOfGame(gameField, configuration, animalFactory, positionOnFieldFactory);
        }
    }
}
