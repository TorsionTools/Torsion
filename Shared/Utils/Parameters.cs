using System.Linq;

namespace Torsion.Utils
{
    internal class Parameters
    {
        internal static string Find(string code)
        {
            return AppVars.ParameterMappings.First(p => p.Code == code).Model;
        }
    }
}
