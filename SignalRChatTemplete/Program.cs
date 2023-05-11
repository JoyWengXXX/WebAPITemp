using CommomLibrary.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//加入 SignalR
builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromHours(1); //設定握手逾時時間
    options.KeepAliveInterval = TimeSpan.FromMinutes(10); //設定握手激活時間
    options.EnableDetailedErrors = true; //啟用詳細錯誤
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//加入 SignalR Hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
