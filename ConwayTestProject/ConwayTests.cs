using System;
using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ConwayTestProject
{
    [TestClass]
    public class ConwayTests
    {
        [TestMethod]
        public void ConstructorBuildsArray()
        {
            GameOfLife game = new GameOfLife(1, false, new List<List<int>> { });
            bool expected = false;
            bool actual = game.cellStatus(0, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConstructorBuildsLargerArray()
        {
            GameOfLife game = new GameOfLife(20, false, new List<List<int>> { });
            bool expected = false;
            bool actual = game.cellStatus(15, 7);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeadCellsCanBeRevived()
        {
            GameOfLife game = new GameOfLife(20, false, new List<List<int>> { });
            bool preFlip = game.cellStatus(15, 7);
            game.changeStatus(15, 7);
            bool postFlip = game.cellStatus(15, 7);
            Assert.AreNotEqual(preFlip, postFlip);
        }

        [TestMethod]
        public void CanCheckNumberOfLiveNeighbors()
        {
            GameOfLife game = new GameOfLife(20, false, new List<List<int>> { });
            int actual = game.checkNeighbors(5, 5);
            int expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanCheckLargerNumberOfLiveNeighbors()
        {
            List<int> cell1 = new List<int> { 5, 4 };
            List<int> cell2 = new List<int> { 4, 4 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2};
            GameOfLife game = new GameOfLife(20, false, liveCells);
            int actual = game.checkNeighbors(5, 5);
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanCheckNumberOfEdgeNeighbors()
        {
            GameOfLife game = new GameOfLife(20, false, new List<List<int>> { });
            int actual = game.checkNeighbors(0, 0);
            int expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanCheckLargerNumberOfEdgeNeighbors()
        {
            List<int> cell1 = new List<int> { 0, 1 };
            List<int> cell2 = new List<int> { 1, 0 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2 };
            GameOfLife game = new GameOfLife(20, false, liveCells);
            int actual = game.checkNeighbors(0, 0);
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanCheckLargerNumberOfOppositeEdgeNeighbors()
        {
            List<int> cell1 = new List<int> { 5, 4 };
            List<int> cell2 = new List<int> { 4, 5 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2 };
            GameOfLife game = new GameOfLife(6, false, liveCells);
            int actual = game.checkNeighbors(5, 5);
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TickOnSingleLiveCellKillsCell()
        {
            List<int> cell1 = new List<int> { 0, 0 };
            List<List<int>> liveCells = new List<List<int>> { cell1 };
            GameOfLife game = new GameOfLife(6, false, liveCells);
            game.Tick();
            bool actual = game.cellStatus(0, 0);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TickOnTwoSeperateLiveCellKillsCell()
        {
            List<int> cell1 = new List<int> { 0, 0 };
            List<int> cell2 = new List<int> { 1, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2 };
            GameOfLife game = new GameOfLife(6, false, liveCells);
            game.Tick();
            bool actual = game.cellStatus(0, 0);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TickOnFourLiveCellsStayAlive()
        {
            List<int> cell1 = new List<int> { 0, 0 };
            List<int> cell2 = new List<int> { 0, 1 };
            List<int> cell3 = new List<int> { 1, 0 };
            List<int> cell4 = new List<int> { 1, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2, cell3, cell4 };
            GameOfLife game = new GameOfLife(6, false,  liveCells);
            game.Tick();
            bool actual = game.cellStatus(0, 0);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipperFlips()
        {
            List<int> cell1 = new List<int> { 0, 1 };
            List<int> cell2 = new List<int> { 1, 1 };
            List<int> cell3 = new List<int> { 2, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2, cell3 };
            GameOfLife game = new GameOfLife(6, false, liveCells);
            game.Tick();
            bool actual = game.cellStatus(1, 0);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipperFlipsOff()
        {
            List<int> cell1 = new List<int> { 0, 1 };
            List<int> cell2 = new List<int> { 1, 1 };
            List<int> cell3 = new List<int> { 2, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2, cell3 };
            GameOfLife game = new GameOfLife(6, false, liveCells);
            game.Tick();
            bool actual = game.cellStatus(0, 1);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipperFlipsTwice()
        {
            List<int> cell1 = new List<int> { 0, 1 };
            List<int> cell2 = new List<int> { 1, 1 };
            List<int> cell3 = new List<int> { 2, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1, cell2, cell3 };
            GameOfLife game = new GameOfLife(6, false, liveCells);
            game.Tick();
            game.Tick();
            bool actual = game.cellStatus(0, 1);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToListReturnsItems()
        {
            List<int> cell1 = new List<int> { 1, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1 };
            GameOfLife game = new GameOfLife(3, false, liveCells);
            Assert.AreEqual(true, game.ToList()[1][1]);
        }

        [TestMethod]
        public void ToListReturnsFullList()
        {
            List<int> cell1 = new List<int> { 1, 1 };
            List<List<int>> liveCells = new List<List<int>> { cell1 };
            GameOfLife game = new GameOfLife(3, false, liveCells);
            List<bool> row1 = new List<bool> { false, false, false };
            List<bool> row2 = new List<bool> { false, true, false };
            List<bool> row3 = new List<bool> { false, false, false };
            CollectionAssert.AreEqual(row1, game.ToList()[0]);
            CollectionAssert.AreEqual(row2, game.ToList()[1]);
            CollectionAssert.AreEqual(row3, game.ToList()[2]);
        }
    }
}
