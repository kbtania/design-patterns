using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Program
    {
        interface IPrototype
        {
            IPrototype Clone();
        }

        class Event
        {
            public DateTime date;
            public string description;
            private string secretKey;
            public User[] users;

            public Event(DateTime date, string description, string secretKey, User[] users)
            {
                this.date = date;
                this.description = description;
                this.secretKey = secretKey;
                this.users = users;
            }

            public Event Clone()
            {
                return this.MemberwiseClone() as Event;
            }
        }

        class Meeting: Event
        {
            public DateTime startTime;
            public DateTime endTime;
            public string location;

            public Meeting(DateTime date, string description, string secretKey, User[] users, DateTime startTime, DateTime endTime, string location) : base(date, description, secretKey, users)
            {
                this.startTime = startTime;
                this.endTime = endTime;
                this.location = location;
            }

            public Meeting Clone()
            {
                Meeting clone = this.MemberwiseClone() as Meeting;
                clone.users = this.users.Clone() as User[];
                
                return clone;
            }

            public override string ToString()
            {
                return $"Date: {this.date}\n Description: {this.description}\n " +
                    $"People: {this.users}\n Start: {this.startTime} \n " +
                    $"End: {this.endTime}\n Location: {this.location}";
            }
        }

        class Birtday: Event
        {
            public User birthdayPerson;
            public Birtday(DateTime date, string description, string secretKey, User[] users, User birthdayPerson) : base( date, description, secretKey, users)
            {
                this.birthdayPerson = birthdayPerson;
            }

            public Birtday Clone()
            {
                Birtday clone = this.MemberwiseClone() as Birtday;
                clone.birthdayPerson = this.birthdayPerson.Clone() as User;

                return clone;
            }

            public override string ToString()
            {
                return $"Date: {this.date}\n Description: {this.description}\n " +
                    $"People: {this.users}\n Birthday of: {this.birthdayPerson}";
            }
        }

        class User
        {
            public string login;
            public string password;
            public string email;
            
            public User(string login, string password, string email)
            {
                this.login = login;
                this.password = password;
                this.email = email;
            }

            public User Clone()
            {
                return this.MemberwiseClone() as User;
            }
            
            public override string ToString()
            {
                return $"{this.email}";
            }
        }

        class Calendar
        {
            public List<Event> events;
            public Calendar()
            {
                this.events = new List<Event>();
            }
            public void addEvent(Event ev)
            {
                this.events.Add(ev);
            }

            public void displayEvents()
            {
                for (int i = 0; i < this.events.Count; i++)
                {
                    Console.WriteLine($"{this.events[i]}");
                }
                
            }

            
        }

        static void Main(string[] args)
        {
            User u1 = new User("log1", "pass1", "email1");
            User u2 = new User("log2", "pass2", "email2");
            User[] users = { u1, u2 };

            Meeting m1 = new Meeting(new DateTime(2021, 10, 20), "Meeting1", "key1", users, new DateTime(2021, 10, 20), new DateTime(2021, 10, 21), "location1");
            Meeting m2 = m1.Clone();
            m2.description = "Meeting2";
            User u3 = new User("log3", "pass3", "email3");
            var b1 = new Birtday(new DateTime(2021, 12, 12), "Birtday Event", "secretkey", users, u3);
            var b2 = b1.Clone();
            b2.birthdayPerson = new User("log4", "pass4", "email4");

            var cal = new Calendar();
            cal.addEvent(m1);
            cal.addEvent(m2);
            cal.addEvent(b1);
            cal.addEvent(b2);

            for (int i = 0; i < users.Length; i++)
            {
                Console.WriteLine(users[i]);
                cal.displayEvents();
            }  
        }
    }
}
