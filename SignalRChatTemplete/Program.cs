using Microsoft.EntityFrameworkCore;
using SignalRChatTemplete.Hubs;

var builder = WebApplication.CreateBuilder(args);

#region ORM資料庫連線
    #region EF Core
    builder.Services.AddDbContext<SignalRChatTemplete.DBContexts.EFCore.ProjectDBContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });
    #endregion

    #region Dapper
    //註冊不同的DB連線
    var projectDBContext_1 = new SignalRChatTemplete.DBContexts.Dapper.ProjectDBContext(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddSingleton(projectDBContext_1);
    //註冊Dapper的Repository
    builder.Services.AddScoped(typeof(CommomLibrary.Dapper.Repository.interfaces.IBaseDapper<>), typeof(CommomLibrary.Dapper.Repository.services.BaseDapper<>));
    #endregion
#endregion

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
