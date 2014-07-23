﻿namespace Labyrinth
{
    using Entities;
    using Entities.Contracts;
    using Loggers.Contracts;
    using Renderer.Contracts;
    using System;
    using UI.Contracts;

    /// <summary>
    /// Class that gives the game objects to different modules, and transfers commands from one class to another, allowing them to be detached.
    /// </summary>
    public class GameEngine
    {
        private IFactory factory;
        private IUserInput input;

        private IConsoleRenderer renderer;

        private ILogger simpleLoggerFileAppender;
        private IGameConsole gameConsole;
        private IGameLogic gameLogic;
        private ILabyrinth labyrinth;
        private IResultsTable resultsTable;

        private IConsoleGraphicFactory consoleGraphicFactory;
        private IScene scene;
        private IRenderable gameConsoleGraphic;
        private IRenderable labyrinthGraphic;
        private IRenderable tableGraphic;

        public GameEngine(IFactory factory)
        {
            this.factory = factory;
            this.input = this.factory.GetUserInputInstance();
            this.labyrinth = this.factory.GetLabyrinthInstance(factory, this.factory.GetMoveHandlerInstance());
            this.gameConsole = new GameConsole(this.factory.GetLanguageStringsInstance());//TODO: use factory
            this.resultsTable = this.factory.GetTopResultsTableInstance();

            this.resultsTable.Table.Changed += (sender, e) =>
            {
                this.factory.GetSerializationManagerInstance().Serialize(sender);
            };
            var fileAppender = this.factory.GetFileAppender("Log.txt");

            this.simpleLoggerFileAppender = this.factory.GetSimpleLogger(fileAppender);

            this.consoleGraphicFactory = this.factory.GetConsoleGraphicFactory();
            this.renderer = this.consoleGraphicFactory.GetConsoleRenderer();
            this.scene = this.factory.GetConsoleScene(this.renderer);

            this.labyrinthGraphic = this.consoleGraphicFactory.GetLabyrinthConsoleGraphic(this.labyrinth,
                this.consoleGraphicFactory.GetCoordinates(0, 1), this.renderer);
            this.gameConsoleGraphic = this.consoleGraphicFactory.GetGameConsoleGraphic(this.gameConsole,
                this.consoleGraphicFactory.GetCoordinates(0, this.labyrinth.LabyrinthSize + 2), this.renderer);
            this.tableGraphic = this.consoleGraphicFactory.GetResultsTableConsoleGraphic(this.resultsTable,
                this.consoleGraphicFactory.GetCoordinates(0, 1), this.renderer);

            this.gameLogic = factory.GetGameLogic(this.labyrinth, this.gameConsole, this.resultsTable, this.input, this.factory);
        }

        public GameEngine()
            : this(new Factory())
        {
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Run()
        {
            this.Init();

            while (!this.gameLogic.Exit)
            {
                this.GameLoop();
            }

            this.ExitApplication();
        }

        private void GameLoop()
        {
            this.gameLogic.Update();
            this.scene.Render();
        }

        private void Init()
        {
            this.resultsTable.Deactivate();
            this.scene.Add(this.labyrinthGraphic);
            this.scene.Add(this.gameConsoleGraphic);
            this.scene.Add(this.tableGraphic);
            this.gameConsole.AddInput("Welcome");
            this.gameConsole.AddInput("Input");
            scene.Render();
        }

        private void ExitApplication()
        {
            //TODO: IMPORTANT the GameEngine must not know that we are using the console! I have commented this, if you have other logic that must be done here, add methods to the IUserInput
            //if (Console.ReadKey(true) != null)
            //{
                Environment.Exit(0);
            //}
        }
    }
}