var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://[::]:33333");

var app = builder.Build();

app.MapGet("/reset", () =>
{
    Latency.ResetLatency();
    return "Application reset";
});
app.MapGet("/data", async () =>
{
    int latency = Latency.GetLatency();
    await Task.Delay(latency);
    return $"Application latency: {latency}";
});

app.Run();

static class Latency
{
    static int counter = 1;
    public static int GetLatency() => counter++ * 400;
    public static void ResetLatency() => counter = 1;
}