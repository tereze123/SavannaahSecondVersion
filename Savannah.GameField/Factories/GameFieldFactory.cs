using Savannah.Common;

namespace Savannah.FieldOfGame.Factories
{
    public class GameFieldFactory : IGameFieldFactory
    {
        private readonly IConfiguration configuration;

        public GameFieldFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IGameField GetGameField()
        {
            return new GameField(configuration);
        }
    }
}
