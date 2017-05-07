using System;
using System.Diagnostics;

namespace EquationMath
{
	public interface IToken
	{

	}

	public class LeftParenthesis : IToken, IEquatable<LeftParenthesis>
	{
		public bool Equals(LeftParenthesis other) => !ReferenceEquals(null, other);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((LeftParenthesis) obj);
		}

		public override int GetHashCode() => 0;
	}

	public class RightParenthesis : IToken, IEquatable<RightParenthesis>
	{
		public bool Equals(RightParenthesis other) => !ReferenceEquals(null, other);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((RightParenthesis) obj);
		}

		public override int GetHashCode() => 0;
	}

	[DebuggerDisplay("Operator {" + nameof(Value) + "}")]
	public class Operator : IToken, IEquatable<Operator>
	{
		public char Value { get; }

		public Operator(char value)
		{
			Value = value;
		}

		public bool Equals(Operator other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Operator) obj);
		}

		public override int GetHashCode() => Value.GetHashCode();
	}


	[DebuggerDisplay("Term {" + nameof(Value) + "}{" + nameof(Name) + "}")]
	public class Term : IToken, IEquatable<Term>
	{
		public double Value { get; }
		public string Name { get; }

		public Term(double value, string name)
		{
			Value = value;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public bool Equals(Term other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Term) obj);
		}

		public override int GetHashCode() => unchecked((Name.GetHashCode() * 397) ^ Value.GetHashCode());
	}
}
