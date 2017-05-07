using System.Linq;

namespace EquationMath
{
	public class PolynomeNormalizer
	{
		private readonly Lexer _lexer;
		private readonly Canonizer _canonizer;
		private readonly ParenthesisRemover _parenthesisRemover;
		private readonly Reducer _reducer;
		private readonly TermStringifier _stringifier;

		public PolynomeNormalizer()
		{
			_lexer = new Lexer();
			_canonizer = new Canonizer();
			_parenthesisRemover = new ParenthesisRemover();
			_reducer = new Reducer();
			_stringifier = new TermStringifier();
		}

		public string Normalize(string input)
		{
			var tokens = _lexer.Tokenize(input).ToArray();
			var canonizedTokens = _canonizer.Canonize(tokens);
			var tokensWithoutParenthesis = _parenthesisRemover.RemoveParenthesis(canonizedTokens);
			var reducedTokens = _reducer.GroupTokens(tokensWithoutParenthesis);
			string result = _stringifier.Stringify(reducedTokens);
			return result;
		}
	}
}
