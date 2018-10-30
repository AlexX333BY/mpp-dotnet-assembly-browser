using System;

namespace AssemblyBrowser.Model.UnitTests.ExtensionMethodsAssembly
{
    public interface IMyInterface
    {
        void MethodB();
    }

    public static class Extension
    {
        public static void MethodA(this IMyInterface myInterface, int i)
        {
            Console.WriteLine
                ("Extension.MethodA(this IMyInterface myInterface, int i)");
        }

        public static void MethodA(this IMyInterface myInterface, string s)
        {
            Console.WriteLine
                ("Extension.MethodA(this IMyInterface myInterface, string s)");
        }

        public static int ExtensionMethodsCount => 2;
    }
}
