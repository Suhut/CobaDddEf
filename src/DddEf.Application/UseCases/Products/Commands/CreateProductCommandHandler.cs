using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Infrastructure.Persistence;
using MediatR;

namespace DddEf.Application.UseCases.Products.Commands
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductId>
    {
        private readonly DddEfContext _dbContext;
        public CreateProductCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create
            (
                 request.ProductCode,
                 request.ProductName
            );
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product.Id;
        }
    }
}
