using System.Text.Json.Serialization;

namespace Torsion.Models
{
    /// <summary>
    /// Class for reading Parameters from JSON file for mapping to Model Parameters
    /// </summary>
    internal class ParameterItem
    {
        //This is the description shown in the Mapping Form
        [JsonPropertyName("description")]
        public string Description { get; set; }
        //This is the value used in the code to look up the parameter
        [JsonPropertyName("code")]
        public string Code { get; set; }
        //This is the value used in the Model
        [JsonPropertyName("model")]
        public string Model { get; set; }
    }
}