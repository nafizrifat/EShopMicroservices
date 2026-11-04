
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                // 1. Convert Request to Command
                var command =
                    request.Adapt<CreateProductCommand>();

                // 2. Send Command through Mediator
                var result = await sender.Send(command);

                // 3. Convert Result to Response
                var response =
                    result.Adapt<CreateProductResponse>();

                // 4. Return HTTP 201 Created
                return Results.Created(
                    $"/products/{response.Id}",
                    response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(
                StatusCodes.Status201Created)
            .ProducesProblem(
                StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create a new product");
        }
    }
}
