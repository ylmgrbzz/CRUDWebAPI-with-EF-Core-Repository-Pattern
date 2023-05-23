namespace CRUDWebAPI.Core
{
    public interface IUnıtOfWork
    {
        IDriverRepository Drivers { get; }
        Task CompleteAsync();
        //Task<bool> SaveAsync();
    }
}
