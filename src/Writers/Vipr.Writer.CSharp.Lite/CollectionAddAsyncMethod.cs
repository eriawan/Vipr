﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Vipr.Core.CodeModel;

namespace Vipr.Writer.CSharp.Lite
{
    public class CollectionAddAsyncMethod : Method
    {
        public CollectionAddAsyncMethod(OdcmClass odcmClass)
        {
            Name = "Add" + odcmClass.Name + "Async";

            Parameters = new[]
            {
                new Parameter(new Type(NamesService.GetConcreteInterfaceName(odcmClass)), "item"),
                new Parameter(new Type(NamesService.GetPrimitiveTypeName("Boolean")), "deferSaveChanges", "false")
            };

            ReturnType = new Type(Identifier.Task);
        }
    }
}
