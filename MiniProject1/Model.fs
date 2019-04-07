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

type CanteenMessage =
    | Order of string * string * string
    | Comment of string
    | End

let orderLoop =
    printfn "Service start..."
    MailboxProcessor.Start(fun inbox ->
       let rec innerLoop func =
            func
            async {
                let! eventMsg = inbox.Receive()
                match eventMsg with
                | Order(t, s, e) -> return! innerLoop (printfn "%A" (buy t s e))
                | Comment s -> return! innerLoop (printfn "%A" s)
                | End -> printfn "Service end..."; return ()
            }
       innerLoop()
    )

module internal String =
    let split separator (s : string) =
        let values = ResizeArray<_>()
        let rec gather start i =
            let add() = s.Substring(start, i - start) |> values.Add
            if i = s.Length then add()
            elif s.[i] = '"' then inQuotes start (i + 1)
            elif s.[i] = separator then add(); gather (i + 1) (i + 1)
            else gather start (i + 1)
        and inQuotes start i =
            if s.[i] = '"' then gather start (i + 1)
            else inQuotes start (i + 1)
        gather 0 0
        values.ToArray()

let examples =
    let order1 = {
        Food = getFood "salad"
        Size = getSize "l"
        Extra = getExtra "bag"
    }

    printfn "%A" (printPrice order1)

    printfn "%A" (buy "salad" "m" "")

    orderLoop.Post(Order("cake", "l", "wrap"))
    orderLoop.Post(Order("sandwich", "m", "wrhtr"))
    orderLoop.Post(Order("salad", "l", "bag"))
    orderLoop.Post(Comment("Delicious Salad :3"))
    orderLoop.Post(Order("salad", "s", ""))
    orderLoop.Post(Order("sandwich", "m", "cutlery"))
    orderLoop.Post(End)

let canteenSystem =
    printfn "Accepted commands:"
    printfn "buy <type> <size> [<extra>]"
    printfn "end"
    printfn "Allowed entries: "
    printfn "<type>: cake, salad, sandwich"
    printfn "<size>: s, m, l"
    printfn "<extra>: wrap, bag, cutlery"
    printfn ""
    let evalBuy s =
        let split = String.split ' ' s
        let Type = split.[1]
        let Size = split.[2]
        match split.Length with
        | 4 -> orderLoop.Post(Order (Type, Size, split.[3]))
        | 3 -> orderLoop.Post(Order (Type, Size, ""))
        
    let rec innerLoop func =
        func
        let c = Console.ReadLine()
        let split = String.split ' ' c
        match split.[0] with
        | "buy" -> innerLoop(evalBuy c |> ignore)
        | "end" -> ()
        | _ -> innerLoop(printfn "Wrong command, try again...")
    innerLoop()

let run =
    examples

    canteenSystem
    0