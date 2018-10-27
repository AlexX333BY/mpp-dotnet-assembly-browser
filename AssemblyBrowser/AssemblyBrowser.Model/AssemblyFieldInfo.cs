using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Model
{
    public class AssemblyFieldInfo : IAccessable
    {
        protected readonly FieldInfo fieldInfo;

        public string Name => fieldInfo.Name;

        public string Type => fieldInfo.FieldType.Name;

        public bool IsPublic => fieldInfo.IsPublic;

        public bool IsProtected => fieldInfo.IsFamily;

        public bool IsInternal => fieldInfo.IsAssembly;

        public bool IsProtectedInternal => fieldInfo.IsFamilyOrAssembly;

        public bool IsPrivate => fieldInfo.IsPrivate;

        public bool IsPrivateProtected => fieldInfo.IsFamilyAndAssembly;

        public bool IsConst => fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;

        public bool IsReadonly => fieldInfo.IsInitOnly;

        public bool IsStatic => fieldInfo.IsStatic;

        public bool IsVolatile
        {
            get
            {
                foreach (Type modifier in fieldInfo.GetRequiredCustomModifiers())
                {
                    if (modifier == typeof(IsVolatile))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public AssemblyFieldInfo(FieldInfo fieldInfo)
        {
            this.fieldInfo = fieldInfo ?? throw new ArgumentException("Field info shouldn't be null");
        }
    }
}
