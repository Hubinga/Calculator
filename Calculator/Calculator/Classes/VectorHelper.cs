namespace Calculator.Classes
{
	public class VectorHelper
	{
		public double CalculateLength(Matrix vector)
		{
			if (!vector.IsVector)
			{
				throw new VectorException("A Vector can only have one column!");
			}

			double sum = 0;

			for (int i = 0; i < vector.RowSize; i++)
			{
				sum += (vector.MatrixBoard[i, 0] * vector.MatrixBoard[i, 0]);
			}

			return Math.Round(Math.Sqrt(sum), 2);
		}

		/// <summary>
		/// calculate length of vector from A to B
		/// </summary>
		/// <param name="pointA"></param>
		/// <param name="pointB"></param>
		/// <returns></returns>
		public double DistanceBetweenTwoPoints(Matrix pointA, Matrix pointB) 
		{
			Matrix vector = pointB - pointA;

			return Math.Sqrt(CalculateLength(vector));
		}

		public double CalculateDotProduct(Matrix vectorA, Matrix vectorB)
		{
			if (!vectorA.IsVector || !vectorB.IsVector)
			{
				throw new VectorException("Dot-product is only defined for vectors!");
			}

			double sum = 0;

			for (int i = 0; i < vectorA.RowSize; i++)
			{
				sum += (vectorA.MatrixBoard[i, 0] * vectorB.MatrixBoard[i, 0]);
			}

			return sum;
		}

		public double[,] CalculateCrossProduct(Matrix vectorA, Matrix vectorB)
		{
			if (!vectorA.IsVector || !vectorB.IsVector)
			{
				throw new VectorException("Cross-product is only defined for vectors with 3 rows!");
			}

			double first = vectorA.MatrixBoard[1, 0] * vectorB.MatrixBoard[2, 0] - vectorA.MatrixBoard[2, 0] * vectorB.MatrixBoard[1, 0];
			double second = vectorA.MatrixBoard[0, 0] * vectorB.MatrixBoard[2, 0] - vectorA.MatrixBoard[2, 0] * vectorB.MatrixBoard[0, 0];
			second *= -1;
			double third = vectorA.MatrixBoard[0, 0] * vectorB.MatrixBoard[1, 0] - vectorA.MatrixBoard[1, 0] * vectorB.MatrixBoard[0, 0];

			return new double[3, 1] { { first }, { second }, { third } };
		}
	}
}
