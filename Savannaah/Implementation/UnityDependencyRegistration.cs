using Savannah.Animals.Factories;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.FieldOfGame.Factories;
using Savannah.GameCoordinator;
using Savannah.GameCoordinator.Factories;
using Savannah.InputAndOutput;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;
using Unity;
using Unity.Lifetime;

namespace Savannah.Client.Implementation
{
    public class UnityDependencyRegistration
    {
        private IUnityContainer container;

        public UnityDependencyRegistration()
        {
            container = new UnityContainer();
        }

        public void RegisterTypes()
        {            
            container.RegisterType<Common.IConfiguration, Configuration>();
            container.RegisterType<IRandomiserFactory, RandomiserFactory>();
            container.RegisterType<IUserInput, UserInputForConsole>();
            container.RegisterType<IPositionOnFieldFactory, PositionOnFieldFactory>();
            container.RegisterType<IGameFieldDrawer, GameFieldDrawerForConsole>();
            container.RegisterType<IPositionOnFieldValidation, PositionOnFieldValidation>();

            container.RegisterType<IAnimalFactory, AnimalFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGameFieldFactory, GameFieldFactory>();
            container.RegisterType<GameManager, GameManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoopOfGameFactory, LoopOfGameFactory>();
        }

        public GameManager GetGameManager()
        {
            var game = container.Resolve<GameManager>();
            return game;
        }

        public IAnimalFactory GetAnimalFactory()
        {
            return container.Resolve<AnimalFactory>();
        }

        public Common.IConfiguration GetConfiguration()
        {
            return container.Resolve<Configuration>();
        }
    }
}
