namespace AssemblyBrowser.Model
{
    public interface IAccessable
    {
        bool IsPublic
        { get; }

        bool IsProtected
        { get; }

        bool IsInternal
        { get; }

        bool IsProtectedInternal
        { get; }

        bool IsPrivate
        { get; }

        bool IsPrivateProtected
        { get; }
    }
}
