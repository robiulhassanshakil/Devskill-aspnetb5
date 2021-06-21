using System;
using System.Data.SqlClient;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=DESKTOP-MP7KLAT\\SQLEXPRESS;Database=testdata;User Id=test;Password=123456;");

            var student1 = new Students() {Name = "riad",Weight = 100};

            var MyORM = new MyORM<Students>(connection);

            MyORM.Insert(student1);
        }
    }
}
