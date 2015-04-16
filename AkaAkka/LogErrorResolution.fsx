#r "../packages/FsPickler.0.9.11/lib/net45/FsPickler.dll"
#r "../packages/Akka.1.0.0/lib/net45/Akka.dll"
#r "../packages/Akka.FSharp.1.0.0/lib/net45/Akka.FSharp.dll"

open Akka
open Akka.Actor
open Akka.FSharp

let system = System.create "MySystem" <| Configuration.parse "
    akka {
        log-config-on-start = on
        stdout-loglevel = DEBUG
        loglevel = ERROR
    }"

