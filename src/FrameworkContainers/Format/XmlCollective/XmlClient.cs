﻿using FrameworkContainers.Format.XmlCollective.Models;

namespace FrameworkContainers.Format.XmlCollective
{
    /// <summary>Dependency inversion alterative to the static class.</summary>
    public interface IXmlClient
    {
        XmlMaybe Maybe { get; }
        XmlResponse Response { get; }
        T ToModel<T>(string xml);
        T ToModel<T>(string xml, XmlReadOptions options);
        string FromModel<T>(T model);
        string FromModel<T>(T model, XmlWriteOptions options);
    }

    public sealed class XmlClient : IXmlClient
    {
        public XmlMaybe Maybe => Xml.Maybe;
        public XmlResponse Response => Xml.Response;

        public T ToModel<T>(string xml) => Xml.ToModel<T>(xml);
        public T ToModel<T>(string xml, XmlReadOptions options) => Xml.ToModel<T>(xml, options);

        public string FromModel<T>(T model) => Xml.FromModel(model);
        public string FromModel<T>(T model, XmlWriteOptions options) => Xml.FromModel(model, options);
    }

    /// <summary>Dependency inversion alterative to the static class (for a single type).</summary>
    public interface IXmlClient<T>
    {
        XmlMaybe<T> Maybe { get; }
        XmlResponse<T> Response { get; }
        T ToModel(string xml);
        T ToModel(string xml, XmlReadOptions options);
        string FromModel(T model);
        string FromModel(T model, XmlWriteOptions options);
    }

    public sealed class XmlClient<T> : IXmlClient<T>
    {
        public XmlMaybe<T> Maybe => XmlMaybe<T>.Instance;
        public XmlResponse<T> Response => XmlResponse<T>.Instance;

        public T ToModel(string xml) => Xml.ToModel<T>(xml);
        public T ToModel(string xml, XmlReadOptions options) => Xml.ToModel<T>(xml, options);

        public string FromModel(T model) => Xml.FromModel(model);
        public string FromModel(T model, XmlWriteOptions options) => Xml.FromModel(model, options);
    }
}
