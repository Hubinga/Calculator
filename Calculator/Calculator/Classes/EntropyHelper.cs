using Calculator.Models;

namespace Calculator.Classes
{
	public class EntropyHelper
	{
		public List<EntropyDataModel> GenerateEntropyDictionary(List<double> values)
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

			List<EntropyDataModel> dataModels = new List<EntropyDataModel>();

			foreach (var pair in valuePairs)
			{
				double surprise;
				if (valuePairs.Count > 2)
				{
					surprise = Math.Log10(1 / pair.Value);
				}
				else
				{
					surprise = Math.Log2(1 / pair.Value);
				}

				dataModels.Add(new EntropyDataModel(pair.Key, pair.Value, Math.Round(surprise, 3)));
			}

			return dataModels;
		}

		public double CalculateEntropy(List<EntropyDataModel> dataModels)
		{
			double entropy = dataModels.Sum(d=>d.Probability * d.Surprise);
			
			//set very small values to 0: e.g. -0,001 -> 0
			if (Math.Abs(entropy) < 0.009)
			{
				entropy = 0;
			}

			return Math.Round(entropy, 3); 
		}

		public double CalculateEntropyForBits(List<EncodingDataModel> encodingDataModels)
		{
			double entropy = encodingDataModels.Sum(d => d.Probability * Math.Log2(1/d.Probability));

			//set very small values to 0: e.g. -0,001 -> 0
			if (Math.Abs(entropy) < 0.009)
			{
				entropy = 0;
			}

			return Math.Round(entropy, 3);
		}
	}
}
