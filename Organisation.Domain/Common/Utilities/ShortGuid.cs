namespace Organisation.Domain.Common.Utilities;

public sealed class ShortGuid
{
    public static string NewGuid()
    {
        // Generate a new standard GUID
        var guid = Guid.NewGuid();

        // Convert the GUID to a byte array
        byte[] bytes = guid.ToByteArray();

        // Convert the byte array to a base64-encoded string with modified base64 characters
        string base64 = Convert.ToBase64String(bytes)
            .Replace("/", "_")
            .Replace("+", "-")
            .Substring(0, 22); // Truncate the string to make it shorter

        return base64;
    }
}