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
        public void CellCanChangeState()
        {
            Cell cell = new Cell();
            Assert.IsFalse(cell.Living);
            cell.switchState();
            Assert.IsTrue(cell.Living);
        }
    }
}
