module MiniProject1.Model
open System

type food = | Cake | Sandwich | Salad

type size = | Small | Medium | Large

type extra = | Wrap | Bag | Cutlery

type order =
    {
        Food : food
        Size : size
        Extra : extra option
    }

let basePrice food =
    match food with
    | Cake -> 15.5
    | Salad -> 20.0
    | Sandwich -> 25.0

let sizeMyltiplier size =
    match size with
    | Small -> 0.75
    | Medium -> 1.0
    | Large -> 1.25

let extraPrice addon =
    match addon with
    | Some a -> (match a with | Wrap -> 2.5 | Bag -> 5.0 | Cutlery -> 1.0)
    | None -> 0.0

let orderPrice order =
    basePrice order.Food * sizeMyltiplier order.Size + extraPrice order.Extra

let foodName food =
    match food with
    | Cake -> "Cake"
    | Salad -> "Salad"
    | Sandwich -> "Sandwich"

let getFood id =
    match id with
    | "cake" -> food.Cake
    | "salad" -> food.Salad
    | "sandwich" -> food.Sandwich

let sizeName size =
    match size with
    | Small -> "Small"
    | Medium -> "Medium"
    | Large -> "Large"

let getSize id =
    match id with
    | "s" -> size.Small
    | "m" -> size.Medium
    | "l" -> size.Large

let extraName extra =
     match extra with
     | Some a -> (match a with | Wrap -> "Wrap" | Bag -> "Bag" | Cutlery -> "Cutlery")
     | None -> ""

let getExtra id =
    match id with
    | "wrap" -> Some(extra.Wrap)
    | "bag" -> Some(extra.Bag)
    | "cutlery" -> Some(extra.Cutlery)
    | _ -> None

let printPrice order =
    match order.Extra with
    | Some s -> sprintf "Price of %s %s with extra %s: %A" (sizeName order.Size) (foodName order.Food) (extraName order.Extra) (orderPrice order)
    | None -> sprintf "Price of %s %s: %A" (sizeName order.Size) (foodName order.Food) (orderPrice order)

let buy t s e =
    let o = {
        Food = getFood t
        Size = getSize s
        Extra = getExtra e
    }
    printPrice o

type eventMsg =
    | Buy of string * string * string
    | End

let orderLoop =
    printfn "Service start..."
    MailboxProcessor.Start(fun inbox ->
       let rec innerLoop func =
            func
            async {
                let! eventMsg = inbox.Receive()
                match eventMsg with
                | Buy(t, s, e) ->return! innerLoop (printfn "%A" (buy t s e)) 
                | End -> printfn "Service end..."; return ()
            }
       innerLoop()
    )

let run =
    let order1 = {
        Food = getFood "salad"
        Size = getSize "l"
        Extra = getExtra "bag"
    }

    printfn "%A" (printPrice order1)

    printfn "%A" (buy "salad" "m" "")

    orderLoop.Post(Buy("cake", "l", "wrap"))
    orderLoop.Post(Buy("sandwich", "m", "wrhtr"))
    orderLoop.Post(Buy("salad", "l", "bag"))
    orderLoop.Post(Buy("salad", "s", ""))
    orderLoop.Post(Buy("sandwich", "m", "cutlery"))
    orderLoop.Post(End)
    
    Console.ReadKey()
    |> ignore
    0