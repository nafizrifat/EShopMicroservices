namespace Catalog.API.Products.GetProductsByCategory
{

    // Query
    public record GetProductsByCategoryQuery(string Category)
        : IQuery<GetProductsByCategoryResult>;

    // Result
    public record GetProductsByCategoryResult(
        IEnumerable<Product> Products);

    internal class GetProductsByCategoryQueryHandler(
        IDocumentSession session,
        ILogger<GetProductsByCategoryQueryHandler> logger)
        : IQueryHandler<
            GetProductsByCategoryQuery,
            GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(
            GetProductsByCategoryQuery query,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "GetProductsByCategory query: {Category}",
                query.Category);

            var products = await session
                .Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
