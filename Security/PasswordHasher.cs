using System.Globalization;
using System.Security.Cryptography;

namespace Companio.Security;

// uses PBKDF2-HMAC-SHA-1 with 10000 iterations
// salt is 16 bytes, hash size is 16 bytes
// format is "P1$cost$salt$hash"
public class PasswordHasher
{
    public static readonly PasswordHasher Default = new();

    public bool Verify(string password, string hash)
    {
        var parts = hash.Split('$');
        if (parts.Length != 4)
            throw new ArgumentException("Invalid hash format");
        if (parts[0] != "P1")
            throw new ArgumentException("Unsupported hash type");

        var cost = int.Parse(parts[1], CultureInfo.InvariantCulture);
        var salt = UrlSafeBase64.Decode(parts[2] + "==");
        var correctHash = parts[3];
        var hasher = new Rfc2898DeriveBytes(password, salt, cost);
        var attemptedHash = UrlSafeBase64.EncodeWithoutPadding(hasher.GetBytes(16));
        return attemptedHash == correctHash;
    }

    public string CreateHash(string password)
    {
        const int cost = 10000;
        var hasher = new Rfc2898DeriveBytes(password, 16, cost);
        var hash = hasher.GetBytes(16);
        var saltString = UrlSafeBase64.EncodeWithoutPadding(hasher.Salt);
        var hashString = UrlSafeBase64.EncodeWithoutPadding(hash);
        var result = "P1$" + cost + "$" + saltString + "$" + hashString;
        return result;
    }
}