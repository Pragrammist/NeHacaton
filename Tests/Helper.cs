using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Tests
{
    public static class Helper
    {
        public static string Serialize<T>(T obj)
        {
            var options = new JsonSerializerOptions 
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            return System.Text.Json.JsonSerializer.Serialize(obj, options);
        }
    }
}