module MiniProject1.Program

[<EntryPoint>]
let main argv = 
    Model.run
    |> ignore
    0 // return an integer exit code
