
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Exceptions;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

// Command: Data required to update a product
public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<UpdateProductResult>;

// Result: Indicates successful update
public record UpdateProductResult(bool IsSuccess);

// Command Handler
internal class UpdateProductCommandHandler(
    IDocumentSession session,
    ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "UpdateProductCommandHandler called for {ProductId}",
            command.Id);

        // 1. Retrieve existing product
        var product = await session.LoadAsync<Product>(
            command.Id,
            cancellationToken);

        // 2. Validate product exists
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        // 3. Update product properties
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        // 4. Register updated document
        session.Update(product);

        // 5. Save changes to PostgreSQL
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
