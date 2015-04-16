#r "../packages/Akka.1.0.0/lib/net45/Akka.dll"
#r "../packages/Akka.FSharp.1.0.0/lib/net45/Akka.FSharp.dll"
#r "../packages/FsPickler.0.9.11/lib/net45/FsPickler.dll"

open Akka
open Akka.Actor
open Akka.FSharp

// Create an (immutable) message type that your actor will respond to
type Greet =
    | Greet of string
    | Anon

[<System.STAThread>]
do
    let system = System.create "MySystem" <| Configuration.parse "
        akka {
            log-config-on-start = on
            stdout-loglevel = DEBUG
            loglevel = ERROR
        }"

    // Use F# computation expression with tail-recursive loop
    // to create an actor message handler and return a reference
    let greeter = spawn system "greeter" <| fun mailbox ->
        let rec loop _ = actor {
            let! msg = mailbox.Receive()
            match msg with
            | Greet who -> printfn "Hello, %s!" who
            | Anon ->      printfn "Hello, Who's that?"
            return! loop() }
        loop()

    greeter <! Greet "World"
    greeter <! Anon

