using blazor_todo.Server.Operations;
using blazor_todo.Server.Services;
using blazor_todo.Server.Utility;
using blazor_todo.Shared.Interface;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppSettingsHelper.EnableLogger();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ITodoServices,TodoServices>();

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>()
    .AddErrorFilter(error =>
    {
        Log.Error(Convert.ToString(error.Exception));
        return error;
    });
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapGraphQL();
app.MapFallbackToFile("index.html");

app.Run();
