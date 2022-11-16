using Microsoft.EntityFrameworkCore;
using PetShopWeb.Data;
using PetShopWeb.Repositories.AnimalRepository;
using PetShopWeb.Repositories.CategoryRepository;
using PetShopWeb.Repositories.CommentRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<PetShopContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var ctx = scope.ServiceProvider.GetRequiredService<PetShopContext>();
//    ctx.Database.EnsureDeleted();
//    ctx.Database.EnsureCreated();
//}

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=HomePage}/{id?}");
});
app.Run();
