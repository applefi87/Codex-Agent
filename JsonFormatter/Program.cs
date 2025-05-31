using System.IO;
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

// 讀取專案目錄下的 test.json 檔案
var filePath = "test.json";
if (!File.Exists(filePath))
{
    logger.LogError("檔案 {FilePath} 不存在。", filePath);
    return;
}

string content;
try
{
    content = File.ReadAllText(filePath);
}
catch (Exception ex)
{
    logger.LogError(ex, "讀取檔案時發生錯誤: {FilePath}", filePath);
    return;
}

if (string.IsNullOrWhiteSpace(content))
{
    logger.LogWarning("檔案 {FilePath} 為空。", filePath);
    return;
}

var options = new JsonSerializerOptions { WriteIndented = true };
try
{
    using var doc = JsonDocument.Parse(content);
    if (doc.RootElement.ValueKind == JsonValueKind.Array)
    {
        foreach (var elem in doc.RootElement.EnumerateArray())
        {
            var formatted = JsonSerializer.Serialize(elem, options);
            Console.WriteLine(formatted);
        }
    }
    else
    {
        var formatted = JsonSerializer.Serialize(doc.RootElement, options);
        Console.WriteLine(formatted);
    }
}
catch (JsonException)
{
    // 嘗試以換行切分多個 JSON 物件 (ndjson)
    foreach (var line in content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
    {
        try
        {
            using var doc = JsonDocument.Parse(line);
            var formatted = JsonSerializer.Serialize(doc.RootElement, options);
            Console.WriteLine(formatted);
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "解析 JSON 行時發生錯誤: {JsonLine}", line);
        }
    }
}