using System;

namespace AssemblyBrowser.Model.UnitTests.TestAssembly
{
    public class TestType
    {
        public int publicField;
        protected bool protectedField;
        private DateTime privateField;

        internal bool IsTest => true;
        public int FieldCount => 3;
        public int PropertyCount => 6;
        public int MethodCount => 3;
        public int NamespaceCount => 1;
        public int TypeCount => 1;

        internal int InternalMethod(int parameter = 42)
        {
            return default;
        }

        internal protected bool InternalProtectedMethod()
        {
            return default;
        }

        protected private long ProtectedPrivateMethod()
        {
            return default;
        }
    }
}
