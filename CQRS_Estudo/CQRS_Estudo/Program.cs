using CQRS_Read_Aplication.People;
using CQRS_Read_Infrastructure.Persistence.People;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.Events;
using CQRS_Write_Infrastructure.Commands;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<ICommandBus, CommandBus>();
builder.Services.AddSingleton<IEventPublisher, CommandBus>();
builder.Services.AddSingleton<ICommandEventRepository, CommandEventRepository>();

var app = builder.Build();

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
