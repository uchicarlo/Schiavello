using blazor_todo.Server.Context;
using blazor_todo.Server.Operations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<ToDoContext>();
builder.Services.AddDbContext<ToDoContext>(options => options.UseSqlite("Data Source=DB\\todo.db"), ServiceLifetime.Transient);

builder.Services.AddGraphQLServer().RegisterDbContext<ToDoContext>().AddProjections().AddFiltering().AddSorting()
	.AddQueryType<Query>().AddMutationType<Mutation>();
var app = builder.Build();
var serviceScopeFactory = (IServiceScopeFactory)app
	   .Services.GetService(typeof(IServiceScopeFactory));

using (var scope = serviceScopeFactory.CreateScope())
{
	var services = scope.ServiceProvider;

	var dbContext = services.GetRequiredService<ToDoContext>();
	dbContext.Database.EnsureCreated();
}


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
