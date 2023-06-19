namespace Calculator.Classes
{
	public class MatrixHelper
	{
		/// <summary>
		/// Solve system of equations (Ax = b)
		/// </summary>
		/// <param name="matrix">matrix A</param>
		/// <param name="vektor">vector b or matrix I to claculate inverse Matrix A</param>
		public void GaußJordan(Matrix matrix, Matrix vector)
		{
			//start from first row (eliminate x1)
			int rowIdx = 0;

			while(rowIdx < matrix.RowSize)
			{
				//get x1, x2, ..., x(rowIdx) from current row
				double currentX = matrix.MatrixBoard[rowIdx, rowIdx];

				if(currentX == 0)
				{
					int rowIdxToChangeWith = GetRowIdx(matrix, rowIdx, rowIdx);

					if(rowIdxToChangeWith == -1)
					{
						throw new GausJordanException("No row found to change with!");
					}

					matrix.ChangeTwoRows(rowIdx, rowIdxToChangeWith);
					vector.ChangeTwoRows(rowIdx, rowIdxToChangeWith);
					continue;
				}

				for (int i = 0; i < matrix.RowSize; i++)
				{
					if(i == rowIdx)
					{
						continue;
					}

					//get x1, x2, ... , x(rowIdx) from other rows
					double xToEliminate = matrix.MatrixBoard[i, rowIdx];

					//eliminate only if not already 0
					if (xToEliminate != 0)
					{
						//xToEliminate + (currentX * x) = 0;
						//e.g. 4 + (1 * x) = 0 -> 4 + x = 0 -> -4/1 = x -> x = -4 -> -(xToEliminate/currentX)
						double factor = -(xToEliminate / currentX);

						for (int j = 0; j < matrix.ColumnSize; j++)
						{
							matrix.MatrixBoard[i, j] += matrix.MatrixBoard[rowIdx, j] * factor;

							//update vector b if it is the inverse
							if (!vector.IsVector)
							{
								vector.MatrixBoard[i, j] += vector.MatrixBoard[rowIdx, j] * factor;
							}
						}

						//update vector b
						if (vector.IsVector)
						{
							vector.MatrixBoard[i, 0] += vector.MatrixBoard[rowIdx, 0] * factor;
						}			
					}
				}
				rowIdx++;
			}

			//make I (diag(1,1,1,...))
			for (int i = 0; i < matrix.RowSize; i++)
			{
				double quotient = matrix.MatrixBoard[i, i];

				matrix.MatrixBoard[i, i] /= quotient;

				for (int j = 0; j < vector.ColumnSize; j++)
				{
					vector.MatrixBoard[i, j] /= quotient;
				}
			}

			//round board values (avoid too many digits and very small values e.g. 5,1E-17)
			vector.RoundBoardValues();
			matrix.RoundBoardValues();
        }

		private int GetRowIdx(Matrix m, int currentRowIdx, int columnIdx)
		{
			for (int i = 0; i < m.RowSize; i++)
			{
				if (i != currentRowIdx)
				{
					//return first row where the element is not 0 at this position
					if (m.MatrixBoard[i, columnIdx]  != 0)
					{
						return i;
					}
				}
			}

			return -1;
		}

		public double CalculateDeterminante(Matrix matrix)
		{
			//start from first row (eliminate x1)
			int rowIdx = 0;
			double determinante = 1;

			while (rowIdx < matrix.RowSize)
			{
				//determinante can be read from matrix if its is a triangular matrix
				if (IsTriangularMatrix(matrix))
				{
					break;
				}

				//get x1, x2, ..., x(rowIdx) from current row
				double currentX = matrix.MatrixBoard[rowIdx, rowIdx];

				if (currentX == 0)
				{
					int rowIdxToChangeWith = GetRowIdx(matrix, rowIdx, rowIdx);

					if (rowIdxToChangeWith == -1)
					{
						throw new GausJordanException("No row found to change with!");
					}

					matrix.ChangeTwoRows(rowIdx, rowIdxToChangeWith);
					//determinante *= -1;
					continue;
				}

				for (int i = 0; i < matrix.RowSize; i++)
				{
					if (i == rowIdx)
					{
						continue;
					}

					//get x1, x2, ... , x(rowIdx) from other rows
					double xToEliminate = matrix.MatrixBoard[i, rowIdx];

					//eliminate only if not already 0
					if (xToEliminate != 0)
					{
						//xToEliminate + (currentX * x) = 0;
						//e.g. 4 + (1 * x) = 0 -> 4 + x = 0 -> -4/1 = x -> x = -4 -> -(xToEliminate/currentX)
						double factor = -(xToEliminate / currentX);

						for (int j = 0; j < matrix.ColumnSize; j++)
						{
							matrix.MatrixBoard[i, j] += matrix.MatrixBoard[rowIdx, j] * factor;
						}

						//determinante *= factor;
					}
				}
				rowIdx++;
			}

			//make I (diag(1,1,1,...))
			for (int i = 0; i < matrix.RowSize; i++)
			{
				determinante *= matrix.MatrixBoard[i, i];
			}

			matrix.RoundBoardValues();

            Console.WriteLine(determinante);
            return determinante;
		}

		private bool IsTriangularMatrix(Matrix matrix)
		{
			for (int i = 0; i < matrix.RowSize; i++)
			{
				for (int j = i; j > 0; j--)
				{
					if (matrix.MatrixBoard[i,j] != 0)
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}
