using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {
                    UserName = "swn",
                    FirstName = "Mehmet",
                    LastName = "Ozkaya",
                    EmailAddress = "ezozkme@gmail.com",
                    CVV = "39875",
                    AddressLine = "Bahcelievler",
                    Country = "Turkey",
                    TotalPrice = 350,
                    CardName = "PKO visa",
                    CardNumber = "1234-5678-9012-3456",
                    PaymentMethod = 1,
                    Expiration = DateTime.Now.AddDays(14).ToString(),
                    ZipCode = "123-45",
                    State = "California"
                }
            };
        }
    }
}
