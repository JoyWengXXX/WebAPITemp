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
#region ORM��Ʈw�s�u
    #region EF Core
    builder.Services.AddDbContext<WebAPITemplete.DBContexts.EFCore.ProjectDBContext_Default>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });
#endregion

    #region Dapper
    //���U���P��DB�s�u
    var projectDBContext_1 = new WebAPITemplete.DBContexts.Dapper.ProjectDBContext_Default(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddSingleton(projectDBContext_1);
    //���UDapper��Repository
    builder.Services.AddScoped(typeof(CommomLibrary.Dapper.Repository.interfaces.IBaseDapper<>), typeof(CommomLibrary.Dapper.Repository.services.BaseDapper<>));
    #endregion
#endregion

#region ���U�UService
//��l�ƨëإߤ@�ӹ��
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//���Uautofac�o�Ӯe��
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister("WebAPITemplete.Services", "Service", Assembly.GetExecutingAssembly())));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister("WebAPITemplete.Models.Mapper", "Mapper", Assembly.GetExecutingAssembly())));

//���U��L����
builder.Services.AddScoped<IProjectDBContext, ProjectDBContext_Default>();
builder.Services.AddScoped<IAPIResponceAdapter, APIResponceAdapter>();
#endregion

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

var app = builder.Build();

#region ���UMiddleWare
app.UseMiddleware<ErrorHandler>();
#endregion

#region �۰ʰ���̷s��Migration
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
