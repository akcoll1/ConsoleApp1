using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Classes
{
    internal class Cats
    {
        //Fields
        public string Name { get; set; }
        public int legs { get; set; }
        public bool fluffy { get; set; }
        public string favoriteFood { get; set; }

        public Cats(string CatsName, int NumberOfLegs, bool isItFluffy, string favoriteSnack) 
        { 
            Name = CatsName;
            legs = NumberOfLegs;
            fluffy = isItFluffy;
            favoriteFood = favoriteSnack;

            Console.WriteLine("Hi, My Cats name is " + CatsName + ", and he has " + NumberOfLegs + " Legs. Fluffy check: " + isItFluffy + ". Finally, his favorite snack is " + favoriteSnack);
        }
    }
}
