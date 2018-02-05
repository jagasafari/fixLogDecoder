module Tests

open System
open System.Collections.Generic
open Xunit
open Swensen.Unquote
open SignatureUtil
open FixMessage
open DataTypes

let fixTagTestData =
    seq { 
        yield ("8=abc", Success (8, "abc")) 
        yield ("=abc", NotFixTag [|"abc"|] |> Failure)
        yield ("nhf=abc", NotFixTagKey "nhf" |> Failure)
        yield (null, NotFixTag [||] |> Failure)
        yield ("", NotFixTag [||] |> Failure)
        yield ("9=78", (9, "78") |> Success)
    }
    |> Seq.map (fun (x, y) -> [| x :> obj; y :> obj |])
 
[<Theory; MemberData("fixTagTestData")>]
let ``fixTag: all cases`` case expected =
    getTag case =! expected
    
[<Fact>]
let ``get signature`` () =
    writeTypeMembers (typeof<Int32>)

[<Fact>]
let ``put fix msg into dictionary in order to search by the tag`` () =
    let m = "8=FIX.4.4|9=126|35=A|49=theBroker.12345|56=CSERVER|34=1|52=20170117- 08:03:04|57=TRADE|50=any_string|98=0|108=30|141=Y|553=12345|554=passw0rd!|10=131|"
    let dict = extractFixMsg m
    dict.[8] =! "FIX.4.4"
    dict.[9] =! "126"
    dict.[10] =! "131"
    dict.[57] =! "TRADE"

[<Fact>]
let ``duplicate keys`` () =
    let m = [ 3, "a"; 3, "b" ] |> Map.ofList 
    m.[3] =! "b"
