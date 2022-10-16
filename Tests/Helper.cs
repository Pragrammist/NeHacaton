using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using static Tests.Constants;

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
            return JsonSerializer.Serialize(obj, options);
        }

        public static T GetLoginUserFromJsonFile<T>() 
        {
            var json = File.ReadAllText(LOGIN_USER_JSON_PATH);
            var res = JsonSerializer.Deserialize<T>(json) ?? throw new NullReferenceException("serializer of user got null");
            return res;
        }

        public static T GetRegistreUserFromJsonFile<T>()
        {
            var json = File.ReadAllText(REGISTRATE_USER_JSON_PATH);
            var res = JsonSerializer.Deserialize<T>(json) ?? throw new NullReferenceException("serializer of user got null");
            return res;
        }
    }
}