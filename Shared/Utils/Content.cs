using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Torsion.Utils
{
    /// <summary>
    /// Class contains methods for reading Content files into Class or Properties
    /// </summary>
    internal class Content
    {
        /// <summary>
        /// Retrieves the list of Class Items from a Json File
        /// </summary>
        /// <typeparam name="T">Specify the Type of Class to return</typeparam>
        /// <param name="fileName"><see cref="string"/> value for the file name.</param>
        /// <returns><see cref="List{TModel}"/></returns>
        /// <remarks>The file name does not need an extension and will look for Json files in %Assembly Path%\Json\</remarks>
        internal static List<TModel> GetFromJSON<TModel>(string fileName) where TModel : class
        {
            //Get the Path relative to the Executing Assembly
            string jsonPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"JSON\{fileName}.json");
            //Check to make sure the file exists at the path provided
            if (File.Exists(jsonPath))
            {
                //Open and read the file with the path using a Stream Reader object
                using (StreamReader reader = new StreamReader(jsonPath))
                {
                    //Read the contents of the file to a string
                    string raw = reader.ReadToEnd();
                    //Deserialize the file to the Generic Class provided
                    return JsonSerializer.Deserialize<List<TModel>>(raw);
                }
            }
            //If the file does not exist then return default empty List;
            return default;
        }
    }
}
