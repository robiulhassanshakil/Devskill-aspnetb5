using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class MyORM<T>where T :IEntity
    {
        private readonly SqlConnection _sqlConnection;


        public MyORM(SqlConnection connection)
        {
            _sqlConnection = connection;
        }

        public MyORM(string connectionString):this(new SqlConnection(connectionString))
        {
            
        }
        public void Insert(T item)
        {
            var sql = new StringBuilder("Insert into ");
            var type = item.GetType();
            var properties = type.GetProperties();
            sql.Append(type.Name);
            sql.Append('(');
            for (int i = 1; i < properties.Length; i++)
            {
                sql.Append(' ').Append(properties[i].Name).Append(',');
            }

            sql.Remove(sql.Length - 1, 1);
            sql.Append(") values (");

            for (int i = 1; i < properties.Length; i++)
            {
                sql.Append('@').Append(properties[i].Name).Append(',');
            }
            

            sql.Remove(sql.Length - 1, 1);
            sql.Append(");");
            var query = sql.ToString();
            var command = new SqlCommand(query, _sqlConnection);

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(item));
            }

            if(_sqlConnection.State == System.Data.ConnectionState.Closed)
            _sqlConnection.Open();
            command.ExecuteNonQuery();

        }

        public void Update(T item)
        {

        }

        public void Delete(T item)
        {

        }

        public void Delete(int id)
        {

        }

        public void GetById(int id)
        {

        }

        public void GetAll()
        {

        }


    }
}
