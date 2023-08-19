namespace What2Watch.Dal.Repos.Base;

public abstract class BaseRepo<T> : BaseViewRepo<T>, IBaseRepo<T> where T : class, new()
{
    protected BaseRepo(ApplicationDbContext context) : base(context) { }

    protected BaseRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options))
    {
    }

    public virtual T Find(int? id)
        => Table.Find(id);

    public virtual void ExecuteParameterizedQuery(string sql, object[] sqlParametersObjects)
        => Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);

    public virtual int Add(T entity, bool persist = true)
    {
        Table.Add(entity);
        return persist ? SaveChanges() : 0;
    }

    public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.AddRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public virtual int Update(T entity, bool persist = true)
    {
        Table.Update(entity);
        return persist ? SaveChanges() : 0;
    }

    public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.UpdateRange(entities);
        return persist ? SaveChanges() : 0;
    }


    public virtual int Delete(T entity, bool persist = true)
    {
        Table.Remove(entity);
        return persist ? SaveChanges() : 0;
    }

    public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.RemoveRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public int SaveChanges()
    {
        try
        {
            return Context.SaveChanges();
        }
        catch (Exception ex)
        {
           
            throw;
        }
       
    }
}
