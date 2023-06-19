namespace Calculator.Classes
{
	public class MatrixHelper
	{
		/// <summary>
		/// Solve system of equations (Ax = b)
		/// </summary>
		/// <param name="matrix">matrix A</param>
		/// <param name="vektor">vector b</param>
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
                    Console.WriteLine("Change Row at Gaus-Jordan:");
                    matrix.Print();
					vector.Print();
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
						}

						//update vector b
						vector.MatrixBoard[i, 0] += vector.MatrixBoard[rowIdx, 0] * factor;
					}
				}

                Console.WriteLine("current matrix A:");
                matrix.Print();
				Console.WriteLine("current vector b:");
				vector.Print();
				rowIdx++;
			}

			//make I (diag(1,1,1,...))
			for (int i = 0; i < matrix.RowSize; i++)
			{
				double quotient = matrix.MatrixBoard[i, i];

				matrix.MatrixBoard[i, i] /= quotient;
				vector.MatrixBoard[i, 0] /= quotient;
			}

			//round board values (avoid too many digits and very small values e.g. 5,1E-17)
			vector.RoundBoardValues();
			matrix.RoundBoardValues();

			Console.WriteLine("current matrix A:");
			matrix.Print();
			Console.WriteLine("current vector b:");
			vector.Print();
			Console.WriteLine("end");
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
	}
}
