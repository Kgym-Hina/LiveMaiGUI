using System.Text.RegularExpressions;

namespace LiveMai;

public static class Utils
{
    public static string GetAllMatchedItems(this string text, string start, string end)
    {
        int startIndex = text.IndexOf(start) + start.Length;
        int endIndex = text.IndexOf(end, startIndex);

        if (startIndex >= 0 && endIndex >= 0)
        {
            string result = text.Substring(startIndex, endIndex - startIndex);
            return result;
        }

        return "sbganmsl";
    }
}