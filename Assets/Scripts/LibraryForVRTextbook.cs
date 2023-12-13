using System.IO;
using System.Runtime.CompilerServices;

namespace UnityVR
{
    public class LibraryForVRTextbook
    {
        public static string GetSourceFileName([CallerFilePath] string sourceFilePath = "") => Path.GetFileName(sourceFilePath.Replace(@"\", "/"));

        public static string getCallerName([CallerMemberName] string memberName = "") => memberName;
        
    }
}