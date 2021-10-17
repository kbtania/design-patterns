using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program
    {
        public interface ITargetOrder
        {
            void MakeOrder();
        }
       
        public class McDonalds
        {
            public List<List<int>> order;

            public McDonalds(List<List<int>> order)
            {
                this.order = order;
            }
            public void McDonaldsOrder()
            {
                foreach (var o in order)
                {
                    Console.WriteLine($"You ordered food at MacDonalds with code: {o[0]}, quantity: {o[1]}");
                }
            }
        }
        public class JapaneseRest
        {
            public List<int> order_food_code;
            public List<int> order_food_quantity;

            public JapaneseRest(List<int> order_food_code, List<int> order_food_quantity) 
            { 
                this.order_food_code = order_food_code;
                this.order_food_quantity = order_food_quantity;
            }
            public void JapaneseOrder()
            {
                for (int i = 0; i < order_food_code.Count; i+=2)
                {
                    Console.WriteLine($"You ordered food at Japanese Rest. with code: {order_food_code[i]}, " +
                        $"quantity: {order_food_quantity[i]}");
                }
            }
        }
        public class UkrainianRest
        {
            public List<int> order;
            public UkrainianRest(List<int> order)
            {
                this.order = order;
            }
            public void UkrainianOrder()
            {
                var grouped_order = order.GroupBy(i => i);
                foreach (var o in grouped_order)
                {
                    Console.WriteLine($"You ordered food at Ukrainian Rest. with code: {o.Key}, " +
                        $"quantity: {o.Count()}");
                }
            }
        }

        public class AdapterOrder : ITargetOrder
        {
            public int foodType;
            public List<List<int>> order;
            public AdapterOrder(int foodType, List<List<int>> order)
            {
                this.foodType = foodType;
                this.order = order;
            }

            public void MakeOrder()
            {
                if (foodType == 1)
                {
                    McDonalds mac = new McDonalds(order);
                    mac.McDonaldsOrder();
                }
                else if (foodType == 2)
                {
                    List<int> codes = new List<int> { };
                    List<int> quantity = new List<int> { };
                   
                    for (int i = 0; i < order.Count; i++)
                    {
                        for (int j = 0; j < order[0].Count; j++)
                        {
                            codes.Add(order[i][0]);
                            quantity.Add(order[i][1]); 
                        }
                    }
                    JapaneseRest jap = new JapaneseRest(codes, quantity);
                    jap.JapaneseOrder();
                }
                else if (foodType == 3)
                {
                    List<int> adaptedOrder = new List<int> { };
                    for (int i = 0; i < order.Count; i++)
                    {
                        for (int j = 0; j < order[i][1]; j++)
                        {
                            adaptedOrder.Add(order[i][0]);
                        }
                    }
                    UkrainianRest ukr = new UkrainianRest(adaptedOrder);
                    ukr.UkrainianOrder();
                }
                else
                {
                    Console.WriteLine("Try again!");
                }
            }
        }
        static void Main(string[] args)
        {
            // Testing Example

            //AdapterOrder usersOrder = new AdapterOrder(foodType, order);
            //usersOrder.MakeOrder();

            //List<List<int>> orderExample1 = new List<List<int>> { new List<int> { 123, 3 }, 
            //                                                new List<int>{ 500, 1 }, new List<int> { 42, 2 } };
            //int foodTypeExample = 1;
            //AdapterOrder adapterExample = new AdapterOrder(foodTypeExample, orderExample1);
            //adapterExample.MakeOrder();

            Console.WriteLine("What would you like to order?\n 1 - Fast Food at McDonalds " +
                "\n 2 - Sushi at Japanese Restaurant \n 3 - Ukrainian Food at Family Restaurant");
            int foodType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many different items from menu would you like to order?"); 
            int itemsCount = Convert.ToInt32(Console.ReadLine());
            List<List<int>> order = new List<List<int>> { };

            for (int i = 0; i < itemsCount; i++)
            {
                Console.WriteLine("Enter the code of dish:");
                int foodCode = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Enter the quantity of {foodCode} dish:");
                int foodQuantity = Convert.ToInt32(Console.ReadLine());
                order.Add(new List<int> { foodCode, foodQuantity });
            }
            
        }
    }
}
