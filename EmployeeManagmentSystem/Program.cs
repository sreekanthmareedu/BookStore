using BookStoreAPI.Data;
using BookStoreAPI.MappingConfig;
using BookStoreAPI.Repository;
using BookStoreAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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

app.Run();
