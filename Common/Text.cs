namespace ITClassHelper
{
    internal class Text
    {
        public static string[] ConvertTextToList(string text, char splitChar)
        {
            return text.Split(splitChar);
        }
    }
}
