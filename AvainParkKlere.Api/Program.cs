
using AvainParkKlere.Api.EntityFrameworkCore;
using AvainParkKlere.Api.Repositories;
using AvainParkKlere.Api.RepositoryInterfaces;
using AvianParkKlere.Contracts.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AvainParkKlere.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Setup connection to the database
            var connectionString = builder.Configuration.GetConnectionString("AvianParkConnectionString");

            builder.Services.AddDbContext<AvianParkDbContext>(options => {
                options.UseSqlServer(connectionString);
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MapperConfig)); // AutoMapper Configuration

            // register data repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentClothingRepository, StudentClothingRepository>();
            builder.Services.AddScoped<IClothingRepository, ClothingRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
