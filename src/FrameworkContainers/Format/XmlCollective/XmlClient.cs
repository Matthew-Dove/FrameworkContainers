namespace FrameworkContainers.Format.XmlCollective
{
    /// <summary>Dependency injection alterative to the static class.</summary>
    public interface IXmlClient
    {
        XmlMaybe Maybe { get; }
        XmlResponse Response { get; }

        T ToModel<T>(string xml);
        string FromModel<T>(T model);
    }

    public sealed class XmlClient : IXmlClient
    {
        public XmlMaybe Maybe => Xml.Maybe;
        public XmlResponse Response => Xml.Response;

        public T ToModel<T>(string xml) => Xml.ToModel<T>(xml);
        public string FromModel<T>(T model) => Xml.FromModel(model);
    }

    /// <summary>Dependency injection alterative to the static class (for a single type).</summary>
    public interface IXmlClient<T>
    {
        XmlMaybe<T> Maybe { get; }
        XmlResponse<T> Response { get; }

        T ToModel(string xml);
        string FromModel(T model);
    }

    public sealed class XmlClient<T> : IXmlClient<T>
    {
        public XmlMaybe<T> Maybe => XmlMaybe<T>.Instance;
        public XmlResponse<T> Response => XmlResponse<T>.Instance;

        public T ToModel(string xml) => Xml.ToModel<T>(xml);
        public string FromModel(T model) => Xml.FromModel(model);
    }
}
