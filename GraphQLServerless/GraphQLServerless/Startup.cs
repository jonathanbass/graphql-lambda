using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using GraphQLServerless.Models;
using GraphQLServerless.Repository;
using GraphQLServerless.Services;

namespace GraphQLServerless;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var awsOptions = Configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        services.AddSingleton<IDataRepository, DataRepository>();
        services.AddErrorFilter<GraphQLErrorFilter>();
        services.AddGraphQLServer()
            .AddQueryType<MovieService>()
            .AddMutationType<MutationType>()
            .AddFiltering();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapGraphQL());
    }
}