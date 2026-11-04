namespace Catalog.API.Products.DeleteProduct;

// No request DTO required.
// Product ID comes from the route parameter.

// Response DTO
public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (
            Guid id,
            ISender sender) =>
        {
            // Send command to MediatR
            var result = await sender.Send(
                new DeleteProductCommand(id));

            // Map Result → Response
            var response =
                result.Adapt<DeleteProductResponse>();

            // Return HTTP 200 OK
            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(
            StatusCodes.Status200OK)
        .ProducesProblem(
            StatusCodes.Status400BadRequest)
        .ProducesProblem(
            StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete a product by ID");
    }
}
