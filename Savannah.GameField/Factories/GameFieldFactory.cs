using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField.Factories;
using System.ComponentModel.Composition;

namespace Savannah.FieldOfGame.Factories
{
    public class GameFieldFactory : IGameFieldFactory
    {
        private readonly IConfiguration configuration;
        private readonly IPositionOnFieldFactory positionOnFieldFactory;
        private readonly IRandomiserFactory randomiserFactory;

        [ImportingConstructor]
        public GameFieldFactory(IConfiguration configuration, IPositionOnFieldFactory positionOnFieldFactory, IRandomiserFactory randomiserFactory)
        {
            this.configuration = configuration;
            this.positionOnFieldFactory = positionOnFieldFactory;
            this.randomiserFactory = randomiserFactory;
        }

        public IGameField GetGameField()
        {
            return new GameField(configuration, positionOnFieldFactory, randomiserFactory);
        }
    }
}
