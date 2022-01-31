namespace Core_Portfolio.Interface
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepo<T> Entity { get; }
        void Save();
    }
}
