using Xunit;

namespace EquationMath.Test
{
	public class TermStringifierTest
	{
		[Fact]
		public void SimpleStringify()
		{
			IToken[] tokens =
			{
				new Term(1, "x^2"),
				new Operator('-'),
				new Term(1, "y^2"),
				new Operator('+'),
				new Term(4.5, "xy"),
				new Operator('-'),
				new Term(2, string.Empty),
				new Operator('='),
				new Term(0, string.Empty)
			};

			const string expected = "x^2-y^2+4.5xy-2=0";

			var stringifier = new TermStringifier();
			string result = stringifier.Stringify(tokens);
			Assert.Equal(expected, result);
		}
	}
}
