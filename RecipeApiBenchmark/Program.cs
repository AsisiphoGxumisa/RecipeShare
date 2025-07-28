using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var client = new HttpClient();
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 500; i++)
        {
            var response = await client.GetAsync("https://localhost:7055/api/recipes");
            response.EnsureSuccessStatusCode();
        }

        stopwatch.Stop();

        Console.WriteLine($"Total Time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Average Latency: {stopwatch.ElapsedMilliseconds / 500.0} ms/request");
        Console.ReadLine();
    }
}
