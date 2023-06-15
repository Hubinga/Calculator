using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
	public class MatrixDimensionModel
	{
		[Required]
		public int RowSize { get; set; }
		[Required]
		public int CollumnSize { get; set; }
	}
}

