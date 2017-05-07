using System.IO;
using System.Linq;
using static System.Console;

namespace EquationMath.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var polynomeNormalizer = new PolynomeNormalizer();
			if (args.Length > 0)
			{
				WriteLine("Working in file mode");
				string inputFileName = args[0];
				var results = File.ReadLines(inputFileName).Select(polynomeNormalizer.Normalize);
				File.WriteAllLines(inputFileName + ".out", results);
			}
			else
			{
				WriteLine("Working in interactive mode");
				while (true)
				{
					WriteLine("Enter some equation (for example 'x^2 + 3.5xy + y = y^2 - xy + y')");
					string input = ReadLine();
					string result = polynomeNormalizer.Normalize(input);
					WriteLine($"Normalized polynome: '{result}'");
				}
			}
		}
	}
}
