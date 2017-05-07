using System;
using System.Linq;
using Xunit;

namespace EquationMath.Test
{
	public class LexerTest
	{
		[Fact]
		public void SimpleParsing()
		{
			IToken[] expected =
			{
				new Term(1.5, "x^2y^2"),
				new Operator('+'),
				new Term(2, "y")
			};
			var lexer = new Lexer();
			var tokens = lexer.Tokenize("1.5x^2y^2 + 2y").ToArray();
			Assert.Equal(expected, tokens);
		}

		[Fact]
		public void Parenthesis()
		{
			IToken[] expected =
			{
				new LeftParenthesis(),
				new Term(2, "x"),
				new RightParenthesis()
			};
			var lexer = new Lexer();
			var tokens = lexer.Tokenize("(2x)").ToArray();

			Assert.Equal(expected, tokens);
		}

		[Fact]
		public void ParsingError()
		{
			var lexer = new Lexer();
			var argumentException = Assert.Throws<ArgumentException>("input", () => lexer.Tokenize("2x^^2").ToArray());
			Assert.Contains("Parsing error on symbol '^'", argumentException.Message);
		}

		[Fact]
		public void AllTogether()
		{
			IToken[] expected =
			{
				new Term(1, "x^2"),
				new Operator('+'),
				new Term(3.5, "xy"),
				new Operator('+'),
				new Term(1, "y"),
				new Operator('='),
				new Term(1, "y^2"),
				new Operator('-'),
				new Term(1, "xy"),
				new Operator('+'),
				new Term(1, "y"),
				new Operator('+'),
				new Term(2, string.Empty)
			};
			var lexer = new Lexer();
			var tokens = lexer.Tokenize("x^2 + 3.5xy + y = y^2 - xy + y + 2").ToArray();

			Assert.Equal(expected, tokens);
		}
	}
}
