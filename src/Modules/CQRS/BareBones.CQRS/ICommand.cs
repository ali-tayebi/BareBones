namespace BareBones.CQRS
{
    public interface ICommand
    {
        string IdentityKey { get; }
    }
}
