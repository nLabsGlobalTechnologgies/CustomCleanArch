var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(cfr =>
//{
//    cfr.AddDefaultPolicy(plc =>
//    {
//        plc.AllowAnyMethod();
//        plc.AllowAnyHeader();
//        plc.AllowAnyOrigin();
//        plc.WithOrigins();
//    });
//});

//builder.Services.AddApplication();
//builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
