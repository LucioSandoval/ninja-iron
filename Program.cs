using System;
using System.Collections.Generic;
public class Program
{
interface IConsumable
{
    string Name { get; set; }
    int Calories { get; set; }
    bool IsSpicy { get; set; }
    bool IsSweet { get; set; }
    string GetInfo();
}

class Food : IConsumable
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public bool IsSpicy { get; set; }
    public bool IsSweet { get; set; }

    public string GetInfo()
    {
        return $"{Name} (Food). Calories: {Calories}. Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }

    public Food(string name, int calories, bool spicy, bool sweet)
    {
        Name = name;
        Calories = calories;
        IsSpicy = spicy;
        IsSweet = sweet;
    }
}

class Drink : IConsumable
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public bool IsSpicy { get; set; }
    public bool IsSweet { get; set; }

    public string GetInfo()
    {
        return $"{Name} (Drink). Calories: {Calories}. Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }

    public Drink(string name, int calories)
    {
        Name = name;
        Calories = calories;
        IsSpicy = false;
        IsSweet = true;
    }
}

abstract class Ninja
{
    protected int calorieIntake;
    public List<IConsumable> ConsumptionHistory;

    public Ninja()
    {
        calorieIntake = 0;
        ConsumptionHistory = new List<IConsumable>();
    }

    public abstract bool IsFull { get; }
    public abstract void Consume(IConsumable item);
}

class SweetTooth : Ninja
{
    public override bool IsFull => calorieIntake >= 1500;

    public override void Consume(IConsumable item)
    {
        if (!IsFull)
        {
            calorieIntake += item.Calories;
            if (item.IsSweet)
            {
                calorieIntake += 10;
            }
            ConsumptionHistory.Add(item);
            Console.WriteLine($"SweetTooth consumed: {item.GetInfo()}");
        }
        else
        {
            Console.WriteLine("SweetTooth is full and cannot eat more.");
        }
    }
}

class SpiceHound : Ninja
{
    public override bool IsFull => calorieIntake >= 1200;

    public override void Consume(IConsumable item)
    {
        if (!IsFull)
        {
            calorieIntake += item.Calories;
            if (item.IsSpicy)
            {
                calorieIntake -= 5;
            }
            ConsumptionHistory.Add(item);
            Console.WriteLine($"SpiceHound consumed: {item.GetInfo()}");
        }
        else
        {
            Console.WriteLine("SpiceHound is full and cannot eat more.");
        }
    }
}

class Buffet
{
    public List<IConsumable> Menu;

    public Buffet()
    {
        Menu = new List<IConsumable>
        {
            new Food("Pizza", 300, false, false),
            new Food("Sushi", 250, false, false),
            new Food("Burger", 400, false, false),
            new Food("Fries", 200, false, false),
            new Food("Ice Cream", 150, false, true),
            new Food("Hot Wings", 200, true, false),
            new Food("Donut", 100, false, true),
            new Food("Salad", 100, false, false),
            new Drink("Soda", 150),
            new Drink("Water", 0)
        };
    }

    public IConsumable Serve()
    {
        Random rand = new Random();
        int index = rand.Next(Menu.Count);
        return Menu[index];
    }
}


    static void Main(string[] args)
    {
        Buffet buffet = new Buffet();
        SweetTooth sweetTooth = new SweetTooth();
        SpiceHound spiceHound = new SpiceHound();

        while (!sweetTooth.IsFull)
        {
            IConsumable item = buffet.Serve();
            sweetTooth.Consume(item);
        }

        while (!spiceHound.IsFull)
        {
            IConsumable item = buffet.Serve();
            spiceHound.Consume(item);
        }

        Console.WriteLine($"SweetTooth consumed {sweetTooth.ConsumptionHistory.Count} items.");
        Console.WriteLine($"SpiceHound consumed {spiceHound.ConsumptionHistory.Count} items.");

        if (sweetTooth.ConsumptionHistory.Count > spiceHound.ConsumptionHistory.Count)
        {
            Console.WriteLine("SweetTooth consumed the most items.");
        }
        else if (spiceHound.ConsumptionHistory.Count > sweetTooth.ConsumptionHistory.Count)
        {
            Console.WriteLine("SpiceHound consumed the most items.");
        }
        else
        {
            Console.WriteLine("SweetTooth and SpiceHound consumed the same number of items.");
        }
    }
}