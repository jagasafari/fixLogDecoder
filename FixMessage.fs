module FixMessage

open System
open DataTypes

let split (s: char) (x: string) = 
    if isNull x then [||]
    else x.Split(s, StringSplitOptions.RemoveEmptyEntries)

let getTag str = 
    split '=' str
    |> function
    | [||] -> NotFixTag [||] |> Failure 
    | [|x; y|] -> 
        match Int32.TryParse x with
        | (true, x') -> (x', y) |> Success
        | (false, _) -> NotFixTagKey x |> Failure
    | x -> NotFixTag x |> Failure 

let extractFixMsg msg =
    let tag = function
        | Success (x, y) -> Some (x, y) | _ -> None
    split '|' msg
    |> Seq.map (getTag >> tag)
    |> Seq.choose id
    |> Map.ofSeq
