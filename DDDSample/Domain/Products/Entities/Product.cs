using System;

namespace DDDSample.Domain.Products.Entities;

public class Product
{
    public int Id { get; init; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public int Stock { get; private set; }

    public Product(string name, decimal price, string description, int stock)
    {
        Name = name;
        Price = price;
        Description = description;
        Stock = stock;
    }

    public void Update(string name, decimal price, string description, int stock)
    {
        Name = name;
        Price = price;
        Description = description;
        Stock = stock;
    }
}