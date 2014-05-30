﻿// ********************************
// <copyright file="ResultsList.cs" company="Telerik Academy">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a table with the top results
    /// </summary>
    public class ResultsList
    {
        /// <summary>
        /// Maximum count of top results in the table.
        /// </summary>
        private const int MaxCount = 5;

        /// <summary>
        /// Holds the sorted list of top results.
        /// </summary>
        private List<Result> topResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsList"/> class.
        /// </summary>
        public ResultsList()
        {
            this.topResults = new List<Result>();
            this.topResults.Capacity = ResultsList.MaxCount;
        }

        /// <summary>
        /// Checks if a given amount of moves is good enough to enter the results table.
        /// </summary>
        /// <param name="currentMoves">Integer value representing the amount of moves.</param>
        /// <returns>True if a result is good enough and false if the result is not good enough to enter the results table.</returns>
        public bool IsTopResult(int currentMoves)
        {
            if (this.topResults.Count < ResultsList.MaxCount)
            {
                return true;
            }

            if (currentMoves < this.topResults.Max().MovesCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a new result formed form specified moves and player name in the results table.
        /// </summary>
        /// <param name="movesCount">Moves to be added.</param>
        /// <param name="playerName">Player name to be added.</param>
        public void AddResult(int movesCount, string playerName)
        {
            Result result = new Result(movesCount, playerName);
            if (this.topResults.Count == this.topResults.Capacity)
            {
                this.topResults[this.topResults.Count - 1] = result;
            }
            else
            {
                this.topResults.Add(result);
            }

            this.topResults.Sort();
        }

        /// <summary>
        /// Converts the result table into string.
        /// </summary>
        /// <returns>String representing the converted results table.</returns>
        public override string ToString()
        {
            var output = new List<string>();
            if (this.topResults.Count == 0)
            {
                output.Add(UserInputAndOutput.SCOREBOARD_EMPTY_MSG);
            }
            else
            {
                for (int i = 0; i < this.topResults.Count; i++)
                {
                    output.Add(string.Format("{0}. {1} --> {2} moves{3}", i + 1, this.topResults[i].PlayerName, this.topResults[i].MovesCount));
                }
            }

            return string.Join(Environment.NewLine, output);
        }
    }
}
