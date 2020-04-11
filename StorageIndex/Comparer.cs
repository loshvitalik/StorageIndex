using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageIndex
{
	public static class Comparer
	{
		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return toCheck.IndexOf(source, comp) >= 0;
		}

	}
}
