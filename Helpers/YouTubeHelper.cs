namespace CISConnect.Helpers;

public static class YouTubeHelper
{
    public static string GetId(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return string.Empty;
        var value = url.Trim();

        if (value.Contains("/embed/", StringComparison.OrdinalIgnoreCase))
            return value.Split("/embed/", StringSplitOptions.RemoveEmptyEntries)
                        .Last().Split('?', '&', '/').FirstOrDefault() ?? string.Empty;

        if (value.Contains("youtu.be/", StringComparison.OrdinalIgnoreCase))
            return value.Split("youtu.be/", StringSplitOptions.RemoveEmptyEntries)
                        .Last().Split('?', '&', '/').FirstOrDefault() ?? string.Empty;

        if (value.Contains("watch?v=", StringComparison.OrdinalIgnoreCase))
            return value.Split("watch?v=", StringSplitOptions.RemoveEmptyEntries)
                        .Last().Split('&').FirstOrDefault() ?? string.Empty;

        if (value.Contains("/shorts/", StringComparison.OrdinalIgnoreCase))
            return value.Split("/shorts/", StringSplitOptions.RemoveEmptyEntries)
                        .Last().Split('?', '&', '/').FirstOrDefault() ?? string.Empty;

        return string.Empty;
    }

    public static string GetEmbedUrl(string url)
    {
        var id = GetId(url);
        return string.IsNullOrWhiteSpace(id) ? url : $"https://www.youtube-nocookie.com/embed/{id}";
    }

    public static string GetWatchUrl(string url)
    {
        var id = GetId(url);
        return string.IsNullOrWhiteSpace(id) ? url : $"https://www.youtube.com/watch?v={id}";
    }

    public static string GetThumbnailUrl(string url)
    {
        var id = GetId(url);
        return string.IsNullOrWhiteSpace(id) ? string.Empty : $"https://img.youtube.com/vi/{id}/hqdefault.jpg";
    }
}
