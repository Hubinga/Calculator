using System.Runtime.CompilerServices;

namespace Calculator.Classes
{
	public class Matrix
	{
		public int ColumnSize { get; private set; }
		public int RowSize { get; private set; }
		public Tuple<int, int> Dimension => new Tuple<int, int>(RowSize, ColumnSize);

		private int[,] matrix;

		//filled matrix
		public Matrix(int[,] matrixNumbers)
		{
			ColumnSize = matrixNumbers.GetLength(0);
			RowSize = matrixNumbers.GetLength(1);
			matrix = matrixNumbers;
		}

		//empty matrix
		public Matrix(int columnSize, int rowSize)
		{
			RowSize = rowSize;
			ColumnSize = columnSize;
			matrix = new int[RowSize, ColumnSize];
		}

		public static Matrix operator+(Matrix a, Matrix b)
		{
			if(a.Dimension != b.Dimension)
			{
				throw new WrongDimesnionException("Beide MAtrizen müssen die selbe Dimension haben");
			}

			int[,] matrixNumbers = new int[a.RowSize, a.ColumnSize];

			for (int i = 0; i < a.RowSize; i++)
			{
				for (int j = 0; j < a.ColumnSize; j++)
				{
					matrixNumbers[i, j] = a.matrix[i, j] + b.matrix[i, j];
				}
			}
			
			return new Matrix(matrixNumbers);
		}
	}
}
