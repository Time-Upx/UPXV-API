using FluentValidation;
using UPXV.Data.Repositories;
using UPXV.Models;
using UPXV.Services;
using UPXV.Validation;

namespace UPXV_API;

public static class ComponentRegistry
{
   public static IServiceCollection AddServices (this IServiceCollection services) => services
      .AddScoped<ConsumableService>()
      .AddScoped<PatrimonyService>()
      .AddScoped<StatusService>()
      .AddScoped<UnitService>()
      .AddScoped<TagService>();
   public static IServiceCollection AddRepositories (this IServiceCollection services) => services
      .AddScoped<ConsumableRepository>()
      .AddScoped<PatrimonyRepository>()
      .AddScoped<StatusRepository>()
      .AddScoped<UnitRepository>()
      .AddScoped<TagRepository>()
      .AddScoped<ItemService>();
   public static IServiceCollection AddValidators (this IServiceCollection services) => services
      .AddScoped<IValidator<Consumable>, ConsumableValidator>()
      .AddScoped<IValidator<Patrimony>, PatrimonyValidator>()
      .AddScoped<IValidator<Status>, StatusValidator>()
      .AddScoped<IValidator<Unit>, UnitValidator>()
      .AddScoped<IValidator<Tag>, TagValidator>();
}
