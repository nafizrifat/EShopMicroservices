namespace Catalog.API.Products.GetProducts
{

    // No request DTO needed for GET all products.

    // Response DTO
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
        {
            // Send Query to MediatR
            var result = await sender.Send(
                new GetProductsQuery());

            // Map Result to Response using Mapster
            var response = result.Adapt<GetProductsResponse>();

            // Return HTTP 200 OK
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(
            StatusCodes.Status200OK)
        .ProducesProblem(
            StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get all products");
        }
    }
}
