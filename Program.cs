using Microsoft.Extensions.Configuration;

var configuration =  new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile($"appsettings.json", optional: false);
            
var config = configuration.Build();
var apiKeyValue = config.GetSection("apiKeys").Value;

if(args.Length > 0)
{
     HttpClient client = new HttpClient();
     client.DefaultRequestHeaders.Add("authorization", apiKeyValue);
}
else
{
    System.Console.WriteLine("--> You need to provide some input ");
}