/*
 _____           _       _            _____       _                
/  ___|         | |     | |          /  ___|     | |               
\ `--. _   _  __| | ___ | | ___   _  \ `--.  ___ | |_   _____ _ __ 
 `--. \ | | |/ _` |/ _ \| |/ / | | |  `--. \/ _ \| \ \ / / _ \ '__|
/\__/ / |_| | (_| | (_) |   <| |_| | /\__/ / (_) | |\ V /  __/ |   
\____/ \__,_|\__,_|\___/|_|\_\\__,_| \____/ \___/|_| \_/ \___|_|   

by Daniel Herschel
December 2020

-------------------------------------
| 3 |   |   |   |   |   |   |   |   |
-------------------------------------
|   | 5 |   | 7 |   | 3 |   |   | 8 |
-------------------------------------
|   |   |   |   | 2 | 8 |   | 7 |   |
-------------------------------------
| 7 |   |   |   |   |   |   | 4 | 3 |
-------------------------------------
|   |   |   |   |   |   |   |   |   |
-------------------------------------
|   |   | 3 | 9 |   | 4 | 1 |   | 5 |
-------------------------------------
| 4 |   |   | 3 |   |   | 8 |   |   |
-------------------------------------
| 1 |   |   |   | 4 |   |   |   |   |
-------------------------------------
| 9 | 6 | 8 |   |   |   | 2 |   |   |
-------------------------------------

 */

using System;
using System.Diagnostics;

namespace Sudoku
{

    /*
     * Main class.
     * Connects everything together to
     * a user friendly sudoku solver.
     */
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {

            const string LOGO = @"
             _____           _       _             _____       _                
            /  ___|         | |     | |           /  ___|     | |               
            \ `--. _   _  __| | ___ | | ___   _   \ `--.  ___ | |_   _____ _ __ 
             `--. \ | | |/ _` |/ _ \| |/ / | | |   `--. \/ _ \| \ \ / / _ \ '__|
            /\__/ / |_| | (_| | (_) |   <| |_| |  /\__/ / (_) | |\ V /  __/ |   
            \____/ \__,_|\__,_|\___/|_|\_\\__,_|  \____/ \___/|_| \_/ \___|_|   
            
                                       by Daniel Herschel
                                           December 2020
                                        ";

            Console.WriteLine(LOGO);

            string input;

            while (true)
            {

                string boardString = IOManager.GetBoard();

                if (boardString.Equals("-1"))
                {
                    Console.WriteLine("Did not get a board. Exiting...");
                    break;
                }   

                SudokuBoard board = new SudokuBoard(boardString);

                if (board.Validate()) // Check if board is a valid Sudoku board
                {

                    if (board.Size <= 16) // Check if the size is smaller that 16x16 
                    {
                        // Solving the board.
                        IOManager.PrintBoard(board);
                        Console.WriteLine("Press any key to solve the board.");
                        _ = Console.ReadKey();
                        Console.WriteLine("\nSolving the board...");

                        // Create a stopwatch and start it.
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        if (board.Solve()) // Try to solve the Sudoku.
                        {
                            // Check time elapsed from the start of solving.
                            stopwatch.Stop();
                            TimeSpan elapsed = stopwatch.Elapsed;

                            // Print the board and time solved.
                            IOManager.PrintBoard(board);
                            Console.WriteLine("Board as one-line string: ");
                            Console.WriteLine(board.ToOneLineString());
                            Console.WriteLine("Solved the board in " + elapsed.TotalMilliseconds + " ms.");

                            // Export the board to text file if the user wants to.
                            Console.WriteLine("\nDo you want to export the board into a text file? Y/N");
                            input = Console.ReadLine();

                            while (!input.ToLower().Equals("y") && !input.ToLower().Equals("n"))
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                                Console.WriteLine("Do you want to export the board into a text file? Y/N");
                                input = Console.ReadLine();
                            }

                            if (input.ToLower().Equals("y"))
                                IOManager.ExportToFile(board.ToOneLineString());

                        }
                        else // If couldn't solve the Sudoku.
                        {
                            stopwatch.Stop(); // Stop the stopwatch.
                            Console.WriteLine("Could not solve the Sudoku.");
                        }
                    } else // If the board is greater than 16x16.
                    {
                        Console.WriteLine("Board is too big (max 16x16).");
                    }

                } else // If the board is not a valid Sudoku board
                {
                    Console.WriteLine("Invalid Sudoku board.");
                }

                Console.WriteLine("\nDo you want to enter another board? Y/N");
                input = Console.ReadLine();

                while (!input.ToLower().Equals("y") && !input.ToLower().Equals("n"))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.WriteLine("Do you want to enter another board? Y/N");
                    input = Console.ReadLine();
                }

                if (input.ToLower().Equals("n"))
                    break;

            } // End while

        } // End main

    }

}