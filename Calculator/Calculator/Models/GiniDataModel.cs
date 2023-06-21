namespace Calculator.Models
{
	public class GiniDataModel
	{
		public GiniDataModel(double value, double sumedUpValue, double shareOfTotal, double percentage)
		{
			Value = value;
			SumedUpValue = sumedUpValue;
			this.shareOfTotal = shareOfTotal;
			Percentage = percentage;
		}

		public double Value { get; private set; }
		public double SumedUpValue { get; private set; }
		public double shareOfTotal { get; private set; }
		public double Percentage { get; private set; }
	}
}
