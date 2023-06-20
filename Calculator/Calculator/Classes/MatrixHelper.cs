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
					SwapRows(matrix, vector, rowIdx);
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

		private void SwapRows(Matrix matrix, Matrix? vector, int rowIdx)
		{
			int rowIdxToChangeWith = GetRowIdx(matrix, rowIdx, rowIdx);

			if (rowIdxToChangeWith == -1)
			{
				throw new GausJordanException("No row found to change with!");
			}

			matrix.SwapTwoRows(rowIdx, rowIdxToChangeWith);
			vector?.SwapTwoRows(rowIdx, rowIdxToChangeWith);
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
		/// <summary>
		/// Compute determinante using Gauß-Jordan Algorithumus
		/// </summary>
		/// <param name="matrix">(n,n)-Matrix</param>
		/// <returns></returns>
		/// <exception cref="GausJordanException"></exception>
		public double CalculateDeterminante(Matrix matrix)
		{
			//start from first row (eliminate x1)
			int rowIdx = 0;
			double determinante = 1;

			while (rowIdx < matrix.RowSize)
			{
				//determinante can be computed if its is a triangular matrix
				if (IsTriangularMatrix(matrix))
				{
					break;
				}

				//get x1, x2, ..., x(rowIdx) from current row
				double currentX = matrix.MatrixBoard[rowIdx, rowIdx];

				if (currentX == 0)
				{
					SwapRows(matrix, null, rowIdx);
					//Swapping 2 lines changes the sign
					determinante *= -1;
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
					}
				}
				rowIdx++;
			}

			//determinante is product of diagonal elements (triangular matrix)
			for (int i = 0; i < matrix.RowSize; i++)
			{
				determinante *= matrix.MatrixBoard[i, i];
			}

			matrix.RoundBoardValues();

            return Math.Round(determinante, 2);
		}

		private bool IsTriangularMatrix(Matrix matrix)
		{
			for (int i = 0; i < matrix.RowSize; i++)
			{
				for (int j = 0; j < i; j++)
				{
					if (matrix.MatrixBoard[i,j] != 0)
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Linear Dependence Test using Gauß-Jordan Algorithumus
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public bool LinearDependence(Matrix matrix)
		{
			//start from first row (eliminate x1)
			int rowIdx = 0;

			while (rowIdx < matrix.RowSize)
			{
				//if all elements of one row or collumn are 0: stop (linear dependence)
				if (ContainsNullRowOrColumn(matrix))
				{
					break;
				}

				//get x1, x2, ..., x(rowIdx) from current row
				double currentX = matrix.MatrixBoard[rowIdx, rowIdx];

				if (currentX == 0)
				{
					SwapRows(matrix, null, rowIdx);
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
					}
				}
				rowIdx++;
			}

			matrix.RoundBoardValues();
			return ContainsNullRowOrColumn(matrix);
		}

		private bool ContainsNullRowOrColumn(Matrix matrix)
		{
			//rows
			for (int i = 0; i < matrix.RowSize; i++)
			{
				var row = Enumerable.Range(0, matrix.ColumnSize).Select(e => matrix.MatrixBoard[i, e]).ToList();

				if(row.All(e=>e == 0))
				{
					return true;
				}
			}

			//columns
			for (int i = 0; i < matrix.ColumnSize; i++)
			{
				var column = Enumerable.Range(0, matrix.RowSize).Select(e => matrix.MatrixBoard[e, i]).ToList();

				if (column.All(e => e == 0))
				{
					return true;
				}
			}

			return false;
		}

		public Matrix BuildMatrixFromVectors(List<Matrix> vectors)
		{
			foreach (var vector in vectors) 
			{
				if (!vector.IsVector)
				{
					throw new VectorException("Vector can only have one column!");
				}
			}

			Matrix matrix = new Matrix(vectors[0].RowSize, vectors.Count);

			for (int i = 0; i < matrix.RowSize; i++)
			{
				for (int j = 0; j < matrix.ColumnSize; j++)
				{
					matrix.MatrixBoard[i, j] = vectors[j].MatrixBoard[i, 0];
				}
			}

			return matrix;
		}
	}
}
