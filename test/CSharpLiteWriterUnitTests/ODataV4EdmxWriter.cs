﻿using System;
using System.Linq;
using System.Text;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace CSharpLiteWriterUnitTests
{
    public static class ODataV4EdmxWriter
    {
        public static string ToEdmx(this OdcmModel odcmModel, bool addEdmxTag = false)
        {
            var sb = new StringBuilder();

            if (addEdmxTag) sb.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?><edmx:Edmx Version=\"4.0\" xmlns:edmx=\"http://docs.oasis-open.org/odata/ns/edmx\">");
            sb.AppendFormat("<edmx:DataServices>");
            if(odcmModel.Namespaces.Any()) sb.Append(odcmModel.Namespaces.Select(ToEdmx).Aggregate((c, n) => c + "\n" + n));
            sb.AppendFormat("</edmx:DataServices>");
            if (addEdmxTag) sb.AppendFormat("</edmx:Edmx>");

            return sb.ToString();
        }

        public static string ToEdmx(this OdcmNamespace odcmNamespace)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("<Schema Namespace=\"{0}\" xmlns=\"http://docs.oasis-open.org/odata/ns/edm\">", odcmNamespace.Name);
            if(odcmNamespace.Classes.Any()) sb.Append(odcmNamespace.Classes.Select(ToEdmx).Aggregate((c, n) => c + "\n" + n));
            sb.AppendFormat("</Schema>");

            return sb.ToString();
        }

        public static string ToEdmx(this OdcmClass odcmClass)
        {
            if (odcmClass is OdcmEntityClass)
            {
                return ((OdcmEntityClass) odcmClass).ToEdmx();
            }

            var sb = new StringBuilder();

            var tagName = GetTagName(odcmClass);

            sb.AppendFormat("<{0} Name=\"{1}\">", tagName, odcmClass.Name);
            if (odcmClass.Properties.Any()) sb.Append(odcmClass.Properties.Select(ToEdmx).Aggregate((c, n) => c + "\n" + n));
            sb.AppendFormat("</{0}>", tagName);

            return sb.ToString();
        }

        public static string ToEdmx(this OdcmEntityClass odcmClass)
        {
            if (odcmClass is OdcmMediaClass)
            {
                return ((OdcmMediaClass)odcmClass).ToEdmx();
            }

            var sb = new StringBuilder();

            var tagName = GetTagName(odcmClass);

            sb.AppendFormat("<{0} Name=\"{1}\">", tagName, odcmClass.Name);
            sb.Append(GetKeyNode(odcmClass));
            if (odcmClass.Properties.Any()) sb.Append(odcmClass.Properties.Select(ToEdmx).Aggregate((c, n) => c + "\n" + n));
            sb.AppendFormat("</{0}>", tagName);

            return sb.ToString();
        }

        public static string ToEdmx(this OdcmMediaClass odcmClass)
        {
            var sb = new StringBuilder();

            var tagName = GetTagName(odcmClass);

            sb.AppendFormat("<{0} Name=\"{1}\" HasStream=\"true\">", tagName, odcmClass.Name);
            sb.Append(GetKeyNode(odcmClass));
            if (odcmClass.Properties.Any()) sb.Append(odcmClass.Properties.Select(ToEdmx).Aggregate((c, n) => c + "\n" + n));
            sb.AppendFormat("</{0}>", tagName);

            return sb.ToString();
        }

        public static string ToEdmx(this OdcmEnum odcmEnum)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("<EnumType Name=\"{0}\">", odcmEnum.Name);
            if (odcmEnum.Members.Any()) sb.Append(odcmEnum.Members.Select(ToEdmx).Aggregate((c, n) => c + "\n" + n));
            sb.AppendFormat("</EnumType>");

            return sb.ToString();
        }

        private static string ToEdmx(OdcmEnumMember odcmEnumMember)
        {
            return String.Format("Member Name=\"{0}\"/>", odcmEnumMember.Name);
        }

        private static string GetKeyNode(OdcmEntityClass odcmClass)
        {
            if (!odcmClass.Key.Any())
                return string.Empty;

            var sb = new StringBuilder();

            sb.Append("<Key>");
            sb.Append(odcmClass.Key.Select(ToPropertyRef).Aggregate((c, n) => c + "\n" + n));
            sb.Append("</Key>");

            return sb.ToString();
        }

        private static string ToPropertyRef(OdcmProperty k)
        {
            return String.Format("<PropertyRef Name=\"{0}\"/>", k.Name);
        }

        private static string ToEdmx(this OdcmProperty odcmProperty)
        {
            var tagName = "Property";
            var typeAttributeName = "Type";
            var edmType = odcmProperty.Type.FullName;

            if (odcmProperty.Class.Kind == OdcmClassKind.Service)
            {
                if (odcmProperty.IsCollection)
                {
                    tagName = "EntitySet";
                    typeAttributeName = "EntityType";
                }
                else
                {
                    tagName = "Singleton";
                }
            }
            else if (odcmProperty.Class.Kind == OdcmClassKind.Entity)
            {
                if (odcmProperty.Type is OdcmClass && ((OdcmClass)odcmProperty.Type).Kind == OdcmClassKind.Entity)
                {
                    tagName = "NavigationProperty";

                    if (odcmProperty.IsCollection)
                        edmType = String.Format("Collection({0})", edmType);
                }
            }

            return String.Format("<{0} Name=\"{1}\" {2}=\"{3}\" />", tagName, odcmProperty.Name, typeAttributeName, edmType);
        }

        private static string GetTagName(OdcmClass odcmClass)
        {
            var tagName = string.Empty;

            switch (odcmClass.Kind)
            {
                case OdcmClassKind.Complex:
                    tagName = "ComplexType";
                    break;
                case OdcmClassKind.Entity:
                case OdcmClassKind.MediaEntity:
                    tagName = "EntityType";
                    break;
                case OdcmClassKind.Service:
                    tagName = "EntityContainer";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return tagName;
        }
    }
}
