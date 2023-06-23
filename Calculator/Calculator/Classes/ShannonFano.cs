using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.Classes
{
	public class ShannonFano
	{
		public List<EncodingDataModel> encodingDataModels { get; private set; } = new List<EncodingDataModel>();

		public double CalculateEntropy()
		{
			EntropyHelper entropyHelper = new EntropyHelper();
			return entropyHelper.CalculateEntropyForBits(encodingDataModels);
		}

		public double CalculateAverageMessageLenght()
		{
			return Math.Round(encodingDataModels.Sum(e => e.Probability * e.Encoding.Length),3);
		}

		public void GenerateShannonFano(List<Tuple<string, int>> valuePairs)
		{
			encodingDataModels.Clear();

			double totalAmount = valuePairs.Sum(a => a.Item2);

			//store values with probabilities 
			foreach (var pair in valuePairs)
			{
				encodingDataModels.Add(new EncodingDataModel(pair.Item1, pair.Item2 / totalAmount, ""));
			}

			//sort ascending
			encodingDataModels.Sort((x, y) => x.Probability.CompareTo(y.Probability));

			NextSubset(1, 0, encodingDataModels.Count - 1, true);
		}

		private void NextSubset(double probabilityOfSubset, int startIdxSubset, int endIdxSubset, bool left)
		{
            //not at first call (not a subset)
            if (probabilityOfSubset < 1)
			{
				//add code to encoding dictionary: 0 if left side, 1 if right side
				for (int i = startIdxSubset; i <= endIdxSubset; i++)
				{
					encodingDataModels[i].Encoding += left ? "0" : "1";
				}
			}

			//stop if only one element left in subset
			if(startIdxSubset == endIdxSubset)
			{
				return;
			}

			double currentProbabilityOfSubset = probabilityOfSubset / 2;
			double currentProbability = 0;
			int lastIdxFromSubset = startIdxSubset;

			for (int i = startIdxSubset; i <= endIdxSubset; i++)
			{
				double probabilityValue = encodingDataModels[i].Probability;

				if (currentProbability + probabilityValue <= currentProbabilityOfSubset)
				{
					currentProbability += probabilityValue;
					lastIdxFromSubset = i;
				}
				else
				{
					double leftSideProbability = Math.Round(currentProbability + probabilityValue, 3);
					double rightSideProbability = Math.Round(probabilityOfSubset - currentProbability, 3);

					//add currentProbability to left side subset if the result is closer to currentProbabilityOfSubset
					if (leftSideProbability < rightSideProbability)
					{
						currentProbability += probabilityValue;
						lastIdxFromSubset = i;
					}

					break;
				}
			}
			//left side 
			NextSubset(currentProbability, startIdxSubset, lastIdxFromSubset, true);
			//right side
			NextSubset(probabilityOfSubset - currentProbability, lastIdxFromSubset + 1, endIdxSubset, false);
		}
	}
}
