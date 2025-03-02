using Evensts_Case1;
using Evensts_Case2;
using Evensts_Case3;

namespace Delegate_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //
            //var example1 = new DelegateExample1();
            //example1.CallExample();
            //example1.CallExample2();
            //example1.CallExample3();
            //
            //var example2 = new DelegateExample2();
            //example2.CallExample();
            //
            //var callbackExample = new CallbackExample();
            //callbackExample.CallExample();
            //
            //LambdaExample lambdaExample = new LambdaExample();
            //lambdaExample.CallExample();
            //lambdaExample.CallExample();
            //
            //FluentBuilderUsage fluentBuilderUsage = new FluentBuilderUsage();
            //fluentBuilderUsage.Run();

            //PersonController controller = new PersonController();
            //controller.LiveDay();
            //controller.LiveDay();
            //controller.BecameUgly();
            //controller.LiveDay();

            //var appleCaseRunner1 = new Evensts_Case1.AppleCaseRunner();
            //appleCaseRunner1.Run();

            //var appleCaseRunner2 = new Evensts_Case2.AppleCaseRunner();
            //appleCaseRunner2.Run();

            var appleCaseRunner3 = new Evensts_Case3.AppleCaseRunner();
            appleCaseRunner3.Run();
        }
    }

    
}
