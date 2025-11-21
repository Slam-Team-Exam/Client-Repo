using System;
using System.Net.Http;
using System.Threading.Tasks;
internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Client starting…");

        // You can override this from Docker Compose using environment variables
        var serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL")
                         ?? "http://service:8080/health";

        using var httpClient = new HttpClient();

        try
        {
            Console.WriteLine($"Calling: {serviceUrl}");

            var response = await httpClient.GetAsync(serviceUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Service responded with:");
            Console.WriteLine(content);

            // Exit code 0 = success (important for automated walking skeleton tests)
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client failed:");
            Console.WriteLine(ex);

            // Non-zero exit code indicates failure in CI/Compose
            Environment.Exit(1);
        }
    }
}