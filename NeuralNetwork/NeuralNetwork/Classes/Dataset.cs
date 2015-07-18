namespace NeuralNetwork.Classes
{
	public class DataSet
	{
		public double[] Values { get; set; }
		public int[] Results { get; set; }

		public DataSet(double[] values, int[] results)
		{
			Values = values;
			Results = results;
		}
	}
}
