using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Program
    {
        //class Number
        //{
        //    public string number;
        //    public Number(string num)
        //    {
        //        this.number = num;
        //    }
        //    public void CreateNum()
        //    {
        //        var factory = new FlyweightFactory();

        //        //var flyweigt = factory.GetFlyweight(num);

        //        for (int i = 0; i < number.Length; i++)
        //        {
        //            var a = Convert.ToInt32(System.Char.GetNumericValue(number[i]));
                    
        //        }
                
        //    }
        //}

        public abstract class Flyweight
        {
            public abstract void Display();
        }

        public class ConcreteFlyweight : Flyweight
        {
            private string key;
            //private int key;
            private List<List<string>> symbols = new List<List<string>> { 
                new List<string>{"*****", "*   *", "*   *", "*   *", "*****"},
                new List<string>{ " **", "  *", "  *", "  *", "*****" },
                new List<string>{"*****", "   * ", "  *", " *", "*****"},
                new List<string>{ "*****", "    *","*****", "    *", "*****" },
                new List<string>{ "*   *", "*   *", "*****", "    *", "    *" },
                new List<string>{ "*****", "*", "*****", "    *", "*****" },
                new List<string>{"*****", "*", "*****", "*   *", "*****"},
                new List<string>{ "*****", "    *", "    *", "    *", "    *" },
                new List<string>{ "*****", "*   *", "*****", "*   *", "*****" },
                new List<string>{ "*****", "*   *", "*****", "    *", "*****" }
            };
            
            public ConcreteFlyweight(string key)
            {
                this.key = key;
            }
            public override void Display()
            {
                List<int> intList = new List<int> { };
                for (int i = 0; i < key.Length; i++)
                {
                    intList.Add(Convert.ToInt32(System.Char.GetNumericValue(key[i])));
                }


                for (int j = 0; j < intList.Count(); j++)
                {
                    for (int i = 0; i < symbols[0].Count; i++)
                    {
                        Console.WriteLine($"{symbols[intList[j]][i]}");
                    }
                    Console.WriteLine("");
                }

            }
        }

        public class FlyweightFactory
        {
            private Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();
            public Flyweight GetFlyweight(string key)
            {
                Flyweight flyweight = null;
                if (flyweights.ContainsKey(key))
                {
                    flyweight = flyweights[key];
                } 
                else
                {
                    flyweight = new ConcreteFlyweight(key);
                    flyweights.Add(key, flyweight);
                }
                return flyweight;
            }
        }

        static void Main(string[] args)
        {
            var factory = new FlyweightFactory();
            
            var flyweigt = factory.GetFlyweight("337");
            var flyweigt2 = factory.GetFlyweight("144");
            flyweigt.Display();
            flyweigt2.Display();
        }
    }
}

 
