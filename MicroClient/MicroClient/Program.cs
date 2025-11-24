using System;
using System.Net.Http;
using System.Threading.Tasks;
internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.WriteLine("Client starting…");

        // Read URLs from environment variables or use defaults
        var loginUrl = Environment.GetEnvironmentVariable("LOGIN_URL") 
                       ?? "http://login:8080/api/auth/health";

        var matchmakerUrl = Environment.GetEnvironmentVariable("MATCHMAKER_URL") 
                            ?? "http://matchmakerservice:5000/Match/health";

        var storeUrl = Environment.GetEnvironmentVariable("STORE_URL") 
                       ?? "http://storeservice:8081/Store";   // GET /Store

        var playerUrl = Environment.GetEnvironmentVariable("PLAYER_URL") 
                        ?? "http://playerinfoservice:6000/api/player"; // GET /api/player

        var gameServerUrl = Environment.GetEnvironmentVariable("GAME_SERVEL_URL")
                        ?? "http://gameserver:5001/health";

        var relayRouterURL = Environment.GetEnvironmentVariable("RELAY_ROUTER_URL")
                        ?? "http://relayrouter:5002/health";

        using var httpClient = new HttpClient();

        while (true)
        {
            Console.WriteLine("=== Calling services ===");

            try
            {
                // Login
                Console.WriteLine($"Calling Login at: {loginUrl}");
                var loginResponse = await httpClient.GetAsync(loginUrl);
                loginResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Login OK: " + await loginResponse.Content.ReadAsStringAsync());

                // Matchmaker
                Console.WriteLine($"Calling Matchmaker at: {matchmakerUrl}");
                var matchResponse = await httpClient.GetAsync(matchmakerUrl);
                matchResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Matchmaker OK: " + await matchResponse.Content.ReadAsStringAsync());

                // Store (GET /Store)
                Console.WriteLine($"Calling Store at: {storeUrl}");
                var storeResponse = await httpClient.GetAsync(storeUrl);
                storeResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Store OK: " + await storeResponse.Content.ReadAsStringAsync());

                // Player (GET /api/player)
                Console.WriteLine($"Calling Player Info at: {playerUrl}");
                var playerResponse = await httpClient.GetAsync(playerUrl);
                playerResponse.EnsureSuccessStatusCode();
                Console.WriteLine("Player Info OK: " + await playerResponse.Content.ReadAsStringAsync());

                // Game Server
                Console.WriteLine($"Calling Player Info at: {gameServerUrl}");
                var gameServerRespone = await httpClient.GetAsync(gameServerUrl);
                gameServerRespone.EnsureSuccessStatusCode();
                Console.WriteLine("Player Info OK: " + await playerResponse.Content.ReadAsStringAsync());

                //Relay Router
                Console.WriteLine($"Calling Player Info at: {relayRouterURL}");
                var relayRouterRespone = await httpClient.GetAsync(relayRouterURL);
                relayRouterRespone.EnsureSuccessStatusCode();
                Console.WriteLine("Player Info OK: " + await relayRouterRespone.Content.ReadAsStringAsync());
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