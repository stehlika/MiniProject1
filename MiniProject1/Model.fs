module MiniProject1.Model

type food = | Cake | Sandwich | Salad

type size = | Small | Medium | Large

type order =
    {
        Food : food
        Size : size
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

let orderPrice order =
    basePrice order.Food * sizeMyltiplier order.Size

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
    
let printPrice order =
    sprintf "Price of %s %s: %A" (sizeName order.Size)(foodName order.Food)(orderPrice order)

let run =
    let order1 = {
        Food = getFood "salad"
        Size = getSize "l"
    }
     let order2 = {
        Food = getFood "cake"
        Size = getSize "s"
    }
      let order3 = {
        Food = getFood "sandwich"
        Size = getSize "m"
    }
       let order4 = {
        Food = getFood "cake"
        Size = getSize "l"
    }
    printfn "%A" (printPrice order1)
    printfn "%A" (printPrice order2)
    printfn "%A" (printPrice order3)
    printfn "%A" (printPrice order4)
    
    0