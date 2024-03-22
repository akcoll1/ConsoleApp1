using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Classes
{
    public class Car
    {
        //class members
        public string paintColour = "Red";
        public int maxSpeed = 200;
        public bool EV = false;
        public string make = "Ford";
        public string model = "Mustang";

        //method
        public void carProfile()
        {
            Console.WriteLine("This car is a " + make + " " + model + ", and it has a top speed of " + maxSpeed);
        }

    }
}
