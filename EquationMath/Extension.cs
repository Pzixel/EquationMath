using System.Text;

namespace EquationMath
{
    internal static class Extension
    {
	    public static bool Contains(this StringBuilder stringBuilder, char value)
	    {
		    for (int i = 0; i < stringBuilder.Length; i++)
		    {
			    if (stringBuilder[i] == value)
				    return true;
		    }
		    return false;
	    }
    }
}
