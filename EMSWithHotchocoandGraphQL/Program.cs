using EMSWithHotchocoandGraphQL.MutationResolver;
using EMSWithHotchocoandGraphQL.QueryResolver;
using EMSWithHotchocoandGraphQL.Services;
using HotChocolate.Execution.Processing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

//starts here
builder.Services.AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<UserQueryResolver>()
    .AddMutationType(m => m.Name("Mutation"))
    .AddType<UserMutationResolver>();
    //.AddSubscriptionType<Subscription>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapGraphQL();
    _ = app.MapControllers();

});

app.Run();
