using AsciiArtSvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/{text}", (string text, string? font) => AsciiArt.Write(text, font));

app.Run();
