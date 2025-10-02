using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UPXV.Data;

namespace UPXV_API;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);

      IConfiguration config = builder.Configuration;

      builder.Services.AddCors();

      builder.Services.AddLogging();

      //builder.Services.AddAuthentication(options =>
      //{
      //   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      //   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      //   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      //}).AddJwtBearer(options =>
      //{
      //   options.TokenValidationParameters = new TokenValidationParameters
      //   {
      //      ValidateLifetime = true,

      //      ValidateAudience = false,
      //      ValidateIssuer = false,
      //      ValidateActor = false,
      //      ValidateIssuerSigningKey = false,
      //   };
      //});

      //builder.Services.AddAuthorization();

      builder.Services.AddServices();
      builder.Services.AddRepositories();
      builder.Services.AddValidators();
      builder.Services.AddControllers();

      builder.Services.AddMySQL(config);

      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      app.UseHttpsRedirection();

      //app.UseAuthentication();
      //app.UseAuthorization();

      app.UseSwagger();

      app.UseSwaggerUI(c => {
         c.SwaggerEndpoint("v1/swagger.json", "UPXV");
      });

      //app.UseAuthorization();

      app.MapControllers();

      app.Services.InitializeDatabase();

      app.Run();

   }
}
