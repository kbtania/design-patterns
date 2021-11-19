using System;
using System.Collections.Generic;

namespace Lab5
{
    class Program
    {
        public interface IComponent
        {
            void Accept(IVisitor visitor);
        }


        public class Person
        {
            public string fullname;
            public string gender;
            public DateTime date_of_birth;
            public Person(string fullname, string gender, DateTime date_of_birth)
            {
                this.fullname = fullname;
                this.gender = gender;
                this.date_of_birth = date_of_birth;
            }
            public override string ToString()
            {
                return $"{fullname}, {gender}, {date_of_birth.Date.ToString("dd/MM/yyyy")}";
            }
        }

        public class PrivateHouse: IComponent
        {
            public List<Person> residents;
            public PrivateHouse(List<Person> residents)
            {
                this.residents = residents;
            }
            public void Accept(IVisitor visitor)
            {
                visitor.VisitPrivateHouse(this);
            }
        }
        

        public class Apartment : IComponent
        {
            public List<Person> residents;
            public Apartment(List<Person> residents)
            {
                this.residents = residents;
            }
            public void Accept(IVisitor visitor)
            {
                visitor.VisitApartment(this);
            }
        }

        public class ApartmentBuilding
        {
            public List<Apartment> apartments;
            public ApartmentBuilding(List<Apartment> apartments)
            {
                this.apartments = apartments;
            }
            
        }

        public interface IVisitor
        {
            void VisitPrivateHouse(PrivateHouse element);

            void VisitApartment(Apartment element);
        }

        class MilitaryOffice : IVisitor
        {
            public void VisitPrivateHouse(PrivateHouse element)
            {
                DateTime current_date = DateTime.Today;
                for (int i = 0; i < element.residents.Count; i++)
                {
                    if ((current_date - element.residents[i].date_of_birth).TotalDays / 365 <= 27
                        && (current_date - element.residents[i].date_of_birth).TotalDays / 365 >= 18
                        && element.residents[i].gender == "Ч")
                    {
                        Console.WriteLine($"[Приватний будинок]: Вiйськкомат завiтав у гостi до {element.residents[i]}");
                    }

                }
            }

            public void VisitApartment(Apartment element)
            {
                // Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
                //for (int i = 0; i < element.residents.Count; i++)
                //{
                //    for (int j = 0; j < i; j++)
                //    {
                //        Console.WriteLine($"Military Office at apartment {element.residents[i]}");
                //    }
                //}
                DateTime current_date = DateTime.Today;
                for (int i = 0; i < element.residents.Count; i++)
                {
                    if ((current_date - element.residents[i].date_of_birth).TotalDays / 365 <= 27
                        && (current_date - element.residents[i].date_of_birth).TotalDays / 365 >= 18
                        && element.residents[i].gender == "Ч")
                    {
                        Console.WriteLine($"[Багатоквартирний будинок]: Вiйськкомат завiтав у гостi до {element.residents[i]}");
                    }

                }
            }
        }

        class CensusService : IVisitor
        {
            public void VisitPrivateHouse(PrivateHouse element)
            {
                for (int i = 0; i < element.residents.Count; i++)
                {
                    
                    Console.WriteLine($"[Приватний будинок]: У будинку №{i+1} живе {element.residents.Count} осiб.");
                }
                
            }

            public void VisitApartment(Apartment element)
            {
                for (int i = 0; i < element.residents.Count; i++)
                {
                    Console.WriteLine($"[Багатоквартирний будинок]: У квартиpi №{i+1} живе {element.residents.Count} осiб.");
                }
            }
        }

        static void Main(string[] args)
        {
            List<Person> people1 = new List<Person> { 
                new Person("Hermione Granger", "Ж", new DateTime(2001, 10, 11)),
                new Person("Lord Voldemort", "Ч", new DateTime(1995, 10, 17)), 
                new Person("Severus Snape", "Ч", new DateTime(1980, 12, 11))};

            List<Person> people2 = new List<Person> { 
                new Person("The Hulk", "Ч", new DateTime(1996, 10, 17)), 
                new Person("Captain America", "Ч", new DateTime(2003, 12, 11)),
                new Person("Black Widow", "Ж", new DateTime(1998, 12, 19)), 
                new Person("Iron Man", "Ч", new DateTime(1980, 03, 04))};

            List<Person> people3 = new List<Person> {
                new Person("Sherlock Holmes", "Ч", new DateTime(2000, 04, 11)),
                new Person("Mary (Morstan) Watson", "Ж", new DateTime(2000, 12, 17)),
                new Person("Dr. John Watson", "Ч", new DateTime(2001, 02, 16))};

            List<Person> people4 = new List<Person> {
                new Person("Daenerys", "Ж", new DateTime(2002, 06, 19))};

            List<Person> people5 = new List<Person> {
                new Person("Luke Skywalker", "Ч", new DateTime(1975, 05, 11)),
                new Person("Darth Vader", "Ч", new DateTime(1996, 08, 18)),};


            PrivateHouse ph1 = new PrivateHouse(people1);
            PrivateHouse ph2 = new PrivateHouse(people2);
            List<PrivateHouse> privateHouses = new List<PrivateHouse> { ph1, ph2 };

            Apartment a1 = new Apartment(people3);
            Apartment a2 = new Apartment(people4);
            Apartment a3 = new Apartment(people5);
            List<Apartment> apartments = new List<Apartment> { a1, a2, a3 };

            MilitaryOffice m = new MilitaryOffice();
            CensusService c = new CensusService();

            for (int i = 0; i < privateHouses.Count; i++)
            {
                privateHouses[i].Accept(m);
                //privateHouses[i].Accept(c);
            }

            for (int i = 0; i < apartments.Count; i++)
            {
                apartments[i].Accept(m);
                //apartments[i].Accept(c);
            }

            Console.WriteLine("\n");

            for (int i = 0; i < privateHouses.Count; i++)
            {
                privateHouses[i].Accept(c);
            }
            for (int i = 0; i < apartments.Count; i++)
            {
                apartments[i].Accept(c);
            }


        }
    }
}
