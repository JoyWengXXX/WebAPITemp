using CommomLibrary.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

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
