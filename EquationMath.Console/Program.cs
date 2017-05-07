using System;
using System.IO;
using System.Linq;

namespace EquationMath.Console
{
    class Program
    {
        static void Main(string[] args)
		{
			var polynomeNormalizer = new PolynomeNormalizer();
			if (args.Length > 0)
	        {
		        System.Console.WriteLine("Working in file mode");
		        string inputFileName = args[0];
		        var results = File.ReadLines(inputFileName).Select(polynomeNormalizer.Normalize);
		        File.WriteAllLines(inputFileName + ".out", results);
	        }
	        else
	        {
				System.Console.WriteLine("Working in interactive mode");
		        while (true)
		        {
			        System.Console.WriteLine("Enter some equation (for example 'x^2 + 3.5xy + y = y^2 - xy + y')");
			        string input = System.Console.ReadLine();
			        string result = polynomeNormalizer.Normalize(input);
			        System.Console.WriteLine($"Normalized polynome: '{result}'");
		        }
			}
        }
    }
}