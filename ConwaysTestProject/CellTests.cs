using System;
using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysTestProject
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CellConstructorMakesCellInstance()
        {
            Cell cell = new Cell();
            Assert.IsNotNull(cell);
        }

        [TestMethod]
        public void CellTellsItsState()
        {
            Cell cell = new Cell();
            Assert.IsFalse(cell.Living);
        }

        [TestMethod]
        public void CellTellsItsLivingNeighbors()
        {
            Cell cell = new Cell();
            Assert.AreEqual(0, cell.LiveNeighbors);
        }
    }
}
