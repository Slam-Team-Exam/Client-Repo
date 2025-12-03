internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.WriteLine("Client starting…");

        var loginUrl = "http://localhost:8080/api/auth/health";
        var matchmakerUrl = "http://localhost:5000/Match/health";
        var storeUrl = "http://localhost:8081/Store";
        var playerUrl = "http://localhost:6000/api/player/GetPlayerInfo";
        var relayRouterURL = "http://localhost:5002/health";



        using var httpClient = new HttpClient();

        while (true)
        {
            Console.WriteLine("=== Calling services ===");

            try
            {
                Console.WriteLine($"Calling Login at: {loginUrl}");
                var loginResponse = await httpClient.GetAsync(loginUrl);
                loginResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Login OK: " + await loginResponse.Content.ReadAsStringAsync());

                Console.WriteLine($"Calling Matchmaker at: {matchmakerUrl}");
                var matchResponse = await httpClient.GetAsync(matchmakerUrl);
                matchResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Matchmaker OK: " + await matchResponse.Content.ReadAsStringAsync());

                Console.WriteLine($"Calling Player Info at: {playerUrl}");
                var playerResponse = await httpClient.GetAsync(playerUrl);
                playerResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Player Info OK: " + await playerResponse.Content.ReadAsStringAsync());

                Console.WriteLine($"Calling Store at: {storeUrl}");
                var storeResponse = await httpClient.GetAsync(storeUrl);
                storeResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Store OK: " + await storeResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client error:");
                Console.WriteLine(ex);
            }

            Console.WriteLine("Waiting 10 seconds…");
            await Task.Delay(10_000);
        }
    }
}