using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationMath
{
	public class Canonizer
	{
		public IReadOnlyList<IToken> Canonize(IReadOnlyCollection<IToken> tokens)
		{
			var equalityTokens = tokens.OfType<Operator>().Where(token => token.Value == '=').ToArray();
			if (equalityTokens.Length != 1)
			{
				throw new ArgumentException("Single equality operator is allowed in equation", nameof(tokens));
			}

			var equalityToken = equalityTokens.Single();
			var tokensBeforeEqualiToken = tokens.TakeWhile(token => token != equalityToken);
			var tokensAfterEqualityToken = tokens.SkipWhile(token => token != equalityToken).Skip(1);

			var result = new List<IToken>(tokens.Count + 5);
			result.AddRange(tokensBeforeEqualiToken);
			result.Add(new Operator('-'));
			result.Add(new LeftParenthesis());
			result.AddRange(tokensAfterEqualityToken);
			result.Add(new RightParenthesis());

			result.Add(equalityToken);
			result.Add(new Term(0, string.Empty));

			return result;
		}
	}
}
