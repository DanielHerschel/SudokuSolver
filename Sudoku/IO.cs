using System;
using System.IO;
using System.Windows.Forms;

namespace Sudoku
{
    
    /*
     * This class is responsible for the IO:
     * getting the board from the console or from a file,
     * validating the contents of the input and printing or
     * exporting (to a text file) the board.
     */
    public class IOManager
    {

        public static void PrintBoard(SudokuBoard board)
        {

            Console.WriteLine("Board:\n\n" + board.ToString());

        }

        /*
         * Return: a string that represents a Sudoku board.
         * Gives the user a choice to input a board from the console or from a file.
         * Calls GetBoardFromConsole() and GetBoardFromFile() respectively.
         */
        public static string GetBoard()
        {

            string input;

            Console.WriteLine("Enter an option:\nC - input board from the console.\nF - input board from a text file." +
                "\nE - input board from file explorer.\nX - to exit.");
            input = Console.ReadLine();

            while (!input.ToLower().Equals("c") && !input.ToLower().Equals("f") && !input.ToLower().Equals("e") && !input.ToLower().Equals("x"))
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine("Enter an option:\nC - input board from the console.\nF - input board from a text file." +
                    "\nE - input board from file explorer.\nX - to exit.");
                input = Console.ReadLine();
            }

            if (input.ToLower().Equals("c"))
                return GetBoardFromConsole();
            else if (input.ToLower().Equals("f"))
                return GetBoardFromFile();
            else if (input.ToLower().Equals("e"))
                return GetBoardFromFileExplorer();
            else
                return "-1";

        }

        /*
         * Return: a string inputted from the console.
         * Calls ValidateString() to check if the string is valid. If not, gets another input.
         */
        private static string GetBoardFromConsole()
        {

            string board;

            Console.WriteLine("Enter a board (one-line string): (X to exit)");
            board = Console.ReadLine();

            if (board.ToLower().Equals("x")) // Exit if the input is "x" or "X".
                return "-1";

            while (!ValidateString(board)) // Check if the board is a valid string.
            {
                Console.WriteLine("Invalid string. Please try again.");
                Console.WriteLine("Enter a board (one-line string): (X to exit)");
                board = Console.ReadLine();

                if (board.ToLower().Equals("x")) // Exit if the input is "x" or "X".
                    return "-1";
            }

            return board;

        }

        /*
         * Return: a string inputted from a text file.
         * Checks if the file is valid and exists and calls ValidateString() to check if the string is valid.
         * If not, asks for a file again.
         */
        private static string GetBoardFromFile()
        {

            string path = @"", board;

            do
            {
                Console.WriteLine("Enter the path to the text file: (X to exit)");
                path = Console.ReadLine();

                if (path.ToLower().Equals("x")) // Exit if the input is "x" or "X".
                    return "-1";

                if (!File.Exists(path)) // Check if file exists and has permissions/
                {
                    Console.WriteLine("File does not exist. Please try again.");
                    continue;
                }

                if (!Path.GetExtension(path).Equals(".txt")) // Check if the file extension is ".txt".
                {
                    Console.WriteLine("Invalid file extension. Please try again.");
                    continue;
                }

                board = File.ReadAllText(path);

                if (!ValidateString(board)) // Check if the board is a valid string.
                {
                    Console.WriteLine("Invalid string. Please try again.");
                    continue;
                }

                break;
            } while (true);

            return board;

        }

        /*
         * Return: a string inputted from a text file.
         * Opens a dialog window for the user to select a text file.
         * Checks if the file is valid and exists and calls ValidateString() to check if the string is valid.
         * If not, asks for a file again.
         */
        private static string GetBoardFromFileExplorer()
        {

            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false, // Allow picking of only one file.
                Title = "Open Board Text", // Dialog title.
                Filter = "Text|*.txt|All|*.*", // Only allow text files.
            };

            do
            {

                DialogResult result = dialog.ShowDialog(); // shows file dialog , get if the operation succeed

                if (result != DialogResult.OK)
                {
                    Console.WriteLine("Could not open file. Do you want to try again?");
                    string input = Console.ReadLine();

                    while (!input.ToLower().Equals("y") && !input.ToLower().Equals("n"))
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine("Do you want to import file again? Y/N");
                        input = Console.ReadLine();
                    }

                    if (input.ToLower().Equals("n"))
                        break;

                    continue;
                }

                string path = dialog.FileName; //gets file path from the dialog property called 'FileName'
                string board = File.ReadAllText(path); //reads file content to 'input'

                if (ValidateString(board))
                    return board;

                Console.WriteLine("Invalid string. Please try again.");

            } while (true);

            return "-1";

        }

        /*
         * Params: string input
         * Return: true if the string is valid and false if not.
         * A valid string is a string that contains only chars of the ascii table from '0'
         * to '0' + the size of the board.
         * A valid string is also when the sqaure root of the length has also a square root (81 -> 9 -> 3, 16 -> 4 -> 2 etc.);
         */
        public static bool ValidateString(string input)
        {
            float size = MathF.Sqrt(input.Length);
            if (size % 1 != 0) // Check if the square root is a whole number.
                return false;

            if (MathF.Sqrt(size) % 1 != 0) // Check if the square root of the size is a whole number.
                return false;

            char[] temp = input.ToCharArray(); // Convert the string to char array to make it easier to go over it.

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] < '0' || temp[i] > ('0' + (int)size)) {
                    return false;
                }
            }

            return true;

        }

        /*
         * Params string boardString.
         * Opens a window for the user to select a folder to export the Sudoku file to.
         * Exports the string to a text file in a directory given by the user.
         */
        public static void ExportToFile(string boardString)
        {
            
            const string fileName = @"output.txt";
            string input;

            var dialog = new FolderBrowserDialog();

            do
            {

                DialogResult result = dialog.ShowDialog(); // Open dialog window.

                if (result != DialogResult.OK) // Check if not gotten a folder.
                {
                    // Handle the error.
                    Console.WriteLine("Could not select folder. Do you want to try again?");
                    input = Console.ReadLine();

                    while (!input.ToLower().Equals("y") && !input.ToLower().Equals("n"))
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine("Do you want to select a folder again? Y/N");
                        input = Console.ReadLine();
                    }

                    if (input.ToLower().Equals("n"))
                        return;

                    continue;
                }

                string path = Path.Combine(dialog.SelectedPath, fileName); // Path to the new file.

                // If the file exists already, delete it and create a new one.
                if (File.Exists(path)) 
                    File.Delete(path);

                try
                {
                    using (StreamWriter sw = new StreamWriter(path)) // Write into the file.
                    {
                        sw.WriteLine(boardString);
                    }
                    Console.WriteLine("Exported to file: " + path + fileName);
                    return;
                }
                catch (UnauthorizedAccessException e) // No permission to create the file.
                {
                    Console.WriteLine(e.Message);
                }
                catch (PathTooLongException e) // Path longer than system-defined maximum length.
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Exporting was unsuccessful. Do you want to try again? Y/N");
                input = Console.ReadLine();

                while (!input.ToLower().Equals("y") && !input.ToLower().Equals("n"))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.WriteLine("Do you want to try exporting again? Y/N");
                    input = Console.ReadLine();
                }

            } while (true); 

        }

    }

}