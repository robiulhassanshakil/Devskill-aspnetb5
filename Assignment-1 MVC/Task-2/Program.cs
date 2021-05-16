namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassWithPrivateMethod target = new ClassWithPrivateMethod();
            ReflectionUtility utility = new ReflectionUtility();
            utility.CallPrivate(target, "Print", new object[] { "Hello World" });
            
        }
    }
}
