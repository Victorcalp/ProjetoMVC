using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Data;
using ProjetoMVC.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//faz conexão com banco MySQL
builder.Services.AddDbContext<Contexto>(options => options.UseMySql("server = localhost; initial catalog = ProjetoMVC; uid = developer; pwd = Victorc@lp0609 ", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31 - mysql")));

//Registra a classe no sistema de indepedencia da aplicação, permite que o serviço pode ser injetado em outros e tbm possa ser intejado
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesService>();

var app = builder.Build();

//Vai rodar o SeedingService para popular a base dados caso esteja vazia
app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

//Vai definir a localidade como US
var enUs = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUs),
    SupportedCultures = new List<CultureInfo> { enUs },
    SupportedUICultures = new List<CultureInfo> { enUs }
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
