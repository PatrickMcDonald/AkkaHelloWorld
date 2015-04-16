open Akka
open Akka.Actor
open Akka.FSharp

// Create an (immutable) message type that your actor will respond to
type Greet = Greet of string

[<EntryPoint>]
let main argv =

    let system = System.create "MySystem" <| Configuration.parse "
        akka {
            log-config-on-start = on
            stdout-loglevel = DEBUG
            loglevel = DEBUG
        }"

    // Use F# computation expression with tail-recursive loop
    // to create an actor message handler and return a reference
    let greeter = spawn system "greeter" <| fun mailbox ->
        let rec loop _ = actor {
            let! msg = mailbox.Receive()
            match msg with
            | Greet who -> printfn "Hello %s" who
            return! loop() }
        loop()

    greeter <! Greet "World"

    System.Console.ReadKey true |> ignore

    0 // return an integer exit code
