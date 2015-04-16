using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Add these two lines
using Akka;
using Akka.Actor;

namespace CsAkkaConsole
{
    //Create an (immutable) message type that your actor will respond to
    public class Greet
    {
        public Greet(string who)
        {
            Who = who;
        }
        public string Who { get; private set; }
    }

    // Create the actor class
    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            // Tell the actor to respond
            // to the Greet message
            Receive<Greet>(greet =>
               Console.WriteLine("Hello {0}", greet.Who));
        }
    }

    // or using the TypedActor API
    public class TypedGreetingActor : TypedActor, IHandle<Greet>
    {
        public void Handle(Greet greet)
        {
            Console.WriteLine("Hello {0}!", greet.Who);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //create a new actor system (a container for your actors)
            var system = ActorSystem.Create("MySystem");
            //create your actor and get a reference to it.
            //this will be an "ActorRef", which is not a
            //reference to the actual actor instance
            //but rather a client or proxy to it
            var greeter = system.ActorOf<GreetingActor>("greeter");
            //send a message to the actor
            greeter.Tell(new Greet("World"));

            //this prevents the app from exiting
            //Before the async work is done
            Console.ReadKey(true);
        }
    }
}
