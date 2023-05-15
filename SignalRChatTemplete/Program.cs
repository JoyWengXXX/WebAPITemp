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

#region ORM��Ʈw�s�u
    #region EF Core
    builder.Services.AddDbContext<SignalRChatTemplete.DBContexts.EFCore.ProjectDBContext_SignalR>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("SignalRConnection");
        options.UseSqlServer(connectionString);
    });
    #endregion

    #region Dapper
    //���U���P��DB�s�u
    var projectDBContext_1 = new SignalRChatTemplete.DBContexts.Dapper.ProjectDBContext_SignalR(builder.Configuration.GetConnectionString("SignalRConnection"));
    var projectDBContext_2 = new SignalRChatTemplete.DBContexts.Dapper.ProjectDBContext_Default(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddSingleton(projectDBContext_1);
    builder.Services.AddSingleton(projectDBContext_2);
    //���UDapper��Repository
    builder.Services.AddScoped(typeof(CommomLibrary.Dapper.Repository.interfaces.IBaseDapper<>), typeof(CommomLibrary.Dapper.Repository.services.BaseDapper<>));
#endregion
#endregion

// Add services to the container.
//��l�ƨëإߤ@�ӹ��
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//���Uautofac�o�Ӯe��
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister("SignalRChatTemplete.Services", Assembly.GetExecutingAssembly())));
//���U��L����
builder.Services.AddScoped<IProjectDBContext, ProjectDBContext_Default>();
builder.Services.AddScoped<IProjectDBContext, ProjectDBContext_SignalR>();
builder.Services.AddScoped<IAPIResponceAdapter, APIResponceAdapter>();
//�[�J SignalR
builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromHours(1); //�]�w����O�ɮɶ�
    options.KeepAliveInterval = TimeSpan.FromMinutes(10); //�]�w����E���ɶ�
    options.EnableDetailedErrors = true; //�ҥθԲӿ��~
});
#region JWT�]�w
JWTAuthorizationSetting.JWTSetting(builder);
#endregion

#region �w��JWT�]�w�ק�Swagger
builder.Services.AddSwaggerGen(options => {
    //��Swagger�i�HŪ������
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //����api�p�����O�@
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        //��������Atype���http�ɡA�z�Lswagger�e�����{�Үɥi�H�ٲ�Bearer�e���
        Type = SecuritySchemeType.Http,
        //�ĥ�Bearer token
        Scheme = "Bearer",
        //bearer�榡�ϥ�jwt
        BearerFormat = "JWT",
        //�{�ҩ�bhttp request��header�W
        In = ParameterLocation.Header,
        //�y�z
        Description = "JWT���Ҵy�z"
    });
    //�s�@�B�~���L�o���A�L�oAuthorize�BAllowAnonymous�A�ƦܬO�S����attribute
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});
#endregion

#region �i��Serilog�����U
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

#region �۰ʰ���̷s��Migration
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

#region ���UMiddleWare
app.UseMiddleware<ErrorHandler>();
#endregion

app.UseHttpsRedirection();

//�]�wHub�n��JWT Token�~��s�u
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

//�[�J SignalR Hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
