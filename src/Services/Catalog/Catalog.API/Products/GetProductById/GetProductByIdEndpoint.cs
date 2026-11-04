namespace Catalog.API.Products.GetProductById
{
    // No request DTO required.
    // Product ID comes from the URL route parameter.

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (
                Guid id,
                ISender sender) =>
            {
                // Send query to MediatR
                var result = await sender.Send(
                    new GetProductByIdQuery(id));

                // Map result to response DTO
                var response =
                    result.Adapt<GetProductByIdResponse>();

                // Return HTTP 200 OK
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(
                StatusCodes.Status200OK)
            .ProducesProblem(
                StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By ID")
            .WithDescription("Get product by its ID");
        }
    }
}
