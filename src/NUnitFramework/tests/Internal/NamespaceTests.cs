// ***********************************************************************
// Copyright (c) 2018 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework.Api;

namespace NUnit.Framework.Internal
{
    [TestFixture]
    public class NamespaceTests
    {
        [Test]
        public void AllExportedNameSpacesAreNotSystem()
        {
#if !NETCOREAPP1_1
            var exportedTypes = Assembly.GetAssembly(typeof(FrameworkController)).GetExportedTypes();
#else
            var exportedTypes = typeof(FrameworkController).GetTypeInfo().Assembly.GetExportedTypes();
#endif

            var exportedTypesWhitelist = new[] 
            {
                typeof(System.Web.UI.ICallbackEventHandler)
            };

            Assert.Multiple(() =>
            {
                foreach (var type in exportedTypes.Except(exportedTypesWhitelist))
                {
                    Assert.IsFalse(
                        type.Namespace.StartsWith("System", StringComparison.CurrentCultureIgnoreCase),
                        $"Type {type.FullName} is in the System namespace but should not be.");
                }
            });
        }
    }
}
