using Microsoft.EntityFrameworkCore;
using SignalRChatTemplete.Hubs;

var builder = WebApplication.CreateBuilder(args);

#region ORM��Ʈw�s�u
    #region EF Core
    builder.Services.AddDbContext<SignalRChatTemplete.DBContexts.EFCore.ProjectDBContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });
    #endregion

    #region Dapper
    //���U���P��DB�s�u
    var projectDBContext_1 = new SignalRChatTemplete.DBContexts.Dapper.ProjectDBContext(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddSingleton(projectDBContext_1);
    //���UDapper��Repository
    builder.Services.AddScoped(typeof(CommomLibrary.Dapper.Repository.interfaces.IBaseDapper<>), typeof(CommomLibrary.Dapper.Repository.services.BaseDapper<>));
    #endregion
#endregion

// Add services to the container.
//�[�J SignalR
builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromHours(1); //�]�w����O�ɮɶ�
    options.KeepAliveInterval = TimeSpan.FromMinutes(10); //�]�w����E���ɶ�
    options.EnableDetailedErrors = true; //�ҥθԲӿ��~
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

//�[�J SignalR Hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
