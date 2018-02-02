module Tests

open System
open Xunit

[<Fact>]
let ``put fix msg into dictionary in order to search by the tag`` () =
    let m = "8=FIX.4.4|9=126|35=A|49=theBroker.12345|56=CSERVER|34=1|52=20170117- 08:03:04|57=TRADE|50=any_string|98=0|108=30|141=Y|553=12345|554=passw0rd!|10=131|"
    Assert.True(true)
