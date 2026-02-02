namespace PersonColor.Api.Mappings
{
    public static class ColorMapping
    {
        private static readonly Dictionary<int, string> _colorMap = new()
        {
            { 1, "blau" },
            { 2, "grün" },
            { 3, "violett" },
            { 4, "rot" },
            { 5, "gelb" },
            { 6, "türkis" },
            { 7, "weiß" }
        };

        public static string? GetColor(int id)
        {
            return _colorMap.TryGetValue(id, out var color) ? color : null;
        }
    }
}
