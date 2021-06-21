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
            _sqlConnection.Close();

            Console.WriteLine("Insert Successful");

        }

        public void Update(T item)
        {
            var sql = new StringBuilder("Update ");
            var type = item.GetType();
            var properties = type.GetProperties();
            sql.Append(type.Name);
            sql.Append(' ');
            sql.Append("set ");
            for (int i = 1; i < properties.Length; i++)
            {
                sql.Append(properties[i].Name).Append('=').Append('@').Append(properties[i].Name).Append(',');
            }

            sql.Remove(sql.Length - 1, 1);
            sql.Append(" where ");
            sql.Append(properties[0].Name);
            sql.Append('=');
            sql.Append('@');
            sql.Append(properties[0].Name);
            sql.Append(";");

            var query = sql.ToString();
            var command = new SqlCommand(query,_sqlConnection);
            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(item));
            }

            if (_sqlConnection.State==System.Data.ConnectionState.Closed)
                _sqlConnection.Open();

            command.ExecuteNonQuery();
            _sqlConnection.Close();

            Console.WriteLine("update successful");


        }

        public void Delete(T item)
        {
            var type = item.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(item));
            }




        }

        public void Delete(int id)
        {
            var sql = new StringBuilder("Delete From ");
            var type = id.GetType();
            sql.Append(type.Name);
            sql.Append(" where ");
            sql.Append("ID = @");
            sql.Append(id);
            sql.Append(";");
            var query = sql.ToString();

            var command = new SqlCommand(query, _sqlConnection);
            command.Parameters.Add(id);


        }

        public void GetById(int id)
        {

        }

        public void GetAll()
        {

        }


    }
}
