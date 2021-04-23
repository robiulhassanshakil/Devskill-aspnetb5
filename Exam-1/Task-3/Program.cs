using System;
using System.Reflection;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Type X = typeof(BaseModel);
            Type[] Model = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in Model)
            {
                if (type.IsAnsiClass)
                {
                    Console.WriteLine(type.Name);
                }
                
            }
        }
    }
}
