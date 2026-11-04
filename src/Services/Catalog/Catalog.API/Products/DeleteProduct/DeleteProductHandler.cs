namespace Catalog.API.Products.DeleteProduct;

// Command: Delete product by ID
public record DeleteProductCommand(Guid Id)
    : ICommand<DeleteProductResult>;

// Result: Return deletion status
public record DeleteProductResult(bool IsSuccess);

// Command Handler
internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler called for {ProductId}", command.Id);

        // Register product for deletion
        session.Delete<Product>(command.Id);

        // Commit changes to PostgreSQL
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
