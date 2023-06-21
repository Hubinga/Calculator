using Calculator.Models;

namespace Calculator.Classes
{
    public class GiniIndexHelper
    {
        public List<GiniDataModel> GenerateGiniDataModels(List<double> values)
        {
            List<GiniDataModel> models = new List<GiniDataModel>();

            double n = values.Count;
            double sumedUpValue = 0;
            double totalSum = values.Sum();

            foreach (double value in values)
            {
                sumedUpValue += value;
                models.Add(new GiniDataModel(value, sumedUpValue, Math.Round(sumedUpValue / totalSum, 3), Math.Round(1 /n, 3)));
            }

            return models;
        }

        public double CalculateGiniCoeffizient(List<GiniDataModel> models) 
        { 
            double b = 1/(double) models.Count;
            double sumOfTrapeze = 0;

            //calculate sum of trpaze: A = (h1 + h2) / 2 * b (h1,h2 = y-coordinates)
            for (int i = 0; i < models.Count; i++)
            {
                double h1 = i == 0 ? 0 : models[i - 1].shareOfTotal;

                sumOfTrapeze += (h1 + models[i].shareOfTotal) / 2 * b;
            }

            double maxConcentrationArea = 0.5;
            double concentrationArea = maxConcentrationArea - sumOfTrapeze;
            double gini = concentrationArea / maxConcentrationArea;

            return Math.Round(gini, 2);
		}
    }
}
