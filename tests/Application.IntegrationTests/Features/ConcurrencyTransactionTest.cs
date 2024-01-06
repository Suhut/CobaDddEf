using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Domain.Aggregates.Customer;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features;

using static Testing;
public class ConcurrencyTransactionTest : BaseTestFixture
{

    [Test]
    public async Task Test123ya()
    {
 
        var command01 = new CreateCustomerCommand
        (
            "Code001",
            "Name001"
        ); 
        var customer01 = await SendAsync(command01); 
        

        var entity01_1 = await FindAsync<Customer>(customer01);
        var entity01_2 = await FindAsync<Customer>(customer01);

        //entity01_1.CustomerCode = "Item01 Name update 1";
        //await UpdateAsync(entity1);

        //entity2.ItemName = "Item01 Name update 2";

        //await FluentActions.Invoking(() =>
        //    UpdateAsync(entity2)).Should().ThrowAsync<ConcurrencyException>();


    }

}
