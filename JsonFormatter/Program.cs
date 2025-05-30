using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // 可在此處註冊其他服務
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("JSON Formatter 應用程式啟動");

var input = Console.ReadLine();
if (string.IsNullOrWhiteSpace(input))
{
    logger.LogWarning("未輸入任何 JSON 字串");
    return;
}

try
{
    using var jsonDoc = JsonDocument.Parse(input);
    var options = new JsonSerializerOptions { WriteIndented = true };
    var formattedJson = JsonSerializer.Serialize(jsonDoc.RootElement, options);
    Console.WriteLine(formattedJson);
}
catch (JsonException ex)
{
    logger.LogError(ex, "無效的 JSON 輸入");
}