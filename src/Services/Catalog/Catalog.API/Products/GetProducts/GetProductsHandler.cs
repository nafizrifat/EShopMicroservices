namespace Catalog.API.Products.GetProducts
{
    // Query - Read operation
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    // Result - Collection of products
    public record GetProductsResult(IEnumerable<Product> Products);

    // Query Handler
    internal class GetProductsQueryHandler( IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle( GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "GetProductsQueryHandler.Handle called");

            var products = await session
                .Query<Product>()
                .ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
