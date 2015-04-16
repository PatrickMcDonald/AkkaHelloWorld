
//#load "Library1.fs"
//open AkaAkka

// Define your library scripting code here

#r "../packages/Akka.1.0.0/lib/net45/Akka.dll"
open Akka
open Akka.Actor
open Akka.Configuration

type Greet(who: string) =
    member this.Who = who

type GreetingActor() =
    inherit TypedActor()
    interface IHandle<Greet> with
        member this.Handle(greet: Greet) =
            printfn "Hello %s" greet.Who


//create a new actor system (a container for your actors)
//let system = ActorSystem.Create "MySystem"
let system = ActorSystem.Create ("MySystem", ConfigurationFactory.ParseString "
    akka {
        log-config-on-start = on
        stdout-loglevel = DEBUG
        loglevel = ERROR
    }")

//create your actor and get a reference to it.
//this will be an "ActorRef", which is not a
//reference to the actual actor instance
//but rather a client or proxy to it
let greeter = system.ActorOf<GreetingActor> "Greeter"

//send a message to the actor
Greet "World" |> greeter.Tell
