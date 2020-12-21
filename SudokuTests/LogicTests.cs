using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuTests
{
    /*
     * Test class responsible for testing
     * the algorithm's logic with normal and
     * extreme cases
     */
    [TestClass]
    public class LogicTests
    {

        // Test the SudokuBoard.Validate() function.
        [TestMethod]
        public void TestValidate()
        {

            // Arrange
            string validString = "000075400000000008080190000300001060000000034000068170204000603900000020530200000";
            SudokuBoard validBoard = new SudokuBoard(validString);

            // Act
            bool res1 = validBoard.Validate();

            // Assert
            Assert.AreEqual(true, res1);

        }

        // Test the SudokuBoard.Solve() method on a 4x4 board.
        [TestMethod]
        public void TestSolve4x4()
        {

            // Arrange
            string s = "1000000400200300";
            SudokuBoard board = new SudokuBoard(s);

            // Act
            bool res = board.Solve();

            // Assert
            IOManager.PrintBoard(board);
            Assert.AreEqual(true, res);

        }

        // Test the SudokuBoard.Solve() method on a 9x9 board.
        [TestMethod]
        public void TestSolve9x9()
        {

            // Arrange
            string s = "000075400000000008080190000300001060000000034000068170204000603900000020530200000";
            SudokuBoard board = new SudokuBoard(s);

            // Act
            bool res = board.Solve();

            // Assert
            Assert.AreEqual(true, res);
            IOManager.PrintBoard(board);

        }

        // Test the SudokuBoard.Solve() method on a 16x16 board.
        [TestMethod]
        public void TestSolve16x16()
        {

            // Arrange
            string s = "5000000000000002000000:0>00000000080000000000000000000000000600000=000000000020000000000000000?0;0000000000000000003000000@8000000000000000008000000000>0000000000000000000007000200000003000000000000000000008030000000000000000000000000000040000?0@=000000000";
            SudokuBoard board = new SudokuBoard(s);

            // Act
            bool res = board.Solve();

            // Assert
            IOManager.PrintBoard(board);
            Assert.AreEqual(true, res);

        }

        /* ================================= Extreme Cases Tests ================================= */

        // Tests how the algorithm acts when given an invalid board with a duplicate in one of the rows.
        [TestMethod]
        public void TestDuplicateGiven()
        {

            // Arrange
            string invalidString = "000077540000000000808019000030000106000000003400006817020400060390000002053020000";
            // -------------------------^^ this is where the board is invalid.
            SudokuBoard invBoard = new SudokuBoard(invalidString);

            // Act
            bool res = invBoard.Solve();

            // Assert
            Assert.AreEqual(false, res);

        }

        //  =========== Test empty boards  ===========

        // Test an empty 4x4 board.
        [TestMethod]
        public void TestEmpty4x4Board()
        {

            // Arrange
            string s = new string('0', 16);
            SudokuBoard board = new SudokuBoard(s);

            // Act
            bool res = board.Solve();

            // Assert
            IOManager.PrintBoard(board);
            Assert.AreEqual(true, res);

        }

        // Test an empty 9x9 board.
        [TestMethod]
        public void TestEmpty9x9Board()
        {

            // Arrange
            string s = new string('0', 81);
            SudokuBoard board = new SudokuBoard(s);

            // Act
            bool res = board.Solve();

            // Assert
            IOManager.PrintBoard(board);
            Assert.AreEqual(true, res);

        }

        // Test an empty 16x16 board.
        [TestMethod]
        public void TestEmpty16x16Board()
        {

            // Arrange
            string s = new string('0', 256);
            SudokuBoard board = new SudokuBoard(s);

            // Act
            bool res = board.Solve();

            // Assert
            IOManager.PrintBoard(board);
            Assert.AreEqual(true, res);

        }

        // =========== Test unsovable boards  ===========
        // An unsolvable board is when a square, box, row or column has no possible candidates
        // for a number or any number.

        // Test an unsolvable 4x4 board.
        [TestMethod]
        public void TestUnsovable4x4()
        {

            // Arrange
            string invalidString = "1210002001000002";
            SudokuBoard invBoard = new SudokuBoard(invalidString);

            // Act
            bool res = invBoard.Solve();

            // Assert
            Assert.AreEqual(false, res);

        }

        // Test an unsolvable 9x9 board.
        [TestMethod]
        public void TestUnsovable9x9()
        {

            // Arrange
            string invalidString = "516849732307605000809700065135060907472591006968370050253186074684207500791050608";
            SudokuBoard invBoard = new SudokuBoard(invalidString);

            // Act
            bool res = invBoard.Solve();

            // Assert
            Assert.AreEqual(false, res);

        }

        // Test an unsolvable 16x16 board.
        [TestMethod]
        public void TestUnsovable16x16()
        {

            // Arrange
            string invalidString = "6937:1;@4>2?8<5=52@>=763<9;81:4?:;=?248<57@13>69<81459>?=36:@72;96?21;<8>:=573@48<5;9632?@74010074:@>=?5201;900<13>07:@4009<?2;0?0204>7=0109;@08300068007=4@00000071@<000;8050020@083510:2?>64<70?05800000020=002180;0=70?0000>00063<@00900=0000@=4:?29>;537<681";
            SudokuBoard invBoard = new SudokuBoard(invalidString);

            // Act
            bool res = invBoard.Solve();

            // Assert
            Assert.AreEqual(false, res);

        }

    }
}
