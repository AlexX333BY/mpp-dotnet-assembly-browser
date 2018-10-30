using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser.Model.UnitTests.TestAssembly;
using System.IO;

namespace AssemblyBrowser.Model.UnitTests
{
    [TestClass]
    public class SimpleTypeUnitTests
    {
        TestType typeInstance;
        AssemblyInfo assemblyInfo;
        string fullNamespace;

        [TestInitialize]
        public void Init()
        {
            typeInstance = new TestType();
            assemblyInfo = new AssemblyInfo(Path.GetFullPath("AssemblyBrowser.Model.UnitTests.TestAssembly.dll"));
            fullNamespace = "AssemblyBrowser.Model.UnitTests.TestAssembly";
        }

        [TestMethod]
        public void NamespaceCountTest()
        {
            Assert.AreEqual(typeInstance.NamespaceCount, assemblyInfo.Namespaces.Count);
        }

        [TestMethod]
        public void TypesCountTest()
        {
            string key = nameof(AssemblyBrowser.Model.UnitTests.TestAssembly);
            Assert.AreEqual(typeInstance.TypeCount, assemblyInfo.Namespaces[fullNamespace].Datatypes.Count);
        }

        [TestMethod]
        public void FieldsCountTest()
        {
            Assert.AreEqual(typeInstance.FieldCount, assemblyInfo.Namespaces[fullNamespace].Datatypes[0].Fields.Count);
        }

        [TestMethod]
        public void PropertiesCountTest()
        {
            Assert.AreEqual(typeInstance.PropertyCount, assemblyInfo.Namespaces[fullNamespace].Datatypes[0].Properties.Count);
        }

        [TestMethod]
        public void MethodCountTest()
        {
            /* because of inherited Object methods */
            Assert.IsTrue(assemblyInfo.Namespaces[fullNamespace].Datatypes[0].Methods.Count >= typeInstance.MethodCount);
        }
    }
}
