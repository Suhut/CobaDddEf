using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Products.Commands;
public record CreateProductCommand
(
   string ProductCode,
   string ProductName
) : IRequest<ProductId>;
