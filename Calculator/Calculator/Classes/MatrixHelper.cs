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

				for (int i = 0; i < matrix.RowSize; i++)
				{
					if(i == rowIdx)
					{
						continue;
					}

					//get x1, x2, ... , x(rowIdx) from other rows
					double xToEliminate = matrix.MatrixBoard[i, rowIdx];
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

			//round vector b
			for (int i = 0; i < vector.RowSize; i++)
			{
				vector.MatrixBoard[i, 0] = Math.Round(vector.MatrixBoard[i, 0], 3);
			}

			Console.WriteLine("current matrix A:");
			matrix.Print();
			Console.WriteLine("current vector b:");
			vector.Print();
			Console.WriteLine("end");
        }
	}
}
