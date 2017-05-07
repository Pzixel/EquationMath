using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationMath
{
	public class Reducer
	{
		public IEnumerable<IToken> GroupTokens(IEnumerable<IToken> tokens)
		{
			bool negateCoefficient = false;
			var dictionary = new Dictionary<string, double>();
			foreach (var token in tokens)
			{
				switch (token)
				{
					case Term term:
						var coefficient = negateCoefficient ? -term.Value : term.Value;
						if (dictionary.TryGetValue(term.Name, out var currentCoefficient))
							dictionary[term.Name] = currentCoefficient + coefficient;
						else
							dictionary[term.Name] = coefficient;
						break;
					case Operator op:
						negateCoefficient = op.Value == '-';
						break;
				}
			}
			var terms = dictionary.ToArray();
			var firstPair = terms.First();
			yield return new Term(firstPair.Value, firstPair.Key);
			foreach (var pair in terms.Skip(1).Where(term => Math.Abs(term.Value) > double.Epsilon))
			{
				char op = pair.Value > 0 ? '+' : '-';
				yield return new Operator(op);
				yield return new Term(Math.Abs(pair.Value), pair.Key);
			}
			yield return new Operator('=');
			yield return new Term(0, string.Empty);
		}
	}
}

