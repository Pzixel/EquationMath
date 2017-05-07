using Xunit;

namespace EquationMath.Test
{
	public class PolynomeNormalizerTest
	{
		[Fact]
		public void MainTest()
		{
			const string input = "x^2 - (3.5xy - y) = y^2 - xy + y";
			const string expected = "x^2-2.5xy-y^2=0";

			var normalizer = new PolynomeNormalizer();
			string result = normalizer.Normalize(input);
			Assert.Equal(expected, result);
		}
	}
}
