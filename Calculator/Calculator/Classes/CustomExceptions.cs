namespace Calculator.Classes
{
	public class WrongDimesnionException : Exception
	{
		public WrongDimesnionException(string? message) : base(message)
		{
		}
	}

	public class GausJordanException : Exception
	{
		public GausJordanException(string? message) : base(message)
		{
		}
	}

	public class VectorException : Exception
	{
		public VectorException(string? message) : base(message)
		{
		}
	}
}
