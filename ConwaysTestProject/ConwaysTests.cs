using System;
using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysTestProject
{
    [TestClass]
    public class ConwaysTests
    {
        [TestMethod]
        public void GameConstructorMakesGameInstance()
        {
            GameOfLife game = new GameOfLife(25, false);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GameConstructorOverload()
        {
            GameOfLife game = new GameOfLife(25, new string[] { "0,0", "1,0" });
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void InitializedGameBoardIsFullOfDeadCells()
        {
            GameOfLife game = new GameOfLife(25, false);
            bool actualCellValue = game.cellAt(24, 24).Living;
            Assert.IsFalse(actualCellValue);
        }

        [TestMethod]
        public void OverloadInitializeHasLiveCells()
        {
            GameOfLife game = new GameOfLife(25, new string[] { "0,0", "1,0" });
            Assert.IsTrue(game.cellAt(0, 0).Living);
            Assert.IsTrue(game.cellAt(1, 0).Living);
        }

        [TestMethod]
        public void GameCanChangeArbitraryCellState()
        {
            GameOfLife game = new GameOfLife(25, false);
            game.switchState(3, 20);
            game.switchState(15, 3);
            Assert.IsTrue(game.cellAt(3, 20).Living);
            Assert.IsTrue(game.cellAt(15, 3).Living);
            Assert.IsFalse(game.cellAt(3, 2).Living);
        }

        [TestMethod]
        public void CellsTellNeighborsTheyAreAlive()
        {
            GameOfLife game = new GameOfLife(25, false);
            game.switchState(0, 0);
            game.check();
            Assert.AreEqual(1, game.cellAt(0, 1).LiveNeighbors);
        }

        [TestMethod]
        public void MoreCellsTellNeighborsTheyAreAlive()
        {
            GameOfLife game = new GameOfLife(25, false);
            game.switchState(0, 0);
            game.switchState(0, 2);
            game.switchState(1, 1);
            game.check();
            Assert.AreEqual(3, game.cellAt(0, 1).LiveNeighbors);
        }

        [TestMethod]
        public void OneCellDies()
        {
            GameOfLife game = new GameOfLife(25, false);
            game.switchState(0, 0);
            Assert.IsTrue(game.cellAt(0, 0).Living);
            game.Tick();
            Assert.IsFalse(game.cellAt(0, 0).Living);
        }

        [TestMethod]
        public void ThreeLiveCellsFlipsDeadCell()
        {
            GameOfLife game = new GameOfLife(25, false);
            game.switchState(0, 0);
            game.switchState(0, 2);
            game.switchState(1, 1);
            Assert.IsFalse(game.cellAt(0, 1).Living);
            game.Tick();
            Assert.IsTrue(game.cellAt(0, 1).Living);
        }
    }
}
