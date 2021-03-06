﻿using System.Collections.Generic;

namespace Vipr.Writer.CSharp.Settings
{
    public class CSharpWriterSettings
    {
        public CSharpWriterSettings()
        {
            OdcmNamespaceToProxyNamespace = new Dictionary<string, string>();
            OdcmClassNameToProxyClassName = new Dictionary<string, IDictionary<string, string>>();
            MediaEntityAddAsyncVisibility = Visibility.Public;
        }

        public IDictionary<string, string> OdcmNamespaceToProxyNamespace { get; set; }

        public IDictionary<string, IDictionary<string, string>> OdcmClassNameToProxyClassName { get; set; }

        public string NamespacePrefix { get; set; }

        public bool OmitUpcastMethods { get; set; }
        
        public bool ForcePropertyPascalCasing { get; set; }

        public Visibility MediaEntityAddAsyncVisibility { get; set; }
        public bool OmitMethodNamespaceInUriPaths { get; set; }
    }
}
