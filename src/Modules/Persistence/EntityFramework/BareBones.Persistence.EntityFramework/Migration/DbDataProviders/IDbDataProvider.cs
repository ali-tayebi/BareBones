namespace BareBones.Persistence.EntityFramework.Migration
{
    public interface IDbDataProvider<out TData>
    {
        TData GetData();
    }
}
