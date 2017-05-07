using System;
using System.Collections.Generic;

namespace EquationMath
{
	public class ParenthesisRemover
	{
		public IEnumerable<IToken> RemoveParenthesis(IReadOnlyCollection<IToken> tokens)
		{
			AssertParenthesisAreBalanced(tokens);
			return GetTokensWithoutParenthesis(tokens);
		}

		private static IEnumerable<IToken> GetTokensWithoutParenthesis(IEnumerable<IToken> tokens)
		{
			bool shouldNegateArithmeticOperator = false;
			foreach (var token in tokens)
			{
				switch (token)
				{
					case Operator op when shouldNegateArithmeticOperator:
						char negatedOp = op.Value == '+' ? '-' : '+';
						yield return new Operator(negatedOp);
						break;
					case LeftParenthesis _:
					case RightParenthesis _:
						shouldNegateArithmeticOperator = !shouldNegateArithmeticOperator;
						break;
					default:
						yield return token;
						break;
				}
			}
		}

		private static void AssertParenthesisAreBalanced(IEnumerable<IToken> tokens)
		{
			int leftParenthesisCount = 0;
			foreach (var token in tokens)
			{
				if (token is LeftParenthesis)
					leftParenthesisCount++;
				else if (token is RightParenthesis)
				{
					if (leftParenthesisCount == 0)
						throw new ArgumentException("Right parenthesis is before left one");
					leftParenthesisCount--;
				}
			}
			if (leftParenthesisCount > 0)
				throw new ArgumentException($"{leftParenthesisCount} left parenthesis are not balanced");
		}
	}
}
