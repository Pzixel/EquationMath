using System;
using Xunit;

namespace EquationMath.Test
{
	public class ParenthesisRemoverTest
	{
		[Fact]
		public void SimpleParsing()
		{
			IToken[] tokens =
			{
				new Term(1, "x"),
				new Operator('-'),
				new LeftParenthesis(),
				new Term(3, "y"),
				new Operator('-'),
				new Term(2, "y"),
				new RightParenthesis(),
				new Operator('='),
				new Term(0, string.Empty)
			};

			IToken[] expected =
			{
				new Term(1, "x"),
				new Operator('-'),
				new Term(3, "y"),
				new Operator('+'),
				new Term(2, "y"),
				new Operator('='),
				new Term(0, string.Empty)
			};

			var parenthesisRemover = new ParenthesisRemover();
			var result = parenthesisRemover.RemoveParenthesis(tokens);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void TooManyBracesError()
		{
			IToken[] tokens =
			{
				new LeftParenthesis(),
				new LeftParenthesis(),
				new RightParenthesis()
			};

			var parenthesisRemover = new ParenthesisRemover();
			var exception = Assert.Throws<InvalidOperationException>(() => parenthesisRemover.RemoveParenthesis(tokens));
			Assert.Equal("1 left parenthesis are not balanced", exception.Message);
		}

		[Fact]
		public void NotEnoughBracesError()
		{
			IToken[] tokens =
			{
				new LeftParenthesis(),
				new RightParenthesis(),
				new RightParenthesis()
			};

			var parenthesisRemover = new ParenthesisRemover();
			var exception = Assert.Throws<InvalidOperationException>(() => parenthesisRemover.RemoveParenthesis(tokens));
			Assert.Contains("Right parenthesis is placed before the left one", exception.Message);
		}
	}
}
