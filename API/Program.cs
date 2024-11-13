using ShoppingCart.Entities;
using ShoppingCart.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyHeader()
        // todo-at: move to appsettings? hard-coded, say only GET and POST?
        .AllowAnyMethod()
        // todo-at: move to appsettings
        .WithOrigins("http://localhost:5044");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SetUpRoutes(app);

app.Run();
return;

// todo-at: rip out the loyalty rule bit and leave that on another branch
void SetUpRoutes(WebApplication webApplication)
{
    const string webRoot = "/shoppingcart";

    webApplication.MapGet($"{webRoot}/cart",
            Checkout () =>
                CheckoutModel.Instance.GetCart())
        .WithName("GetCart")
        .WithOpenApi();

    webApplication.MapGet($"{webRoot}/products",
            Product[] () =>
                ProductModel.Instance.RetrieveProducts())
        .WithName("GetProducts")
        .WithOpenApi();

    webApplication.MapPost($"{webRoot}/scan",
        ScanResult (Product product) =>
            CheckoutModel.Instance.Scan(product))
        .WithName("PostAddProductToCart")
        .WithOpenApi();
}
