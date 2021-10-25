using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RandomQuiz.Db;

namespace RandomQuiz.Extensions
{
    public static class DbServiceExtension
    {
        public static void AddDatabaseService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<QuestionContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
