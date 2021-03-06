﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using Xunit;

namespace CSharpLiteWriterUnitTests
{
    public class Given_an_OdcmClass_Entity_Abstract : EntityTestBase
    {
        
        public Given_an_OdcmClass_Entity_Abstract()
        {
            base.Init(m => m.Namespaces[0].Classes.First().IsAbstract = true);
        }

        [Fact]
        public void The_Concrete_class_is_abstract()
        {
            ConcreteType.IsAbstract.Should().BeTrue("Because the Odcm class is abstract.");
        }

        [Fact]
        public void The_Fetcher_interface_does_not_expose_an_ExecuteAsync_Method()
        {
            FetcherInterface.Should()
                .NotHaveMethod("ExecuteAsync", "Because abstract Entities cannot be queried");
        }

        [Fact]
        public void The_Fetcher_interface_does_not_expose_an_Expand_Method()
        {
            FetcherInterface.Should()
                .NotHaveMethod("Expand", "Because abstract Entities cannot be queried");
        }

        [Fact]
        public void The_Fetcher_interface_does_not_expose_an_UpdateAsync_Method()
        {
            FetcherInterface.Should()
                .NotHaveMethod("UpdateAsync", "Because abstract Entities cannot be updated");
        }

        [Fact]
        public void The_Fetcher_interface_does_not_expose_a_DeleteAsync_Method()
        {
            FetcherInterface.Should()
                .NotHaveMethod("DeleteAsync", "Because abstract Entities cannot be deleted");
        }

        [Fact]
        public void The_Fetcher_interface_does_not_expose_a_SaveChangesAsync_Method()
        {
            FetcherInterface.Should()
                .NotHaveMethod("SaveChangesAsync", "Because abstract Entities cannot be changed");
        }

        [Fact]
        public void The_Fetcher_class_does_not_expose_an_ExecuteAsync_Method()
        {
            FetcherType.Should()
                .NotHaveMethod("ExecuteAsync", "Because abstract Entities cannot be queried");
        }

        [Fact]
        public void The_Fetcher_class_does_not_expose_an_Expand_Method()
        {
            FetcherType.Should()
                .NotHaveMethod("Expand", "Because abstract Entities cannot be queried");
        }

        [Fact]
        public void The_Fetcher_class_does_not_expose_an_UpdateAsync_Method()
        {
            FetcherType.Should()
                .NotHaveMethod("UpdateAsync", "Because abstract Entities cannot be updated");
        }

        [Fact]
        public void The_Fetcher_class_does_not_expose_a_DeleteAsync_Method()
        {
            FetcherType.Should()
                .NotHaveMethod("DeleteAsync", "Because abstract Entities cannot be deleted");
        }

        [Fact]
        public void The_Fetcher_class_does_not_expose_a_SaveChangesAsync_Method()
        {
            FetcherType.Should()
                .NotHaveMethod("SaveChangesAsync", "Because abstract Entities cannot be changed");
        }
    }
}
