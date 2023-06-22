using Calculator.Models;

namespace Calculator.Classes
{
	public class EntropyHelper
	{
		public Dictionary<double, double> GenerateEntropyDictionary(List<double> values)
		{
			Dictionary<double, double> valuePairs = new Dictionary<double, double>();

			double probability = Math.Round(1 / (double)values.Count, 3);

			foreach (double value in values)
			{
				if (valuePairs.ContainsKey(value))
				{
					valuePairs[value] += probability;
				}
				else
				{
					valuePairs.Add(value, probability);
				}
			}

			//fix rounding eror: set propability to 1 (100%) if only 1 element
			if (valuePairs.Count == 1)
			{
				double firstKey = valuePairs.First().Key;
				valuePairs[firstKey] = 1;
			}

			return valuePairs;
		}

		public double CalculateEntropy(Dictionary<double, double> valuePairs)
		{

			double sum = valuePairs.Values.Sum(p => p * Math.Log10(p));

			//set very small values to 0: e.g. -0,001 -> 0
			if (Math.Abs(sum) < 0.009)
			{
				sum = 0;
			}

			double entropy = sum == 0 ? sum : -1 * sum;

			return Math.Round(entropy, 3); 
		}
	}
}
