using Shortly.Data;
using Shortly.Data.Models;

namespace Shortly.Client.Data
{
    public static class DbInitializer
    {
        public static void SeedDefaultData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.Add(new User()
                    {
                        FullName = "Phiwe Pohleli",
                        Email = "pohlelip@gmail.com"
                    });
                    dbContext.SaveChanges();
                }

                if (!dbContext.Urls.Any())
                {
                    dbContext.Urls.Add(new Url()
                    {
                        OriginalLink = "https://dotnet.how.net",
                        ShortLink = "dtnet",
                        NrOfClicks = 20,
                        DateCreated = DateTime.Now,

                        userId = dbContext.Users.First().Id
                    });
                    dbContext.SaveChanges();

                }
            }
        }
    }
}
