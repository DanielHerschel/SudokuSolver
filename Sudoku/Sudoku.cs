using System;

namespace Sudoku
{

    /*
     * This class is a Sudoku board object.
     * It has two types of methods:
     * - Logic: Methods for validating and solving the Sudoku.
     * - Miscellaneous methods: Constructor, ToString(), etc.
     */
    public class SudokuBoard
    {

        // Sudoku board properties
        int[,] board;
        int size;

        // Getters/Setters
        public int[,] Board
        {
            get { return board; }
            set { return; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        /* ======================================= Sudoku Solving Methods ======================================= */

    /* 
     * Return: true if the Sudoku is valid
     * Checks if each number appears once in every row, column and square.
     */
    public bool Validate()
        {

            for (int i = 0; i < Size; i++)
            {
                bool[] rowFlag = new bool[Size + 1];
                bool[] colFlag = new bool[Size + 1];

                for (int j = 0; j < Size; j++)
                {
                    if (rowFlag[board[i,j]] && board[i, j] > 0) // Check if a number exists in a row already.
                    {
                        return false;
                    }
                    rowFlag[board[i,j]] = true; // If not, turn it's flag on.

                    if (colFlag[board[j,i]] && board[j, i] > 0) // Check if a number exists in a column already.
                    {
                        return false;
                    }
                    colFlag[board[j,i]] = true; // If not, turn it's flag on.

                    if ((i + (int)MathF.Sqrt(Size)) % (int)MathF.Sqrt(Size) == 0 && (j + (int)MathF.Sqrt(Size)) % (int)MathF.Sqrt(Size) == 0)
                        // Check if entered a new square.
                    {

                        bool[] squareFlag = new bool[Size + 1];

                        // Go over the square.
                        for (int k = i; k < i + (int)MathF.Sqrt(Size); k++)
                        {
                            for (int l = j; l < j + (int)MathF.Sqrt(Size); l++)
                            {
                                if (squareFlag[board[k,l]] && board[k,l] > 0) // Check if a number exists in a square already.
                                {
                                    return false;
                                }
                                squareFlag[board[k,l]] = true; // If not, turn it's flag on.
                            }
                        }
                    }

                }
            }

            return true;

        }

        /* 
         * Params: int row, int col.
         * Return: true if could solve the Sudoku and false if not.
         * Using a recursive backtracking algorithm to assign values
         * to the unassigned boxed in an attempt to solve
         * any board with a whole number as the square root
         * of the size (4x4, 9x9, 16x16 etc.).
         */
        public bool Solve()
        {

            int row = -1;
            int col = -1;
            bool isEmpty = true; // To break out of loops.

            // Go over the board to find the first empty box.
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (board[i,j] == 0) // Check if the box is empty.
                    {
                        row = i;
                        col = j;

                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            if (isEmpty) // Check if there is no empty space left.
            {
                return true;
            }

            // else for each-row backtrack
            for (int num = 1; num <= Size; num++)
            {
                if (SafeToPlace(row, col, num))
                {
                    board[row,col] = num;
                    if (Solve()) // If the next iteration was successful
                        return true;
                    else
                        board[row, col] = 0; // Reset the box
                }
            }

            return false;

        }

        /*
         * Params: int row, int col, int num
         * Return: true if it's safe to place the number and false if not.
         * A safe place is a place where the number doesn't already exist in the row, column and square.
         */
        bool SafeToPlace(int row, int col, int num)
        {

            // Check if we find the same number in the row
            // Return false if found.
            for (int i = 0; i <= Size - 1; i++)
                if (board[row,i] == num)
                    return false;

            // Check if we find the same number in the column
            // Return false if found.
            for (int i = 0; i <= Size - 1; i++)
                if (board[i,col] == num)
                    return false;

            // Check if we find the same number in the square
            // Return false if found.
            int boxSize = (int)MathF.Sqrt(Size);
            int startRow = row - row % boxSize, 
                startCol = col - col % boxSize;

            // Go over the square and check.
            for (int i = 0; i < boxSize; i++)
                for (int j = 0; j < boxSize; j++)
                    if (board[i + startRow,j + startCol] == num)
                        return false;

            return true;

        }

        /* ======================================= Misc Methods ======================================= */

        /*
         * Constructor.
         * Params: string boardString
         * Creates a 2D array that represents a Sudoku board from a one-line string
         */
        public SudokuBoard(string boardString)
        {

            Size = (int)Math.Sqrt(boardString.Length);

            board = new int[Size,Size];

            for (int i = 0; i < Size; i++)
            {
                char[] tempRow = (boardString.Substring(i * Size, Size)).ToCharArray();

                for (int j = 0; j < tempRow.Length; j++)
                {
                    board[i,j] = tempRow[j] - '0';
                }
            }

        }

        // ToString() override
        // Formats the board for printing out
        public override string ToString()
        {

            string output = new string('-', ((Size * ((Size + "").Length + 3)) + 1)) + "\n";

            for (int i = 0; i < Size; i++)
            {
                output += "| ";

                for (int j = 0; j < Size; j++)
                {
                    if (board[i,j] == 0)
                        output += new string(' ', (Size + "").Length) + " | ";
                    else
                        output += board[i,j] + new string(' ', (Size + "").Length - (board[i,j] + "").Length) + " | ";
                }

                output = output + "\n" + new string('-', ((Size * ((Size + "").Length + 3)) + 1)) + "\n";
            }

            return output;

        }

        /*
         * Return: a string of one line that represents the Sudoku board.
         * Goes over the board and adds the values to the string.
         */
        public string ToOneLineString()
        {

            string output = "";

            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    output += board[i, j] + '0';

            return output;

        }

    }

}