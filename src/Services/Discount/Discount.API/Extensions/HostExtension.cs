using Npgsql;

namespace Discount.API.Extensions;

public static class HostExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int retry = 0)
    {

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            
            int retryForAvailabity = retry;
        
            try
            {
                logger.LogInformation("Migrating postgresql database");
                using var connection =
                    new NpgsqlConnection(connectionString);
                connection.Open();
            
                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };
            
                // Execute script from sql file
                // string script = File.ReadAllText(@"PATH");

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                            ProductName VARCHAR(24) NOT NULL,
                                                            Description TEXT,
                                                            Amount INT)";
                command.ExecuteNonQuery();


                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                command.ExecuteNonQuery();
                logger.LogInformation("Migrated postresql database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the postresql database");
                if (retryForAvailabity < 50)
                {
                    retryForAvailabity++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host,retryForAvailabity);
                }
            }

            return host;
        }
    }
}