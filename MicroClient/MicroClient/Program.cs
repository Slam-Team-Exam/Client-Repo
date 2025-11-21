using System;
using System.Net.Http;
using System.Threading.Tasks;
internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.WriteLine("Client starting…");

        // Read URLs from environment variables or use defaults
        var loginUrl = Environment.GetEnvironmentVariable("LOGIN_URL") ?? "http://login:8080/api/auth/health";
        var matchmakerUrl = Environment.GetEnvironmentVariable("MATCHMAKER_URL") ?? "http://matchmakerservice:5000/Match/health";

        using var httpClient = new HttpClient();

        try
        {
            // Call login service
            Console.WriteLine($"Calling Login service at: {loginUrl}");
            var loginResponse = await httpClient.GetAsync(loginUrl);
            loginResponse.EnsureSuccessStatusCode();
            Console.WriteLine("Login service responded:");
            Console.WriteLine(await loginResponse.Content.ReadAsStringAsync());

            // Call matchmaker service
            Console.WriteLine($"Calling Matchmaker service at: {matchmakerUrl}");
            var matchResponse = await httpClient.GetAsync(matchmakerUrl);
            matchResponse.EnsureSuccessStatusCode();
            Console.WriteLine("Matchmaker service responded:");
            Console.WriteLine(await matchResponse.Content.ReadAsStringAsync());

            // Exit code 0 = success
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client failed:");
            Console.WriteLine(ex);

            // Non-zero exit code indicates failure in CI/Compose
            return 1;
        }
    }
}