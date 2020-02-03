namespace BareBones.CQRS
{
    public interface ICommand
    {
        string UniqueId { get; }
    }
}
