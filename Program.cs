using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SocialConnectAPI.DataAccess;
using SocialConnectAPI.DataAccess.CommentLike;
using SocialConnectAPI.DataAccess.Comments;
using SocialConnectAPI.DataAccess.PostLike;
using SocialConnectAPI.DataAccess.Posts;
using SocialConnectAPI.DataAccess.Users;
using SocialConnectAPI.Models;
using SocialConnectAPI.Services.Comments;
using SocialConnectAPI.Services.Posts;
using SocialConnectAPI.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(setup =>
        // Return 406 for unsupported formats
    {
        setup.ReturnHttpNotAcceptable = true;
    })
    .AddXmlDataContractSerializerFormatters();
    // Display enums as strings in swagger
    // .AddJsonOptions(options =>
    // {
    //     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialConnectAPI", Version = "v1" });

    // XML documentation file path in /bin folder
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<CommentService, CommentService>();
builder.Services.AddScoped<ICommentLikeRepository, CommentLikeRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<PostService, PostService>();
builder.Services.AddScoped<IPostLikeRepository, PostLikeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService, UserService>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialConnectAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
