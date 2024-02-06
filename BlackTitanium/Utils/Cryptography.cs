using System.Security.Cryptography;
using System.Text;

namespace BlackTitanium.Utils;

public class Cryptography {
    public static string Sha1(string input)
    {
        var hash = SHA1.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash);
    }
}