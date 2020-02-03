namespace BareBones
{
    public interface IIdentifiable<out TKey>
    {
        TKey IdentityKey { get; }
    }
}
