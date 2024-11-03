using System;

public class Program
{
    public static void Main(string[] args)
    {
        WriteEmptyLines();

        if (args.Length > 0 && args[0] == "help")
        {
            PrintHelp();
            return;
        }

        Console.Clear();
        //Example1 example1 = new Example1();
        //example1.Run();

        //Example2 example2 = new Example2();
        //example2.Run();

        BubbleSortExample bubbleSortExample = new BubbleSortExample();
        bubbleSortExample.Run();

        Console.ReadLine();

    }

    private static void PrintHelp()
    {
        Console.WriteLine("Данная программа содербит набор простейших примеров по работе с массивами");
    }

    private static void WriteEmptyLines(int lines = 3)
    {
        for (int i = 0; i < lines; i++)
        {
            Console.WriteLine();
        }
    }
}

public static class ArrayExtensions /*static -- обязательно*/ /*Extensions в имени - желательно -- соглашение по код стайлу*/
{
    public static void PrintArrayValues(this int[] array)//Метод расширения должен быть в публ.ст. /*static -- обязательно*/ /*this у аргумента -- обязательно*/
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    public static void PrintArrayValuesToLine(this int[] array)//Метод расширения должен быть в публ.ст. /*static -- обязательно*/ /*this у аргумента -- обязательно*/
    {
        /*
                1) Begin

                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write(array[i] + " ");
                }

                Console.WriteLine();

                1) End
                */

        Console.WriteLine(string.Join(", ", array));
    }

    //Пример использования
    private static void UsageExample()
    {
        int[] array1 = new int[10]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        };

        array1.PrintArrayValues();//Extensions usage like style
        ArrayExtensions.PrintArrayValues(array1); //Functions usage like style
    }
}

/// <summary>
/// Простой вывод в консоль
/// </summary>
public class Example1
{
    //Член экземпляра объекта
    private int objectInstanceMember = 0;

    //Член типа // статич. поля
    private static int staticMember = 0;//Разделяемые / общие данные. Те если изменить в одном месте - то значение изменится у всех.

    public static void RunStatic() // статич. функ.
    {
        staticMember = 1;
        //objectInstanceMember = 1; // Ошибка - не скомпилируется. Потому что нельзя получить доступ к члену экземпляра объекта из статической функции.

        Example1 example1 = new Example1();// но если внтури создать экз. об. то можно работать с его полями
        example1.objectInstanceMember = 5;
    }

    public void Run()
    {
        objectInstanceMember = 3;

        staticMember = 1;

        int[] array1 = new int[10]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

        int[] array2 =
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };
        //       
        Console.WriteLine($"значения массива {nameof(array1)}");
        PrintArrayValues(array1);
        Console.WriteLine($"значения массива {nameof(array2)}");
        PrintArrayValues(array2);

    }

    public void PrintArrayValues(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }
}

/// <summary>
/// Сравнить значения массивов
/// </summary>
public class Example2
{
    public void Run()
    {
        int[] array1 = new int[10]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

        int[] array2 =
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

        int[] array3 =
            {
                1, 2, 3, 4, 5
            };

        bool compareResult_1_2 = CompareArraysValues(array1, array2);
        array1.PrintArrayValues();
        Console.WriteLine();
        array2.PrintArrayValues();
        PrintResult(nameof(array1), nameof(array2), compareResult_1_2);

        bool compareResult_1_3 = CompareArraysValues(array1, array3);
        array1.PrintArrayValues();
        Console.WriteLine();
        array3.PrintArrayValues();
        PrintResult(nameof(array1), nameof(array3), compareResult_1_3);

        bool compareResult_2_3 = CompareArraysValues(array2, array3);
        array2.PrintArrayValues();
        Console.WriteLine();
        array3.PrintArrayValues();
        PrintResult(nameof(array2), nameof(array3), compareResult_2_3);

    }



    public bool CompareArraysValues(int[] array1, int[] array2)
    {
        int i = 0;

        if (array1.Length != array2.Length)
        {
            return false;
        }

        for (i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                return false;
            }
        }

        return true;

    }
    //Содержимое массива (имя 1го массива) (равно / не равно) содержимому массива (имя 2го массива))
    public void PrintResult(string array1Name, string array2Name, bool result)
    {
        string equals = result ? "равно" : "не равно";

        Console.WriteLine($"Содержимое массива {array1Name} {equals} содержимому массива  {array2Name}");


    }
}



public class BubbleSortExample
{
    public void Run()
    {
        int[] array = { 6, 9, 8, 7, 10, 5, 4, 3, 2, 1 };

        SortArray(array);
    }

    //От меньшего к большему
    private void SortArray(int[] array)
    {
        Console.WriteLine("Массив до сортировки");
        array.PrintArrayValuesToLine();
        Console.WriteLine();
        Console.WriteLine();

        for (int sortCycle = 0; sortCycle < array.Length; sortCycle++)
        {
            for (int i = 0; i < array.Length - (1 + sortCycle); i++)
            {
                if (array[i] > array[i + 1])
                {
                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                }
            }

            array.PrintArrayValuesToLine();
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("Массив после сортировки");
        array.PrintArrayValuesToLine();
    }
}