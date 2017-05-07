using System;
using System.Linq;
using Xunit;

namespace EquationMath.Test
{
	public class CanonizerTest
	{
		[Fact]
		public void SimpleParsing()
		{
			IToken[] tokens =
			{
				new Term(1, "x"),
				new Operator('+'),
				new Term(2, "y"),
				new Operator('='),
				new Term(3, "y")
			};

			IToken[] expected =
			{
				new Term(1, "x"),
				new Operator('+'),
				new Term(2, "y"),
				new Operator('-'),
				new LeftParenthesis(),
				new Term(3, "y"),
				new RightParenthesis(),
				new Operator('='),
				new Term(0, string.Empty)
			};

			var canonizer = new Canonizer();
			var result = canonizer.Canonize(tokens).ToArray();

			Assert.Equal(expected, result);
		}

		[Fact]
		public void MultipleEqualityError()
		{
			IToken[] tokens =
			{
				new Term(1, "x"),
				new Operator('='),
				new Term(2, "y"),
				new Operator('='),
				new Term(3, "y")
			};

			var canonizer = new Canonizer();
			var argumentException = Assert.Throws<ArgumentException>("tokens", () => canonizer.Canonize(tokens).ToArray());
			Assert.Contains("Single equality operator is allowed in equation", argumentException.Message);
		}
	}
}
