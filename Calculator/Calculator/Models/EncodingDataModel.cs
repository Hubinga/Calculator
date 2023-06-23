namespace Calculator.Models
{
	public class EncodingDataModel
	{
		public EncodingDataModel(string value, double probability, string encoding)
		{
			Value = value;
			Probability = probability;
			Encoding = encoding;
		}

		public string Value { get; private set; } = "";
		public double Probability { get; private set; } = 0;
		public string Encoding { get; set; } = "";
	}
}
