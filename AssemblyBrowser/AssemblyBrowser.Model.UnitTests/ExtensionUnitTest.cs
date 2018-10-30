using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using AssemblyBrowser.Model.UnitTests.ExtensionMethodsAssembly;

namespace AssemblyBrowser.Model.UnitTests
{
    [TestClass]
    public class ExtensionUnitTest
    {
        AssemblyInfo assemblyInfo;
        string fullNamespace;

        [TestInitialize]
        public void Init()
        {
            assemblyInfo = new AssemblyInfo(Path.GetFullPath("AssemblyBrowser.Model.UnitTests.ExtensionMethodsAssembly.dll"));
            fullNamespace = "AssemblyBrowser.Model.UnitTests.ExtensionMethodsAssembly";
        }

        [TestMethod]
        public void ExtensionMethodsInExtendedType()
        {
            AssemblyDatatypeInfo extendedDatatype = null;
            foreach (AssemblyDatatypeInfo datatypeInfo in assemblyInfo.Namespaces[fullNamespace].Datatypes)
            {
                if (datatypeInfo.Name.Equals(nameof(IMyInterface)))
                {
                    extendedDatatype = datatypeInfo;
                }
            }
            Assert.AreEqual(Extension.ExtensionMethodsCount, extendedDatatype?.ExtensionMethods.Count);
        }

        [TestMethod]
        public void ExtensionMethodsNotInDeclaredType()
        {
            AssemblyDatatypeInfo declaredDatatype = null;
            foreach (AssemblyDatatypeInfo datatypeInfo in assemblyInfo.Namespaces[fullNamespace].Datatypes)
            {
                if (datatypeInfo.Name.Equals(nameof(Extension)))
                {
                    declaredDatatype = datatypeInfo;
                }
            }
            Assert.AreEqual(0, declaredDatatype?.ExtensionMethods.Count);
        }
    }
}
