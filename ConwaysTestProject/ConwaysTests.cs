﻿using System;
using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysTestProject
{
    [TestClass]
    public class ConwaysTests
    {
        [TestMethod]
        public void GameConstructor()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3");
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GameConstructorWithCellArray()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", new string[] { "0,0", "1,0" });
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GameConstructorWithPattern()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", "none");
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void InitializedPatternlessGameBoardIsFullOfDeadCells()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", "none");
            bool actualCellValue = game.ToList()[5][4];
            Assert.IsFalse(actualCellValue);
        }

        [TestMethod]
        public void OverloadInitializeHasLiveAndDeadCells()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", new string[] { "0,0", "1,0" });
            Assert.IsTrue(game.ToList()[0][0]);
            Assert.IsTrue(game.ToList()[1][0]);
            Assert.IsFalse(game.ToList()[5][5]);
        }

        [TestMethod]
        public void OneCellDies()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", new string[] { "0,0" });
            Assert.IsTrue(game.ToList()[0][0]);
            game.Tick();
            Assert.IsFalse(game.ToList()[0][0]);
        }

        [TestMethod]
        public void ThreeLiveCellsFlipsDeadCell()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", new string[] { "0,0", "0,2", "1,1" });
            Assert.IsFalse(game.ToList()[0][1]);
            game.Tick();
            Assert.IsTrue(game.ToList()[0][1]);
            game.Tick();
            Assert.IsFalse(game.ToList()[0][1]);
        }

        [TestMethod]
        public void BlockStillLifeAllCellsLive()
        {
            GameOfLife game = new GameOfLife(25, 25, "23/3", new string[] { "0,0", "0,1", "1,0", "1,1" });
            game.Tick();
            Assert.IsTrue(game.ToList()[0][0]);
            Assert.IsTrue(game.ToList()[0][1]);
            Assert.IsTrue(game.ToList()[1][0]);
            Assert.IsTrue(game.ToList()[1][1]);
        }

        [TestMethod]
        public void NoSurvivalBornZeroFlipsWholeBoard()
        {
            GameOfLife game = new GameOfLife(5, 5, "/0", "none");
            Assert.IsFalse(game.ToList()[1][3]);
            game.Tick();
            Assert.IsTrue(game.ToList()[3][2]);
        }

        [TestMethod]
        public void NoSurvivalKillsWholeBoard()
        {
            GameOfLife game = new GameOfLife(5, 5, "/0", "none");
            Assert.IsFalse(game.ToList()[1][3]);
            game.Tick();
            Assert.IsTrue(game.ToList()[3][2]);
            game.Tick();
            Assert.IsFalse(game.ToList()[1][3]);
        }
    }
}
