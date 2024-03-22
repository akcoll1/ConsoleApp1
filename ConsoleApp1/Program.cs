using System;
using System.Diagnostics;
using ConsoleApp1.Classes;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var num1 = 1;
            var num2 = 2;
            var num3 = 3;
            var num4 = 4;
            var num5 = 5;

            int[] numbers = {num1, num2, num3, num4, num5};
            string[] names = {"Adam", "Aimee", "Lewis", "Gareth", "Sarah"};

            if (num1 + num2 > num5)
            {
                Console.WriteLine("Thats a big number");
            }
            else if (num1 + num2 < num5)
            {
                Console.WriteLine("Thats a small number");
            }
            else
            {
                Console.WriteLine("Thats an average number");
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
            for(int i = 0;i < names.Length; i++)
            {
                Console.WriteLine(names[i]);
            }
            foreach (int i in numbers)
            {
                Console.WriteLine(i);
            }
            foreach(string i in names)
            {
                Console.WriteLine(i);
            }

            Car myCar = new Car();
            Car myCar2 = new Car();
            myCar2.paintColour = "Orange";
            Console.WriteLine(myCar.paintColour);
            Console.WriteLine(myCar2.paintColour);
            myCar.carProfile();
            Cats Korg = new Cats("Korg", 4, true, "Duck Treats");
            Cats Miek = new Cats("Miek", 4, false, "Lick e Licks");

        }
    }
}
