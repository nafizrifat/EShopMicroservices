namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Step 1: Create Product entity
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // Step 2: Save product to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Step 3: Return result (temporary Guid)
            return new CreateProductResult(product.Id);
        }
    }
}

