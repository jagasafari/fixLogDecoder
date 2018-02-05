module DataTypes

type DecodeError = 
    | NotFixTag of string [] 
    | NotFixTagKey of string

type Result<'T> = Success of 'T | Failure of DecodeError
