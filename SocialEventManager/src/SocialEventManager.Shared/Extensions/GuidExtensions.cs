using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace SocialEventManager.Shared.Extensions;

public static class GuidExtensions
{
    private const char Dash = '-';
    private const char EqualsChar = '=';
    private const byte ForwardSlashByte = (byte)Slash;
    private const char Plus = '+';
    private const byte PlusByte = (byte)Plus;
    private const char Slash = '/';
    private const char Underscore = '_';
    private const int Base64LengthWithoutEquals = 22;

    public static string EncodeBase64String(this Guid guid)
    {
        Span<byte> guidBytes = stackalloc byte[16];
        Span<byte> encodedBytes = stackalloc byte[24];

        MemoryMarshal.TryWrite(guidBytes, ref guid);
        Base64.EncodeToUtf8(guidBytes, encodedBytes, out _, out _);

        Span<char> chars = stackalloc char[Base64LengthWithoutEquals];

        // Replace any characters which are not URL safe.
        // And skip the final two bytes as these will be '==' padding we don't need.
        for (int i = 0; i < Base64LengthWithoutEquals; i++)
        {
            chars[i] = encodedBytes[i] switch
            {
                ForwardSlashByte => Dash,
                PlusByte => Underscore,
                _ => (char)encodedBytes[i],
            };
        }

        return new(chars);
    }

    public static Guid DecodeBase64String(this ReadOnlySpan<char> id)
    {
        Span<char> base64Chars = stackalloc char[24];

        for (var i = 0; i < Base64LengthWithoutEquals; i++)
        {
            base64Chars[i] = id[i] switch
            {
                Dash => Slash,
                Underscore => Plus,
                _ => id[i],
            };
        }

        base64Chars[22] = EqualsChar;
        base64Chars[23] = EqualsChar;

        Span<byte> idBytes = stackalloc byte[16];
        Convert.TryFromBase64Chars(base64Chars, idBytes, out _);

        return new(idBytes);
    }
}
