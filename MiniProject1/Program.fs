module MiniProject1.Program


type FoodType = Salad | Sandwich | Bagel | Cake

type Size = Small | Medium | Large

type Food = {foodType: FoodType; size: Size; desc: string}

let caesarSalad = {foodType = FoodType.Salad; size = Size.Large; desc = "Caesar"};
let veganSalad = {foodType = FoodType.Salad; size = Size.Medium; desc = "Vegan"};
let tunaSalad = {foodType = FoodType.Salad; size = Size.Large; desc = "Tuna"};

let porkSandwich = {foodType = FoodType.Sandwich; size = Size.Medium; desc = "Pork"};
let cheeseSteakSandwich = {foodType = FoodType.Sandwich; size = Size.Large; desc = "CheeseSteak"};
let garlicButterSandwich = {foodType = FoodType.Sandwich; size = Size.Small; desc = "GarlicButter"};

let beefBagel = {foodType = FoodType.Bagel; size = Size.Medium; desc = "Beef"};
let eggBagel = {foodType = FoodType.Bagel; size = Size.Medium; desc = "Egg"};
let basicBagel = {foodType = FoodType.Bagel; size = Size.Small; desc = "Basic"};

let chocolateFudgeBrownieCake = {foodType = FoodType.Cake; size = Size.Medium; desc = "ChocolateFudgeBrownie"};
let strawberyChesecake = {foodType = FoodType.Cake; size = Size.Medium; desc = "StrawberyChesecake"};
let pumpkinPie = {foodType = FoodType.Cake; size = Size.Small; desc = "Punpkin"};



let considerType (food: Food) =
    match food.foodType with
        | Salad -> 20.0
        | Sandwich -> 25.0
        | Bagel -> 30.0
        | Cake -> 15.0
      //  |_ -> 0
        
        
let considerSize (food: Food) =
    match food.size with
        | Small -> 1.0
        | Medium -> 1.25
        | Large -> 1.5
      //  |_ -> 0.0

let calculatePrice (food: Food) =
    considerType(food) * considerSize(food)

calculatePrice(caesarSalad)
calculatePrice(veganSalad)
calculatePrice(tunaSalad)
calculatePrice(porkSandwich)
calculatePrice(cheeseSteakSandwich)
calculatePrice(garlicButterSandwich)
calculatePrice(beefBagel)
calculatePrice(eggBagel)
calculatePrice(chocolateFudgeBrownieCake)
calculatePrice(strawberyChesecake)
calculatePrice(pumpkinPie)

let kvittering = calculatePrice(veganSalad) + calculatePrice(garlicButterSandwich) + calculatePrice(chocolateFudgeBrownieCake)

printf "You have to pay: {0}", kvittering



[<EntryPoint>]
let main argv = 
    Model.run
    |> ignore
    0 // return an integer exit code



/// In this mini-project, you will design a type to represent the
/// different types of foodin the canteen and implement the price calculation.
/// You need to go to the canteen to get the details.
/// For instance, salad,sandwich,andcaketogether with their corresponding
/// sizes (maybe Small, Medium and Large) and prices.


/// You will be calculating the different prices for theabove food
/// itemsdepending on thetypeand size.
/// Phase 1:
/// You are allowed to develop your own strategy for describing the
/// different foodand their sizes as  well  as  well  as  functions
/// that  calculatethe  price(as  obtained  from  the  canteen)
/// for  a given food item.As a minimum, you should define a
/// simple data type for describing the different food itemsoffered
/// in the VIA canteen and implement the calculation of the prices.
/// Hints: You will need define the data typefor thefood and size (union)
/// and the content (for instance abag/wrap)
/// with the size(record).
/// You will also need to define the price calculation
/// function that uses pattern matching.