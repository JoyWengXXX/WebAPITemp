using CommomLibrary.Authorization;
using CommomLibrary.Logging;
using CommomLibrary.AutofacHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SignalRChatTemplete.Hubs;
using SignalRTemplete.Filters;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using SignalRChatTemplete.Middleware;
using CommomLibrary.Dapper.Repository.interfaces;
using SignalRChatTemplete.DBContexts.Dapper;
using SignalRChatTemplete.AppInterfaceAdapters.interfaces;
using SignalRChatTemplete.AppInterfaceAdapters;

var builder = WebApplication.CreateBuilder(args);

#region ORM資料庫連線
    #region EF Core
    builder.Services.AddDbContext<SignalRChatTemplete.DBContexts.EFCore.ProjectDBContext_SignalR>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("SignalRConnection");
        options.UseSqlServer(connectionString);
    });
    #endregion

    #region Dapper
    //註冊不同的DB連線
    var projectDBContext_1 = new SignalRChatTemplete.DBContexts.Dapper.ProjectDBContext_SignalR(builder.Configuration.GetConnectionString("SignalRConnection"));
    var projectDBContext_2 = new SignalRChatTemplete.DBContexts.Dapper.ProjectDBContext_Default(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddSingleton(projectDBContext_1);
    builder.Services.AddSingleton(projectDBContext_2);
    //註冊Dapper的Repository
    builder.Services.AddScoped(typeof(CommomLibrary.Dapper.Repository.interfaces.IBaseDapper<>), typeof(CommomLibrary.Dapper.Repository.services.BaseDapper<>));
#endregion
#endregion

// Add services to the container.
//初始化並建立一個實例
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//註冊autofac這個容器
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister("SignalRChatTemplete.Services", Assembly.GetExecutingAssembly())));
//註冊其他介面
builder.Services.AddScoped<IProjectDBContext, ProjectDBContext_Default>();
builder.Services.AddScoped<IProjectDBContext, ProjectDBContext_SignalR>();
builder.Services.AddScoped<IAPIResponceAdapter, APIResponceAdapter>();
//加入 SignalR
builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromHours(1); //設定握手逾時時間
    options.KeepAliveInterval = TimeSpan.FromMinutes(10); //設定握手激活時間
    options.EnableDetailedErrors = true; //啟用詳細錯誤
});
#region JWT設定
JWTAuthorizationSetting.JWTSetting(builder);
#endregion

#region 針對JWT設定修改Swagger
builder.Services.AddSwaggerGen(options => {
    //使Swagger可以讀取註解
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //說明api如何受到保護
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        //選擇類型，type選擇http時，透過swagger畫面做認證時可以省略Bearer前綴詞
        Type = SecuritySchemeType.Http,
        //採用Bearer token
        Scheme = "Bearer",
        //bearer格式使用jwt
        BearerFormat = "JWT",
        //認證放在http request的header上
        In = ParameterLocation.Header,
        //描述
        Description = "JWT驗證描述"
    });
    //製作額外的過濾器，過濾Authorize、AllowAnonymous，甚至是沒有打attribute
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});
#endregion

#region 進行Serilog的註冊
SerilogSettingExtensions.UseSerilogSetting(builder);
#endregion

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

#region 自動執行最新的Migration
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SignalRChatTemplete.DBContexts.EFCore.ProjectDBContext_SignalR>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}
#endregion

#region 註冊MiddleWare
app.UseMiddleware<ErrorHandler>();
#endregion

app.UseHttpsRedirection();

//設定Hub要有JWT Token才能連線
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/chatHub"))
    {
        var bearerToken = context.Request.Query["access_token"].ToString();

        if (!String.IsNullOrEmpty(bearerToken))
            context.Request.Headers.Add("Authorization", "Bearer " + bearerToken);
    }

    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//加入 SignalR Hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
