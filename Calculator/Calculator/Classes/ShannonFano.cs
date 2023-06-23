using System.Collections.Generic;

namespace Calculator.Classes
{
	public class ShannonFano
	{
		public Dictionary<string, string> valueEncodings { get; private set; } = new Dictionary<string, string>();
		public List<Tuple<string, double>> valueProbabilities { get; private set; } = new List<Tuple<string, double>>();

		public void GenerateShannonFano(List<Tuple<string, int>> valuePairs)
		{
			double totalAmount = valuePairs.Sum(a => a.Item2);

			//store values with probabilities 
			foreach (var pair in valuePairs)
			{
				valueProbabilities.Add(new Tuple<string, double>(pair.Item1, pair.Item2 / totalAmount));
			}

			//sort ascending
			valueProbabilities.Sort((x, y) => x.Item2.CompareTo(y.Item2));

			//initialize encoding dictionary with empty code
			foreach (var pair in valuePairs)
			{
				valueEncodings[pair.Item1] = "";
			}

			NextSubset(1, 0, valueProbabilities.Count - 1, true);
			PrintEncodings();
		}

		public void PrintEncodings()
		{
			foreach (var pair in valueEncodings)
			{
                Console.WriteLine("{0} : {1}", pair.Key, pair.Value);
            }
		}

		private void NextSubset(double probabilityOfSubset, int startIdxSubset, int endIdxSubset, bool left)
		{

            Console.WriteLine("left: {0}, startIdx: {1}, endIdx: {2}", left, startIdxSubset, endIdxSubset);
			PrintEncodings();
            Console.WriteLine();

            //not at first call (not a subset)
            if (probabilityOfSubset < 1)
			{
				//add code to encoding dictionary: 0 if left side, 1 if right side
				for (int i = startIdxSubset; i <= endIdxSubset; i++)
				{
					valueEncodings[valueProbabilities[i].Item1] += left ? "0" : "1";
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
				double probabilityValue = valueProbabilities[i].Item2;

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
