using ShoppingCartApi.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseHttpsRedirection();
//product list
var products = new List<Product>
{
    new Product
    {
        Id = 1,
        Name = "Milk",
        Price = 16
    },
    new Product
    {
        Id = 2,
        Name = "Bread",
        Price = 25
    },
    new Product
    {
        Id = 3,
        Name = "Pasta",
        Price = 35
    },
    new Product
    {
        Id = 4,
        Name = "Cheese",
        Price = 45
    },
    new Product
    {
        Id = 5,
        Name = "Tomato",
        Price = 55
    },
    new Product
    {
        Id = 6,
        Name = " one tray Eggs",
        Price = 35
    },
    new Product
    {
        Id = 7,
        Name = "Salt",
        Price = 10.5
    },
    new Product
    {
        Id = 8,
        Name = "Sugar",
        Price = 25
    },
    new Product
    {
        Id = 9,
        Name = "Potatoes",
        Price = 12.9
    },
    new Product
    {
        Id = 10,
        Name = "Chocolate",
        Price = 15 
    }
    };
    //shopping cart
    var Cart = new ShoppingCart();
    //Get all products
    app.MapGet("/products", () =>
    {
        return products;
    });
    app.MapPost("/cart/add", (int productId, int quantity) =>
    {
        var product = products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
            return Results.NotFound("product  not found");
        var existingItem = Cart.Items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            Cart.Items.Add(new CartItem
            {
                Product = product,
                Quantity = quantity
            });
        }

        return Results.Ok(Cart.Items);
    });
// GET Cart
app.MapGet("/cart" , () =>
{
    return Cart;
});
//REMOVE item from cart
app.MapDelete("/cart/remove/{productId}", (int productId) =>
{
    var item = Cart.Items.FirstOrDefault(i => i.Product.Id == productId);
    if (item == null)
        return Results.NotFound("item not found");
    Cart.Items.Remove(item);
    return Results.Ok(Cart);
});
//clear cart
app.MapDelete("/cart/clear", () =>
        {
            Cart.Items.Clear();
            return Results.Ok("Cart cleared");
        });
app.UseSwagger();
app.UseSwaggerUI();
    app.Run();

    



    
