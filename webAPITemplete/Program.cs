using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.OpenApi.Models;
using WebAPITemplete.Filters;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WebAPITemplete.Middleware;
using CommomLibrary.Authorization;
using CommomLibrary.Logging;
using CommomLibrary.Dapper.Repository.interfaces;
using WebAPITemplete.DBContexts.Dapper;
using WebAPITemplete.AppInterfaceAdapters.interfaces;
using WebAPITemplete.AppInterfaceAdapters;
using CommomLibrary.AutofacHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region ORM資料庫連線
    #region EF Core
    builder.Services.AddDbContext<WebAPITemplete.DBContexts.EFCore.ProjectDBContext_Default>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });
#endregion

    #region Dapper
    //註冊不同的DB連線
    var projectDBContext_1 = new WebAPITemplete.DBContexts.Dapper.ProjectDBContext_Default(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddSingleton(projectDBContext_1);
    //註冊Dapper的Repository
    builder.Services.AddScoped(typeof(CommomLibrary.Dapper.Repository.interfaces.IBaseDapper<>), typeof(CommomLibrary.Dapper.Repository.services.BaseDapper<>));
    #endregion
#endregion

#region 註冊各Service
//初始化並建立一個實例
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//註冊autofac這個容器
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister("WebAPITemplete.Services", "Service", Assembly.GetExecutingAssembly())));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister("WebAPITemplete.Models.Mapper", "Mapper", Assembly.GetExecutingAssembly())));

//註冊其他介面
builder.Services.AddScoped<IProjectDBContext, ProjectDBContext_Default>();
builder.Services.AddScoped<IAPIResponceAdapter, APIResponceAdapter>();
#endregion

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

var app = builder.Build();

#region 註冊MiddleWare
app.UseMiddleware<ErrorHandler>();
#endregion

#region 自動執行最新的Migration
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context1 = services.GetRequiredService<WebAPITemplete.DBContexts.EFCore.ProjectDBContext_Default>();
        context1.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
