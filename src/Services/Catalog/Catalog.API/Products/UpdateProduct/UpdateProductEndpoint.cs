
namespace Catalog.API.Products.UpdateProduct;

// Request DTO - Received from HTTP PUT body
public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

// Response DTO - Sent back to client
public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (
            UpdateProductRequest request,
            ISender sender) =>
        {
            // 1. Map Request → Command
            var command =
                request.Adapt<UpdateProductCommand>();

            // 2. Send Command through MediatR
            var result = await sender.Send(command);

            // 3. Map Result → Response
            var response =
                result.Adapt<UpdateProductResponse>();

            // 4. Return HTTP 200 OK
            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(
            StatusCodes.Status200OK)
        .ProducesProblem(
            StatusCodes.Status400BadRequest)
        .ProducesProblem(
            StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update an existing product");
    }
}
