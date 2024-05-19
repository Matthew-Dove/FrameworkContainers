namespace FrameworkContainers.Network.HttpCollective.Models
{
    /// <summary>A general model to cover 200, 400, and 500 http status codes in custom ways.</summary>
    public readonly struct Http245
    {
        public Header[] Headers { get; }
        public int StatusCode { get; }
        public string Body { get; }

        internal Http245(Header[] headers, int statusCode, string body)
        {
            Headers = headers;
            StatusCode = statusCode;
            Body = body;
        }
    }

    /// <summary>Maps to a 100 range of http status codes.</summary>
    public enum StatusRange245
    {
        Http200,
        Http400,
        Http500
    }

    public static class Http245Extensions
    {
        // For direct status checks, and if statements.
        public static bool Is200(this Http245 http) => http.StatusCode >= 200 && http.StatusCode <= 299;
        public static bool Is400(this Http245 http) => http.StatusCode >= 400 && http.StatusCode <= 499;
        public static bool Is500(this Http245 http) => http.StatusCode >= 500 && http.StatusCode <= 599;

        // For pattern matches, and switch statements.
        public static StatusRange245 GetStatusRange(this Http245 http)
        {
            if (http.Is200()) return StatusRange245.Http200;
            if (http.Is400()) return StatusRange245.Http400;
            return StatusRange245.Http500;
        }
    }
}
