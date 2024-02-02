namespace DemoProject.Application.Interface.Repository
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        Task<IQueryable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
