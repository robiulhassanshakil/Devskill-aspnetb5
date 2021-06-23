using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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
            using SqlCommand command = new SqlCommand(query, _sqlConnection);

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
            using SqlCommand command = new SqlCommand(query,_sqlConnection);
            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(item));
            }

            if (_sqlConnection.State==System.Data.ConnectionState.Closed)
                _sqlConnection.Open();

            command.ExecuteNonQuery();
            ;

            Console.WriteLine("update successful");


        }

        public void Delete(T item)
        {

            if (item.Id!=null)
            {
               Delete(item.Id);    
            }
            
        }

        public void Delete(int id)
        {
            var sql = new StringBuilder("Delete From ");
            var obj = Activator.CreateInstance(typeof(T));

            var tableobj = obj.GetType();
            sql.Append(tableobj.Name);
            sql.Append(" where ");
            sql.Append("ID = ");
            sql.Append(id);
            sql.Append(";");
            var query = sql.ToString();

            using SqlCommand command = new SqlCommand(query, _sqlConnection);

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();

            command.ExecuteNonQuery();

            

            Console.WriteLine("Delete Successful.");

        }

        public void GetById(int id)
        {
            var sql = new StringBuilder("");
        }

        public void GetAll()
        {
            var sql = new StringBuilder("Select *From ");
            var obj = Activator.CreateInstance(typeof(T));
            var tableobj = obj.GetType();
            var properties = tableobj.GetProperties();
            var count = properties.Length;
            sql.Append(tableobj.Name);
            sql.Append(';');
            var query = sql.ToString();
            var coloumList =ReadOparation(query, _sqlConnection);


            foreach (var item in coloumList)
            {
                var objOfItem = item.GetType();
                var propertyOfItem = objOfItem.GetProperties();
                foreach (var propertyInfo in propertyOfItem)
                {
                    Console.WriteLine(propertyInfo.GetValue(item));
                }
            }


        }

        private  IList<T> ReadOparation(string sql, SqlConnection connection)
        {
            if (connection.State==System.Data.ConnectionState.Closed)
                connection.Open();

            using SqlCommand command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            var objlist = new List<T>();
            var baseobject = typeof(T);
            while (reader.Read())
            {
                var obj =(T)Activator.CreateInstance(baseobject);
                var properties = baseobject.GetProperties();
                foreach (var property in properties)
                {
                    property.SetValue(obj,reader[property.Name]);
                }

                objlist.Add(obj);

            }

            return objlist;




        }

    }
}
