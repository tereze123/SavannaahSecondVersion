using Savannah.Animals.Factories;
using Savannah.Client;
using Savannah.Client.Implementation;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.FieldOfGame;
using Savannah.FieldOfGame.Factories;
using Savannah.GameCoordinator;
using Savannah.GameCoordinator.Factories;
using Savannah.InputAndOutput;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;

namespace Savannaah
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityDependencyRegistration unityDependencyRegistration = new UnityDependencyRegistration();

            unityDependencyRegistration.RegisterTypes();

            var gameManager = unityDependencyRegistration.GetGameManager();
            gameManager.Start();
        }
    }
}
