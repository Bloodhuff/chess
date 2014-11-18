using System;
using chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var board = Board.Instance;
            var test = Board.Instance.BackRowList;
            Assert.Equals(test[2],board.MyBoarderArray[7,0]);
        }
    }
}
