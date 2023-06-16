using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
	public class MatrixDimensionModel
	{
		private string dimension = "3,3";

		[Range(1, 20, ErrorMessage = "Only positive number from 1 to 20 allowed")]
		public int RowSize { get; set; } = 3;
		[Range(1, 20, ErrorMessage = "Only positive number from 1 to 20 allowed")]
		public int CollumnSize { get; set; } = 3;
		//add validation
		public string Dimension { get => dimension; set => ExtractDimension(value); }


		private void ExtractDimension(string value)
		{
			string[] dimensionValues = value.Split(',');

			if(dimensionValues.Length != 2)
			{
				throw new Exception("Wrong Dimension Values!");
			}

			int rowSize;
			if (int.TryParse(dimensionValues[0], out rowSize))
			{
				RowSize = Convert.ToInt32(rowSize);
			}


			int collumnSize;
			if (int.TryParse(dimensionValues[1], out collumnSize))
			{
				CollumnSize = Convert.ToInt32(collumnSize);
			}

			dimension = value;
		}
	}
}

