using Savannah.Animals.Factories;
using Savannah.Common;
using Savannah.FieldOfGame;
using Savannah.GameCoordinator.Loop;

namespace Savannah.GameCoordinator.Factories
{
    public class LoopOfGameFactory : ILoopOfGameFactory
    {

        private readonly IConfiguration configuration;
        private readonly IAnimalFactory animalFactory;

        public LoopOfGameFactory( IConfiguration configuration, IAnimalFactory animalFactory)
        {
            this.configuration = configuration;
            this.animalFactory = animalFactory;
        }

        public ILoopOfGame GetLoopOfGame(IGameField gameField)
        {
            return new LoopOfGame(gameField, configuration, animalFactory);
        }
    }
}
