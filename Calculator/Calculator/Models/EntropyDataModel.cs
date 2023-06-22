namespace Calculator.Models
{
	public class EntropyDataModel
	{
		public EntropyDataModel(double value, double probability, double surprise)
		{
			Value = value;
			Probability = probability;
			Surprise = surprise;
		}

		public double Value { get; private set; }
		public double Probability { get; private set; }
		public double Surprise { get; private set; }
	}
}
