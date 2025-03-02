namespace Delegate_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var example1 = new DelegateExample1();
            example1.CallExample();
            example1.CallExample2();
            example1.CallExample3();

            var example2 = new DelegateExample2();
            example2.CallExample();

            var callbackExample = new CallbackExample();
            callbackExample.CallExample();

            LambdaExample lambdaExample = new LambdaExample();
            lambdaExample.CallExample();
            lambdaExample.CallExample();

            FluentBuilderUsage fluentBuilderUsage = new FluentBuilderUsage();
            fluentBuilderUsage.Run();
        }
    }

    
}
