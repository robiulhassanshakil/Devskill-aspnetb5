using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;


namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> list1 = new List<Student>()
            {
                new Student {Name = "Shakil", Age = 25},
                new Student {Name = "Mashuq", Age = 20},
                new Student {Name = "Robin", Age = 30}
            };

            List<Student> list2 = new List<Student>()
            {
                new Student {Name = "Salma", Age = 25},
                new Student {Name = "Asma", Age = 20},
                new Student {Name = "Papia", Age = 30}
            };

            List <string> result = list1.Concat(list2)
                .OrderBy(x => x.Name)
                .OrderBy(x => x.Age)
                .Select(g => g.Name).ToList();

            foreach (var student in result)
            {
                Console.WriteLine(student);
            }
        }



    }
}

