using BookStoreAPI.Data;
using BookStoreAPI.MappingConfig;
using BookStoreAPI.Repository;
using BookStoreAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


/*
Binding db connection 
============================
    string to project*/

builder.Services.AddDbContext<ApplicationDBContext>(
    
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionDetails"));


    }
    
    
    
    );


//==================================================================
//Adding Auto mapper

builder.Services.AddAutoMapper(typeof(MappingConfig));


//=================================================================
//IRepository dependency Injection

builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(u =>
{
    u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u => {

    u.RequireHttpsMetadata = false;
    u.SaveToken = true;
    u.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,

        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                  },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

    options.SwaggerDoc("v1", new OpenApiInfo
    {

        Version = "v1",
        Title = "Magic Villa v1",
        Description = "API to Manage Villa",
        TermsOfService = new Uri("http://example.com/terms")
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {

        Version = "v2",
        Title = "Magic Villa v2",
        Description = "API to Manage Villa",
        TermsOfService = new Uri("http://example.com/terms")
    });
});


var app = builder.Build();

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
