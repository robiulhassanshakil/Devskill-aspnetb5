using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
    public abstract class Repository<TEntity, TKey, TContext> : IRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TContext : DbContext
    {
        protected TContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        protected int CommandTimeout { get; set; }

        public Repository(TContext context)
        {
            this.CommandTimeout = 300;
            this._dbContext = context;
            this._dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity);
        }

        public virtual async Task RemoveAsync(TKey id) => await RemoveAsync(_dbSet.Find(id));

        public virtual async Task RemoveAsync(TEntity entityToDelete) => await Task.Run(() =>
        {

            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {

                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        });

        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> filter) => await Task.Run(() => _dbSet.RemoveRange(_dbSet.Where(filter)));

        public virtual async Task EditAsync(TEntity entityToUpdate) => await Task.Run((Action)(() =>
        {
            _dbSet.Attach(entityToUpdate);

            _dbContext.Entry<TEntity>(entityToUpdate).State = EntityState.Modified;
        }));

        public virtual async Task<TEntity> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> source = _dbSet;
            if (filter != null)
                source = source.Where(filter);
            return await source.CountAsync();
        }

        public virtual async Task<IList<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> source = _dbSet;
            if (filter != null)
                source = source.Where(filter);
            return await source.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync() => await _dbSet.ToListAsync<TEntity>();

        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            int total = source1.Count();
            int totalDisplay = source1.Count();
            if (filter != null)
            {
                source1 = source1.Where(filter);
                totalDisplay = source1.Count();
            }
            if (include != null)
                source1 = include(source1);
            IList<TEntity> listAsync;
            if (orderBy != null)
            {
                IQueryable<TEntity> source2 = orderBy(source1).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    listAsync = await source2.AsNoTracking().ToListAsync();
                else
                    listAsync = await source2.ToListAsync();
            }
            else
            {
                IQueryable<TEntity> source3 = source1.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    listAsync = await source3.AsNoTracking().ToListAsync();
                else
                    listAsync = await source3.ToListAsync();
            }
            return (listAsync, total, totalDisplay);
        }

        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            int total = source1.Count();
            int totalDisplay = source1.Count();
            if (filter != null)
            {
                source1 = source1.Where(filter);
                totalDisplay = source1.Count();
            }
            if (include != null)
                source1 = include(source1);
            IList<TEntity> listAsync;
            if (orderBy != null)
            {
                IQueryable<TEntity> source2 = source1.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    listAsync = await source2.AsNoTracking().ToListAsync();
                else
                    listAsync = await source2.ToListAsync();
            }
            else
            {
                IQueryable<TEntity> source3 = source1.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    listAsync = await source3.AsNoTracking().ToListAsync();
                else
                    listAsync = await source3.ToListAsync();
            }
            return (listAsync, total, totalDisplay);
        }

        public virtual async Task<IList<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            if (filter != null)
                source1 = source1.Where(filter);
            if (include != null)
                source1 = include(source1);
            if (orderBy != null)
            {
                IOrderedQueryable<TEntity> source2 = orderBy(source1);
                return isTrackingOff ? await source2.AsNoTracking().ToListAsync() : await source2.ToListAsync();
            }
            return isTrackingOff ? await source1.AsNoTracking().ToListAsync() : await source1.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetDynamicAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            if (filter != null)
                source1 = source1.Where(filter);
            if (include != null)
                source1 = include(source1);
            if (orderBy != null)
            {
                IOrderedQueryable<TEntity> source2 = source1.OrderBy(orderBy);
                return isTrackingOff ? await source2.AsNoTracking().ToListAsync() : await source2.ToListAsync();
            }
            return isTrackingOff ? await source1.AsNoTracking().ToListAsync() : await source1.ToListAsync();
        }

        public virtual void Add(TEntity entity) => _dbSet.Add(entity);

        public virtual void Remove(TKey id) => Remove(_dbSet.Find(id));

        public virtual void Remove(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> filter) => _dbSet.RemoveRange(_dbSet.Where(filter));

        public virtual void Edit(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> dbSet = _dbSet;
            return filter == null ? dbSet.Count() : dbSet.Where(filter).Count();
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> source = _dbSet;
            if (filter != null)
                source = source.Where(filter);
            return source.ToList();
        }

        public virtual IList<TEntity> GetAll() => _dbSet.ToList();

        public virtual TEntity GetById(TKey id) => _dbSet.Find(id);

        public virtual (IList<TEntity> data, int total, int totalDisplay) Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            int num1 = source1.Count();
            int num2 = source1.Count();
            if (filter != null)
            {
                source1 = source1.Where(filter);
                num2 = source1.Count();
            }
            if (include != null)
                source1 = include(source1);
            if (orderBy != null)
            {
                IQueryable<TEntity> source2 = orderBy(source1).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return isTrackingOff ? (source2.AsNoTracking().ToList(), num1, num2) : (source2.ToList(), num1, num2);
            }
            IQueryable<TEntity> source3 = source1.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return isTrackingOff ? (source3.AsNoTracking().ToList(), num1, num2) : (source3.ToList(), num1, num2);
        }

        public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            int num1 = source1.Count();
            int num2 = source1.Count();
            if (filter != null)
            {
                source1 = source1.Where(filter);
                num2 = source1.Count();
            }
            if (include != null)
                source1 = include(source1);
            if (orderBy != null)
            {
                IQueryable<TEntity> source2 = source1.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return isTrackingOff ? (source2.AsNoTracking().ToList(), num1, num2) : (source2.ToList(), num1, num2);
            }
            IQueryable<TEntity> source3 = source1.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return isTrackingOff ? (source3.AsNoTracking().ToList(), num1, num2) : (source3.ToList(), num1, num2);
        }

        public virtual IList<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            if (filter != null)
                source1 = source1.Where(filter);
            if (include != null)
                source1 = include(source1);
            if (orderBy != null)
            {
                IOrderedQueryable<TEntity> source2 = orderBy(source1);
                return isTrackingOff ? source2.AsNoTracking().ToList() : source2.ToList();
            }
            return isTrackingOff ? source1.AsNoTracking().ToList() : source1.ToList();
        }

        public virtual IList<TEntity> GetDynamic(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false)
        {
            IQueryable<TEntity> source1 = _dbSet;
            if (filter != null)
                source1 = source1.Where(filter);
            if (include != null)
                source1 = include(source1);
            if (orderBy != null)
            {
                IOrderedQueryable<TEntity> source2 = source1.OrderBy(orderBy);
                return isTrackingOff ? source2.AsNoTracking().ToList() : source2.ToList();
            }
            return isTrackingOff ? source1.AsNoTracking().ToList() : source1.ToList();
        }

        protected virtual IDictionary<string, object> ExecuteStoredProcedure(
          string storedProcedureName,
          IDictionary<string, object> parameters = null,
          IDictionary<string, Type> outParameters = null)
        {
            DbCommand dbNull = ConvertNullToDbNull(CreateCommand(storedProcedureName, parameters, outParameters));
            bool flag = false;
            if (dbNull.Connection.State == ConnectionState.Closed)
            {
                dbNull.Connection.Open();
                flag = true;
            }
            try
            {
                dbNull.ExecuteNonQuery();
            }
            finally
            {
                if (flag)
                    dbNull.Connection.Close();
            }
            return CopyOutParams(dbNull, outParameters);
        }

        protected virtual async Task<IDictionary<string, object>> ExecuteStoredProcedureAsync(
          string storedProcedureName,
          IDictionary<string, object> parameters = null,
          IDictionary<string, Type> outParameters = null)
        {
            DbCommand command = CreateCommand(storedProcedureName, parameters, outParameters);
            command = ConvertNullToDbNull(command);
            bool connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
                connectionOpened = true;
            }
            try
            {
                int num = await command.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connectionOpened)
                    await command.Connection.CloseAsync();
            }
            IDictionary<string, object> dictionary = CopyOutParams(command, outParameters);
            command = null;
            return dictionary;
        }

        protected virtual async Task<(IList<TReturn> result, IDictionary<string, object> outValues)> QueryWithStoredProcedureAsync<TReturn>(
          string storedProcedureName,
          IDictionary<string, object> parameters = null,
          IDictionary<string, Type> outParameters = null)
          where TReturn : class, new()
        {
            DbCommand command = CreateCommand(storedProcedureName, parameters, outParameters);
            bool connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
                connectionOpened = true;
            }
            IList<TReturn> result = null;
            try
            {
                result = await ExecuteQueryAsync<TReturn>(command);
            }
            finally
            {
                if (connectionOpened)
                    await command.Connection.CloseAsync();
            }
            (IList<TReturn>, IDictionary<string, object>) valueTuple = (result, CopyOutParams(command, outParameters));
            command = null;
            result = null;
            return valueTuple;
        }

        protected virtual async Task<TReturn> ExecuteScalarAsync<TReturn>(
          string storedProcedureName,
          IDictionary<string, object> parameters = null)
        {
            DbCommand command = CreateCommand(storedProcedureName, parameters);
            bool connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
                connectionOpened = true;
            }
            TReturn result;
            try
            {
                result = await ExecuteScalarAsync<TReturn>(command);
            }
            finally
            {
                if (connectionOpened)
                    await command.Connection.CloseAsync();
            }
            TReturn @return = result;
            command = null;
            result = default;
            return @return;
        }

        private DbCommand CreateCommand(
          string storedProcedureName,
          IDictionary<string, object> parameters = null,
          IDictionary<string, Type> outParameters = null)
        {
            DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = CommandTimeout;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                    command.Parameters.Add(CreateParameter(parameter.Key, parameter.Value));
            }
            if (outParameters != null)
            {
                foreach (KeyValuePair<string, Type> outParameter in outParameters)
                    command.Parameters.Add(CreateOutputParameter(outParameter.Key, outParameter.Value));
            }
            return command;
        }

        private DbParameter CreateParameter(string name, object value) => new SqlParameter(name, CorrectSqlDateTime(value));

        private DbParameter CreateOutputParameter(string name, DbType dbType)
        {
            SqlParameter outputParameter = new SqlParameter(name, CorrectSqlDateTime(dbType));
            outputParameter.Direction = ParameterDirection.Output;
            return outputParameter;
        }

        private DbParameter CreateOutputParameter(string name, Type type)
        {
            SqlParameter outputParameter = new SqlParameter(name, GetDbTypeFromType(type));
            outputParameter.Direction = ParameterDirection.Output;
            return outputParameter;
        }

        private SqlDbType GetDbTypeFromType(Type type)
        {
            if (type == typeof(int) || type == typeof(uint) || type == typeof(short) || type == typeof(ushort))
                return SqlDbType.Int;
            if (type == typeof(long) || type == typeof(ulong))
                return SqlDbType.BigInt;
            if (type == typeof(double) || type == typeof(decimal))
                return SqlDbType.Decimal;
            if (type == typeof(string))
                return SqlDbType.NVarChar;
            if (type == typeof(DateTime))
                return SqlDbType.DateTime;
            if (type == typeof(bool))
                return SqlDbType.Bit;
            if (type == typeof(Guid))
                return SqlDbType.UniqueIdentifier;
            int num = type == typeof(char) ? 1 : 0;
            return SqlDbType.NVarChar;
        }

        private object ChangeType(Type propertyType, object itemValue)
        {
            switch (itemValue)
            {
                case DBNull _:
                    return null;
                case decimal _:
                    if (propertyType == typeof(double))
                        return Convert.ToDouble(itemValue);
                    break;
            }
            return itemValue;
        }

        private object CorrectSqlDateTime(object parameterValue) => parameterValue != null && parameterValue.GetType().Name == "DateTime" && Convert.ToDateTime(parameterValue) < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : parameterValue;

        private async Task<IList<TReturn>> ExecuteQueryAsync<TReturn>(DbCommand command)
        {
            var reader = await command.ExecuteReaderAsync();
            List<TReturn> result = new List<TReturn>();
            while (true)
            {
                if (await reader.ReadAsync())
                {
                    var type = typeof(TReturn);
                    var obj = type.GetConstructor(new Type[0]).Invoke(new object[0]);
                    for (int ordinal = 0; ordinal < reader.FieldCount; ++ordinal)
                    {
                        var property = type.GetProperty(reader.GetName(ordinal));
                        property.SetValue(obj, ChangeType(property.PropertyType, reader.GetValue(ordinal)));
                    }
                    result.Add((TReturn)obj);
                }
                else
                    break;
            }
            IList<TReturn> returnList = result;

            reader = null;
            result = null;

            return returnList;
        }

        private async Task<TReturn> ExecuteScalarAsync<TReturn>(DbCommand command)
        {
            command = ConvertNullToDbNull(command);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            var obj = await command.ExecuteScalarAsync();

            return obj != DBNull.Value ? (TReturn)obj : default;
        }

        private DbCommand ConvertNullToDbNull(DbCommand command)
        {
            for (int index = 0; index < command.Parameters.Count; ++index)
            {
                if (command.Parameters[index].Value == null)
                    command.Parameters[index].Value = DBNull.Value;
            }
            return command;
        }

        private IDictionary<string, object> CopyOutParams(
          DbCommand command,
          IDictionary<string, Type> outParameters)
        {
            Dictionary<string, object> dictionary = null;
            if (outParameters != null)
            {
                dictionary = new Dictionary<string, object>();
                foreach (KeyValuePair<string, Type> outParameter in outParameters)
                    dictionary.Add(outParameter.Key, command.Parameters[outParameter.Key].Value);
            }
            return dictionary;
        }
    }
}
