using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SudokuTests
{
    /*
     * Test class responsible for testing
     * the input and output part of the project.
     */
    [TestClass]
    public class IOTests
    {

        // Test printing out a 4x4 board.
        [TestMethod]
        public void TestPrint4x4()
        {

            // Arrange
            string s = "020400002040"; // String with length of 16.
            SudokuBoard b = new SudokuBoard(s);

            // Act + Assert
            IOManager.PrintBoard(b);
            // When looking at the output file, copy all and paste to notepad. The view is glitched.

        }

        // Test printing out a 9x9 board.
        [TestMethod]
        public void TestPrint9x9()
        {

            // Arrange
            string s = "123456" + new string('0', 75); // String with length of 81.
            SudokuBoard b = new SudokuBoard(s);

            // Act + Assert
            IOManager.PrintBoard(b);
            // When looking at the output file, copy all and paste to notepad. The view is glitched.

        }

        // Test printing out a 16x16 board.
        [TestMethod]
        public void TestPrint16x16()
        {

            // Arrange
            string s = "123456789" + new string('0', 247); // String with length of 256.
            SudokuBoard b = new SudokuBoard(s);

            // Act + Assert
            IOManager.PrintBoard(b);
            // When looking at the output file, copy all and paste to notepad. The view is glitched.

        }

        /*
         * Test the ValidateString() method in IOManager.
         * Test with multiple invalid strings and a valid one.
         */
        [TestMethod]
        public void TestValidateString()
        {

            // Arrange
            string invalidLength = new string('0', 13), invalidCharacters = "asfn" + new string('0', 12); // Invalid strings
            string validString = "1234" + new string('0', 12); // Valid string

            // Act
            bool res1 = IOManager.ValidateString(invalidLength);
            bool res2 = IOManager.ValidateString(invalidCharacters);

            bool res3 = IOManager.ValidateString(validString);

            // Assert
            Assert.AreEqual(false, res1);
            Assert.AreEqual(false, res2);
            Assert.AreEqual(true, res3);

        }

        // Test exporting to file on the Desktop.
        [TestMethod]
        public void TestExportToFile()
        {

            // Arrange
            string s = "1000000400200300";
            SudokuBoard board = new SudokuBoard(s);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "TestOutput.txt";

            // Act
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(board.ToOneLineString());
            }

            // Assert
            Debug.WriteLine("Path to file: " + path); // To see this, run in Debug mode.
            bool exists = File.Exists(path);
            Assert.AreEqual(true, exists);

        }

    }
}
