


using CartProduct.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ShopDb"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartService>();
builder.Services.AddSession();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Products.AddRange(
        new Product { Name = "Harry potter", Price = 100000, Description = "Tập đầu tiên trong loạt truyện Harry Potter nổi tiếng của J.K. Rowling. " },
        new Product { Name = "Nhà giả kim", Price = 50000, Description = "Tác phẩm nổi tiếng của Paulo Coelho kể về hành trình đi tìm kho báu của chàng chăn cừu Santiago. " },
        new Product { Name = "Cha giàu cha nghèo", Price = 120000, Description = "Cuốn sách tài chính cá nhân bán chạy của Robert Kiyosaki, chia sẻ bài học tài chính từ hai người cha: một giàu, một nghèo." }
    );
    context.SaveChanges();
}
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
