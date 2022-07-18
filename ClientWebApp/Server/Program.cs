var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddApplicationService();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddDbContext<NorthwindDbContext>();
builder.Services.AddScoped<SampleDataSeeder>();
builder.Services.AddSingleton<IDateTime, MachineDateTime>();
builder.Services.AddSyncfusionBlazor(opt =>
{
    opt.IgnoreScriptIsolation = true;
});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<SwaggerDefaultValues>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<SampleDataSeeder>();
await dbInitializer.SeedAllAsync(CancellationToken.None);

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayOperationId();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Trader Api");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
