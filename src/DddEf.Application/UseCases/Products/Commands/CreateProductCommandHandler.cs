using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Products.Commands
{
    public sealed class CreateProductCommandHandler(IDddEfContext applicationDbContext) : IRequestHandler<CreateProductCommand, ProductId>
    { 
        public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create
            (
                 request.ProductCode,
                 request.ProductName
            );
            await applicationDbContext.Products.AddAsync(product, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
