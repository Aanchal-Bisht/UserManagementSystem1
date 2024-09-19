using UMSAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("origin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5272")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<LogFileConfig>(builder.Configuration.GetSection("LogFileConfig"));

builder.Services.Configure<RegLogfileConfig>(builder.Configuration.GetSection("RegLogfileConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("origin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
