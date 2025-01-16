using System;
using Autodesk.Revit.DB;

namespace Torsion.Extensions
{
    public static class ParameterExtensions
    {
        /// <summary>
        /// Get the boolean value from an Integer Parameter
        /// </summary>
        /// <param name="parameter"><see cref="Parameter"/></param>
        /// <returns><see langword="bool"/>: <see langword="true"/> for 1 and <see langword="false"/> for 0</returns>
        public static bool AsBool(this Parameter parameter)
        {
            return Convert.ToBoolean(parameter.AsInteger());
        }
        /// <summary>
        /// Determine if a Parameter is Built In
        /// </summary>
        /// <param name="parameter"><see cref="Parameter"/></param>
        /// <returns><see langword="true"/> if Built-In or <see langword="false"/> otherwise</returns>
        public static bool IsBuiltIn(this Parameter parameter)
        {
            if(parameter.Definition is InternalDefinition definition)
            {
                return new ElementId(definition.BuiltInParameter) != ElementId.InvalidElementId;
            }
            return false;
        }
    }
}
