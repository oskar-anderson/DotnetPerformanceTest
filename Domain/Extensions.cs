using System.Drawing;
using System.Text;

namespace Domain;

public static class Extensions
{
        public static T Get<T>(this T[,] board, Point p)
        {
            return board[p.Y, p.X];
        }

        public static void Set<T>(this T[,] board, Point p, T value)
        {
            board[p.Y, p.X] = value;
        }
        
        public static void Fill<T>(this T[,] board, T value)
        {
            for (int y = 0; y < board.GetHeight(); y++)
            {
                for (int x = 0; x < board.GetWidth(); x++)
                {
                    board.Set(new Point(x, y), value);
                }
            }
        }
        
        public static void SetBoardByBounds<T>(this Rectangle board, T[,] original, T[,] setValues)
        {
            if (original.GetHeight() < setValues.GetHeight() 
                || original.GetWidth() < setValues.GetWidth()
                || board.Height != setValues.GetHeight()
                || board.Width != setValues.GetWidth())
            {
                throw new Exception("Sizes do not match!");
            }
            for (int y = board.Top; y < board.Bottom; y++)
            {
                for (int x = board.Left; x < board.Right; x++)
                {
                    Point setValuePoint = new Point(x - board.Left, y - board.Top);
                    T newValue = setValues.Get(setValuePoint);
                    original.Set(new Point(x, y), newValue);
                }
            }
        }
        
        public static T Get<T>(this T[][] board, Point p)
        {
            return board[p.Y][p.X];
        }

        public static void Set<T>(this T[][] board, Point p, T value)
        {
            board[p.Y][p.X] = value;
        }

        public static int GetWidth<T>(this T[,] board)
        {
            return board.GetLength(1);
        }
        
        public static int GetHeight<T>(this T[,] board)
        {
            return board.GetLength(0);
        }
        
        public static string ToString2D<T>(this T[,] board)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    sb.Append(board[i, j]);
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }
        
        // https://stackoverflow.com/questions/3010219/jagged-arrays-multidimensional-arrays-conversion-in-asp-net
        public static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
        
        public static T[,] To2DArray<T>(this T[][] jaggedArray, int numOfColumns, int numOfRows)
        {
            T[,] temp2DArray = new T[numOfColumns, numOfRows];
            
            for (int c = 0; c < numOfColumns; c++)
            {
                for (int r = 0; r < numOfRows; r++)
                {
                    temp2DArray[c, r] = jaggedArray[c][r];
                }
            }

            return temp2DArray;
        }
}