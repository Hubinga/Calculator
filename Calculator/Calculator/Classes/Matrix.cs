using System.Runtime.CompilerServices;

namespace Calculator.Classes
{
	public class Matrix
	{
		public int ColumnSize { get; private set; }
		public int RowSize { get; private set; }
		public Tuple<int, int> Dimension => new Tuple<int, int>(RowSize, ColumnSize);

		public int[,] MatrixBoard { get; private set; }

		//filled matrix
		public Matrix(int[,] matrixNumbers)
		{
			RowSize = matrixNumbers.GetLength(0);
			ColumnSize = matrixNumbers.GetLength(1);
			MatrixBoard = matrixNumbers;
		}

		//empty matrix
		public Matrix(int columnSize, int rowSize)
		{
			RowSize = rowSize;
			ColumnSize = columnSize;
			MatrixBoard = new int[RowSize, ColumnSize];
		}

		public static Matrix operator+(Matrix a, Matrix b)
		{
			if(a.RowSize != b.RowSize || a.ColumnSize != b.ColumnSize)
			{
				throw new WrongDimesnionException("Both matrices must have the same dimension!");
			}

			int[,] result = new int[a.RowSize, a.ColumnSize];

			for (int i = 0; i < a.RowSize; i++)
			{
				for (int j = 0; j < a.ColumnSize; j++)
				{
					result[i, j] = a.MatrixBoard[i, j] + b.MatrixBoard[i, j];
				}
			}
			
			return new Matrix(result);
		}

		public static Matrix operator *(Matrix a, Matrix b)
		{
			//e.g. (2,3) (3, 2)
			//only defined, if column amount of A is equal row amount of B
			if (a.ColumnSize != b.RowSize)
			{
				throw new WrongDimesnionException("Only defined, if column amount of A is equal row amount of B!");
			}

			//(m, n) * (n, r) => (m, r)
			int[,] result = new int[a.RowSize, b.ColumnSize];

			for (int i = 0; i < result.GetLength(0); i++)
			{
				for (int j = 0; j < result.GetLength(1); j++)
				{
					int currentElement = 0;

					//k: from 0 to n - 1 (n = columnsize)
					for (int k = 0; k < a.ColumnSize; k++)
					{
						currentElement += a.MatrixBoard[i, k] * b.MatrixBoard[k, j];
					}

					result[i, j] = currentElement;
				}
			}

			return new Matrix(result);
		}

		public void ChangeDimension(int rowSize, int collumnSize)
		{
			MatrixBoard = new int[rowSize, collumnSize];
			RowSize = rowSize;
			ColumnSize = collumnSize;
		}

		public void Reset(int rowSize = 3, int collumnSize = 3)
		{
			MatrixBoard = new int[rowSize, collumnSize];
			RowSize = rowSize; 
			ColumnSize = collumnSize;
		}

		public void Print()
		{
			for (int i = 0; i < RowSize; i++)
			{
				for (int j = 0; j < ColumnSize; j++)
				{
					Console.Write(MatrixBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
		}
	}
}
