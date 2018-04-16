using System.Configuration;

namespace StudyBuddy.Core
{
    public class WebUtilities
    {
        public static string ActiveDatabase() { return ConfigurationManager.AppSettings["ActiveDatabase"]; }
        public static string EncryptionCipher() { return ConfigurationManager.AppSettings["EncryptionCipher"]; }
    }
}