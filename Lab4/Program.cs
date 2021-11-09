using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program
    {
        public interface IObserver
        { 
            // get updates from publisher
            void Update(ISubject subject);
        }
        public interface ISubject
        {
            // attaches users to news
            void Attach(IObserver observer, string type);

            // detaches users to news
            void Detach(IObserver observer, string type);

            // notifies user when news is added
            void Notify(string type);
        }

        public class TextNews
        {
            public string title;
            public List<string> topic;
            public string text;
            public TextNews(string title, List<string> topic, string text)
            {
                this.title = title;
                this.topic = topic;
                this.text = text;
            }
            public override string ToString()
            {
                return $"{this.title}";
            }
        }

        public class VideoNews
        {
            public string title;
            public List<string> topic;
            public string url;
            public VideoNews(string title, List<string> topic, string url)
            {
                this.title = title;
                this.topic = topic;
                this.url = url;
            }
            public override string ToString()
            {
                return $"{this.title}";
            }
        }

        public class NewsFeed : ISubject
        {
            public List<TextNews> text_news = new List<TextNews> { };
            public List<VideoNews> video_news = new List<VideoNews> { };

            public List<IObserver> text_observers = new List<IObserver>(); // users subscribed to text news
            public List<IObserver> video_observers = new List<IObserver>(); // users subscribed to video news
            
            public void Attach(IObserver observer, string type)
            {
                if (type == "t")
                {
                    Console.WriteLine($"NewsFeed: New user subscribed to text news.");
                    this.text_observers.Add(observer);
                }
                if (type == "v")
                {
                    Console.WriteLine("NewsFeed: New user subscribed to video news.");
                    this.video_observers.Add(observer);
                }
                if (type == "tv")
                {
                    Console.WriteLine("NewsFeed: New user subscribed to text & video news.");
                    this.text_observers.Add(observer);
                    this.video_observers.Add(observer);
                }
            }

            public void Detach(IObserver observer, string type)
            {
                if (type == "t")
                {
                    Console.WriteLine("NewsFeed: User unsubscribed from text news.");
                    this.text_observers.Remove(observer);
                }
                if (type == "v")
                {
                    Console.WriteLine("NewsFeed: User unsubscribed from video news.");
                    this.video_observers.Remove(observer);
                }
                if (type == "tv")
                {
                    Console.WriteLine("NewsFeed: User unsubscribed from text & video news.");
                    this.text_observers.Remove(observer);
                    this.video_observers.Remove(observer);
                }
            }

            public void Notify(string type)
            {
                if (type == "t")
                {
                    Console.WriteLine("NewsFeed: Notifying text subscribers...");

                    foreach (var observer in text_observers)
                    {
                        observer.Update(this);
                    }
                }
                if (type == "v")
                {
                    Console.WriteLine("NewsFeed: Notifying video subscribers...");

                    foreach (var observer in video_observers)
                    {
                        observer.Update(this);
                    }
                }
                if (type == "tv")
                {
                    Console.WriteLine("NewsFeed: Notifying all subscribers...");

                    foreach (var observer in text_observers.Concat(video_observers))
                    {
                        observer.Update(this);
                    }
                }
            }

            public void AddedTextNews(TextNews news)
            {
                this.text_news.Add(news);
                Console.WriteLine(text_news.Last());
                this.Notify("t");

            }
            public void AddedVideoNews(VideoNews news)
            {
                this.video_news.Add(news);
                Console.WriteLine(video_news.Last());
                this.Notify("v");
            }
        }


        class Subscriber : IObserver
        {
            public string name;
            public Subscriber(string name)
            {
                this.name = name;
            }
            public void Update(ISubject subject)
            {
                if ((subject as NewsFeed).text_news.Count != 0 || (subject as NewsFeed).text_news.Count != 0)
                {
                    Console.WriteLine($"{name}: Reacted to the news.");
                }
            }
        }

        static void Main(string[] args)
        {
            var news = new NewsFeed();
            var s1 = new Subscriber("Kate");
            var s2 = new Subscriber("Alex");
            var s3 = new Subscriber("Mary"); 


            var t1 = new TextNews("Twitter poll calls on Elon Musk to sell 10% stake in Tesla", 
                new List<string> { "Tesla", "Elon Musk", "tax" }, 
                "More than 3.5 million Twitter users voted in the poll, launched by Mr Musk on Saturday, " +
                "with nearly 58% voting in favour of the share sale...");


            var t2 = new TextNews("Sir Paul McCartney reveals parents inspired Beatles and solo songs", 
                new List<string> { "music", "Thr Beatles" },
                "Sir Paul McCartney has said his parents were the original inspiration for many of his songs " +
                "and a huge influence on the way he approached his music...");

            var v1 = new VideoNews("Climate change: What do scientists want from COP26 this week?", 
                new List<string> { "climate", "science" },
                "https://www.youtube.com/watch?v=WyYwiF1F5xY");

            var v2 = new VideoNews("iPhone 13 Pro and 13 Pro Max - Worth the upgrade?",
                new List<string> { "iphone", "technologies" },
                "https://www.youtube.com/watch?v=ckiQhacr9Uc");

            news.Attach(s1, "t"); // Kate subscribed to text news
            news.Attach(s2, "t"); // Alex subscribed to text news
            news.AddedTextNews(t1);  // added 1st text news

            Console.WriteLine("\n * * * * *\n ");

            news.Detach(s2, "t"); // Alex unsubscirber from text news
            news.AddedTextNews(t2);

            Console.WriteLine("\n * * * * *\n ");

            news.Attach(s1, "v"); // Kate subscribed to video news
            news.Attach(s3, "v"); // Mary subscribed to video news
            news.Attach(s2, "tv"); // Alex subscribed to text & video news
            news.AddedVideoNews(v1);

            Console.WriteLine("\n * * * * *\n ");

            news.Detach(s1, "v"); // Mary unsubscirber from video news
            news.AddedVideoNews(v2);
        }
    }
}
