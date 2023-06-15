using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
	public class MatrixDimensionModel
	{
		[Required]
		[Range(1, 20, ErrorMessage = "Only positive number from 1 to 20 allowed")]
		public int RowSize { get; set; } = 3;
		[Required]
		[Range(1, 20, ErrorMessage = "Only positive number from 1 to 20 allowed")]
		public int CollumnSize { get; set; } = 3;
	}
}

