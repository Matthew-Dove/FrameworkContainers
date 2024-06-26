﻿using ContainerExpressions.Containers;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;

namespace FrameworkContainers.Format.XmlCollective
{
    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Maybe container.</summary>
    public sealed class XmlMaybe
    {
        internal static readonly XmlMaybe Instance = new XmlMaybe();

        private XmlMaybe() { }

        public Maybe<T> ToModel<T>(string xml)
        {
            var maybe = new Maybe<T>();

            try
            {
                var model = ExtensibleMarkupLanguage.XmlToModel<T>(xml);
                maybe = maybe.With(model);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, typeof(T), xml));
            }

            return maybe;
        }

        public Maybe<string> FromModel<T>(T model)
        {
            var maybe = new Maybe<string>();

            try
            {
                var xml = ExtensibleMarkupLanguage.ModelToXml<T>(model);
                maybe = maybe.With(xml);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, model));
            }

            return maybe;
        }
    }

    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Maybe container (for a single type).</summary>
    public sealed class XmlMaybe<T>
    {
        internal static readonly XmlMaybe<T> Instance = new XmlMaybe<T>();

        private XmlMaybe() { }

        public Maybe<T> ToModel(string xml) => XmlMaybe.Instance.ToModel<T>(xml);

        public Maybe<string> FromModel(T model) => XmlMaybe.Instance.FromModel<T>(model);
    }
}
