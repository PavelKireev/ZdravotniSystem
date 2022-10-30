namespace ZdravotniSystem.DB.Repository
{
    public interface IRepository<T>
    {
        List<T> FindAll();
        void Save(T entity);
        T GetOne(int id);
        void Update(T entity);
        void Delete(int id);
    }
}
