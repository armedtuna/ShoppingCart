using ShoppingCart.Entities;
using ShoppingCart.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyHeader()
        // todo-at: move to appsettings? hard-coded, say only GET and POST?
        .AllowAnyMethod()
        // todo-at: move to appsettings
        .WithOrigins("http://localhost:3001");
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

    // todo-at: complete to support adding new product
    // webApplication.MapPost($"{webRoot}/products",
    //         bool () =>
    //             ProductModel.Instance.RetrieveProducts())
    //     .WithName("GetProducts")
    //     .WithOpenApi();

    // todo-at: no quantity, so should the caller just scan many times?
    webApplication.MapPost($"{webRoot}/scan",
            IResult (ScanProduct scanProduct) =>
                CheckoutModel.Instance.Scan(scanProduct))
        .WithName("PostAddProductToCart")
        .WithOpenApi();

    webApplication.MapDelete($"{webRoot}/remove/{{sku}}",
            IResult (string sku) =>
                CheckoutModel.Instance.Delete(sku))
        .WithName("DeleteRemoveProductFromCart")
        .WithOpenApi();
}
