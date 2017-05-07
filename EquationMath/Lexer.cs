using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EquationMath
{
	public class Lexer
	{
		public IEnumerable<IToken> Tokenize(string input)
		{
			const char decimalSeparator = '.';
			string inputWithoutSpaces = input.Replace(" ", string.Empty);
			var numberBuffer = new StringBuilder();
			var termBuffer = new StringBuilder();
			foreach (char c in inputWithoutSpaces)
			{
				switch (c)
				{
					case var _ when IsTerm(c, termBuffer):
						termBuffer.Append(c);
						break;
					case decimalSeparator:
					case var _ when IsDigit(c):
						numberBuffer.Append(c);
						break;
					case var _ when IsOperator(c):
						if (numberBuffer.Length > 0 || termBuffer.Length > 0)
							yield return new Term(EmptyNumberBufferAsLiteral(numberBuffer), EmptyTermBufferAsTerm(termBuffer));
						yield return new Operator(c);
						break;
					case '(':
						yield return new LeftParenthesis();
						break;
					case ')':
						if (numberBuffer.Length > 0 || termBuffer.Length > 0)
							yield return new Term(EmptyNumberBufferAsLiteral(numberBuffer), EmptyTermBufferAsTerm(termBuffer));
						yield return new RightParenthesis();
						break;
					default:
						throw new ArgumentException($"Parsing error on symbol '{c}'", nameof(input));
				}
			}
			if (numberBuffer.Length > 0 || termBuffer.Length > 0)
			{
				yield return new Term(EmptyNumberBufferAsLiteral(numberBuffer), EmptyTermBufferAsTerm(termBuffer));
			}
		}

		private static bool IsTerm(char c, StringBuilder termBuffer) =>
			char.IsLetter(c) || termBuffer.Length > 0 && (IsDigit(c) || c == '^' && termBuffer[termBuffer.Length - 1] != '^');

		private static bool IsDigit(char c) => c >= '0' && c <= '9';
		private static bool IsOperator(char c) => c == '+' || c == '-' || c == '=';

		private static double EmptyNumberBufferAsLiteral(StringBuilder numberBuffer)
		{
			if (numberBuffer.Length == 0)
				return 1;
			string valueString = numberBuffer.ToString();
			double value = double.Parse(valueString, CultureInfo.InvariantCulture);
			numberBuffer.Clear();
			return value;
		}

		private static string EmptyTermBufferAsTerm(StringBuilder termBuffer)
		{
			if (termBuffer.Length == 0)
				return string.Empty;
			string value = termBuffer.ToString();
			termBuffer.Clear();
			return value;
		}
	}
}
