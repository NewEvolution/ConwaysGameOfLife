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
            GameOfLife game = new GameOfLife(25);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void InitializedGameBoardIsFullOfDeadCells()
        {
            GameOfLife game = new GameOfLife(25);
            bool expectedCellValue = false;
            bool actualCellValue = game.cellAt(24, 24).Living;
            Assert.AreEqual(expectedCellValue, actualCellValue);
        }

        [TestMethod]
        public void GameCanChangeArbitraryCellState()
        {
            GameOfLife game = new GameOfLife(25);
            game.cellAt(3, 20).switchState();
            game.cellAt(15, 3).switchState();
            Assert.IsTrue(game.cellAt(3, 20).Living);
            Assert.IsTrue(game.cellAt(15, 3).Living);
            Assert.IsFalse(game.cellAt(3, 2).Living);
        }
    }
}
