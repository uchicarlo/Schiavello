using blazor_todo.Server.Context;
using blazor_todo.Server.Operations;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
builder.Services.AddDbContextFactory<ToDoContext>(options => options.UseSqlite("db/todo.db"));
builder.Services.AddGraphQLServer().RegisterDbContext<ToDoContext>().AddProjections().AddFiltering().AddSorting()
	.AddQueryType<Query>().AddMutationType<Mutation>();
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
