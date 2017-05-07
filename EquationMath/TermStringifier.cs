using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EquationMath
{
	public class TermStringifier
	{
		public string Stringify(IEnumerable<IToken> tokens)
		{
			var sb = new StringBuilder();
			foreach (var token in tokens)
			{
				switch (token)
				{
					case LeftParenthesis _:
						sb.Append('(');
						break;
					case RightParenthesis _:
						sb.Append(')');
						break;
					case Operator op:
						sb.Append(op.Value);
						break;
					case Term term:
						if (Math.Abs(term.Value - 1) > double.Epsilon || term.Name == string.Empty)
							sb.Append(term.Value.ToString(CultureInfo.InvariantCulture));
						sb.Append(term.Name);
						break;
					default:
						throw new InvalidOperationException($"Unknown term of type {token.GetType()}");
				}
			}
			return sb.ToString();
		}
	}
}
