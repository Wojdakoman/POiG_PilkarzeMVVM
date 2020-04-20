using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PiłkarzeMVVM.Model
{
    /// <summary>
    /// Class used to load and save data in JSON file.
    /// Requires Newtonsoft.Json NuGet pack installed
    /// </summary>
    static class JSON
    {
        public static List<Player> LoadData(string path)
        {
            List<Player> items = new List<Player>();
            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    string json = file.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Player>>(json);
                }
            }
            return items;
        }

        public static void Save(string path, List<Player> list)
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, json);
        }
    }
}
