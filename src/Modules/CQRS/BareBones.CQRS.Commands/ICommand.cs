namespace BareBones.CQRS.Commands
{
    public interface ICommand
    {
        string UniqueId { get; }
    }
}
