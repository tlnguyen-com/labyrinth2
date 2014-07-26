﻿namespace Labyrinth.Renderer
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using Labyrinth.Commons;

    public class LanguageStrings : ILanguageStrings
    {
        private readonly Dictionary<Dialog, string> dialogList;

        public LanguageStrings()
        {
            this.dialogList = new Dictionary<Dialog, string>();

            this.dialogList.Add(Dialog.InvalidMove, "Invalid move!");
            this.dialogList.Add(Dialog.NameAdded, "Name was added to the score board, new game started");
            this.dialogList.Add(Dialog.InvalidCommand, "Invalid command!");
            this.dialogList.Add(Dialog.EnterName, "Please enter your name for the top scoreboard:");
            this.dialogList.Add(Dialog.GoodBye, "Good Bye" + "\n" + "Press any key to exit..." + "\n");
            this.dialogList.Add(Dialog.Input, "Enter your move (A-left, D-right, W-up, S-down)");
            this.dialogList.Add(Dialog.WinMessage, "Congratulations! You escaped in {0} moves.");
            this.dialogList.Add(Dialog.AllWrong, "If this happens something is very wrong with the logic of PrintLabyrinth method");
            this.dialogList.Add(Dialog.Welcome, "Welcome to “Labyrinth” game. Please try to escape. Use 't' to toogle between the top scoreboard menu and game, 'r' to start a new game and 'e' to quit the game.");
        }

        /// <summary>
        /// Get a diaglog from the dialogList
        /// </summary>
        /// <param name="key">The dialog key that you want to get</param>
        /// <returns>The wanted dialog</returns>
        public string GetDialog(Dialog key)
        {
            if (this.dialogList.ContainsKey(key))
            {
                return this.dialogList[key];
            }
            else
            {
                throw new ArgumentOutOfRangeException("there's no such key in dialogList");
            }
        }
    }
}