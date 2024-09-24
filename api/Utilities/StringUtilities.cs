using System.Text.RegularExpressions;

using Ganss.Xss;

namespace api.Utilities;

public static class StringUtilities
{
    private static readonly HtmlSanitizer _htmlSanitizer = new();

    /// <summary>
    /// Sanitizes a string by removing potentially dangerous HTML characters, and optionally converting the case.
    /// </summary>
    /// <param name="input">The string to be sanitized.</param>
    /// <param name="convertCase">
    /// 0 - No conversion.
    /// 1 - Convert to lowercase.
    /// 2 - Convert to UPPERCASE.
    /// 3 - Convert to Title Case (first letter uppercase, rest lowercase).
    /// 4 - Convert to Title Case for each word (like a name).
    /// </param>
    /// <returns>The sanitized string.</returns>
    public static string Sanitize(string input, int convertCase = 0)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        // HTML sanitization
        string sanitized = _htmlSanitizer.Sanitize(input.Trim());

        // Remove hyphens and normalize whitespace
        sanitized = sanitized.Replace("-", string.Empty);
        sanitized = Regex.Replace(sanitized, @"\s+", " ");

        // Optionally convert case
        switch (convertCase)
        {
            case 1:
                sanitized = sanitized.ToLower();
                break;
            case 2:
                sanitized = sanitized.ToUpper();
                break;
            case 3:
                sanitized = char.ToUpper(sanitized[0]) + sanitized.Substring(1).ToLower();
                break;
            case 4:
                sanitized = ConvertToTitleCase(sanitized);
                break;
        }

        return sanitized;
    }

    /// <summary>
    /// Converts a string to title case, capitalizing the first letter of each word.
    /// </summary>
    /// <param name="input">The string to be converted.</param>
    /// <returns>The string in title case.</returns>
    private static string ConvertToTitleCase(string input)
    {
        return string.Join(" ", input.Split(' ')
                                     .Where(word => !string.IsNullOrEmpty(word))
                                     .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }
}
