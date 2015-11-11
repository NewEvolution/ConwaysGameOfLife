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
            bool actualCellValue = game.ToList()[5][4];
            Assert.IsFalse(actualCellValue);
        }

        [TestMethod]
        public void OverloadInitializeHasLiveAndDeadCells()
        {
            GameOfLife game = new GameOfLife(25, new string[] { "0,0", "1,0" });
            Assert.IsTrue(game.ToList()[0][0]);
            Assert.IsTrue(game.ToList()[1][0]);
            Assert.IsFalse(game.ToList()[5][5]);
        }

        [TestMethod]
        public void OneCellDies()
        {
            GameOfLife game = new GameOfLife(25, new string[] { "0,0" });
            Assert.IsTrue(game.ToList()[0][0]);
            game.Tick();
            Assert.IsFalse(game.ToList()[0][0]);
        }

        [TestMethod]
        public void ThreeLiveCellsFlipsDeadCell()
        {
            GameOfLife game = new GameOfLife(25, new string[] { "0,0", "0,2", "1,1" });
            Assert.IsFalse(game.ToList()[0][1]);
            game.Tick();
            Assert.IsTrue(game.ToList()[0][1]);
        }
    }
}
