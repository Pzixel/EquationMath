using System.Linq;
using Xunit;

namespace EquationMath.Test
{
	public class ReducerTest
	{
		[Fact]
		public void SimpleParsing()
		{
			IToken[] tokens =
			{
				new Term(1, "x"),
				new Operator('-'),
				new Term(2, "y"),
				new Operator('-'),
				new Term(3, "y"),
				new Operator('='),
				new Term(0, string.Empty)
			};

			IToken[] expected =
			{
				new Term(1, "x"),
				new Operator('-'),
				new Term(5, "y"),
				new Operator('='),
				new Term(0, string.Empty)
			};

			var reducer = new Reducer();
			var result = reducer.GroupTokens(tokens).ToArray();

			Assert.Equal(expected, result);
		}

		[Fact]
		public void ComplexParsing()
		{
			IToken[] tokens =
			{
				new Term(1, "x^2"),
				new Operator('+'),
				new Term(3.5, "xy"),
				new Operator('+'),
				new Term(1, "y"),
				new Operator('-'),
				new Term(1, "y^2"),
				new Operator('+'),
				new Term(1, "xy"),
				new Operator('-'),
				new Term(1, "y"),
				new Operator('-'),
				new Term(2, string.Empty)
			};

			IToken[] expected =
			{
				new Term(1, "x^2"),
				new Operator('+'),
				new Term(4.5, "xy"),
				new Operator('-'),
				new Term(1, "y^2"),
				new Operator('-'),
				new Term(2, string.Empty),
				new Operator('='),
				new Term(0, string.Empty)
			};

			var reducer = new Reducer();
			var result = reducer.GroupTokens(tokens).ToArray();

			Assert.Equal(expected, result);
		}
	}
}
