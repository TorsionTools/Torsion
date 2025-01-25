using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torsion.Models
{
    internal class MappedParameterGroup
    {
        public string Name { get; set; }
        [JsonPropertyName("parameters")]
        public List<MappedParameter> Parameters {  get; set; }
    }
}