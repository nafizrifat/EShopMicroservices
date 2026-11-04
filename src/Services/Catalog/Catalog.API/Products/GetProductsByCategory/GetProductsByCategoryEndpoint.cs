
namespace Catalog.API.Products.GetProductsByCategory;

// No request DTO required.
// Category comes from the URL route parameter.

// Response DTO
public record GetProductsByCategoryResponse(
    IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                // Send query to MediatR
                var result = await sender.Send(
                    new GetProductsByCategoryQuery(category));

                // Map result to response DTO
                var response =
                    result.Adapt<GetProductsByCategoryResponse>();

                // Return HTTP 200 OK
                return Results.Ok(response);
            })
        .WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>(
            StatusCodes.Status200OK)
        .ProducesProblem(
            StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Category")
        .WithDescription("Get products by category");
    }
}
