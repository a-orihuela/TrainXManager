using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class CADbContextSeedData
    {

        private const string dirDataAppOptions = "../CleanArchitecture.Data/Data/appOptions.json";

        public static async Task LoadDataAsync(CADbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.AppOptions!.Any())
                {
                    var data = File.ReadAllText(dirDataAppOptions);
                    var items = JsonSerializer.Deserialize<List<AppOption>>(data);
                    await context.AppOptions!.AddRangeAsync(items!);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<CADbContextSeedData>();
                logger.LogError(ex.Message);
            }
        }
    }
}
