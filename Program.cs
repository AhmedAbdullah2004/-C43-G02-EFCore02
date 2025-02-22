using Microsoft.EntityFrameworkCore;

namespace Assignment_2_EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
           using ITIDbContext dbContext = new ITIDbContext();

            dbContext.Database.Migrate();


        }
    }
}
