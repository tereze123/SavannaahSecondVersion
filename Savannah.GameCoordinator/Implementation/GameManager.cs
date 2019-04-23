﻿using Savannah.Animals.Factories;
using Savannah.Common;
using Savannah.FieldOfGame.Factories;
using Savannah.GameCoordinator.Factories;
using Savannah.GameCoordinator.Loop;
using Savannah.InputAndOutput;
using System.ComponentModel.Composition;
using System.Threading;
namespace Savannah.GameCoordinator
{
    public class GameManager
    {
        private readonly ILoopOfGame loopOfGame;
        private readonly IUserInput userInput;
        private readonly IGameFieldDrawer gameFieldDrawer;
        private readonly FieldOfGame.IGameField gameField;
        private readonly IConfiguration configuration;
        private readonly IAnimalFactory animalFactory;
        private readonly ILoopOfGameFactory loopOfGameFactory;

        [ImportingConstructor]
        public GameManager(IUserInput userInput,
                           IGameFieldDrawer gameFieldDrawer,
                           IConfiguration configuration,
                           IAnimalFactory animalFactory,
                           IGameFieldFactory gameFieldFactory,
                           ILoopOfGameFactory loopOfGameFactory)
        {
            this.configuration = configuration;
            this.animalFactory = animalFactory;
            this.loopOfGameFactory = loopOfGameFactory;
            gameField = gameFieldFactory.GetGameField();
            loopOfGame = loopOfGameFactory.GetLoopOfGame(gameField);
            this.userInput = userInput;
            this.gameFieldDrawer = gameFieldDrawer;
        }

        public void Start()
        {
            string userKeyPressed = string.Empty;
            gameFieldDrawer.DrawGameField(gameField);
            do
            {
                if (userInput.IsKeyPressed())
                {
                    userKeyPressed = userInput.ReturnKeyPressed();
                    loopOfGame.UsersTurnToAddAnimals(gameField, userKeyPressed);
                    gameFieldDrawer.DrawGameField(gameField);
                }
                loopOfGame.LoopThroughTheGame();
                Thread.Sleep(500);
                gameFieldDrawer.DrawGameField(gameField);
            } while (userKeyPressed != "ESC");
        }

    }
}
