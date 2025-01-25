using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torsion.Models
{
    internal class MappedGroups
    {
        [JsonPropertyName("parameterGroups")]
        public List<MappedParameterGroup> ParameterGroups { get; set; }
    }
}