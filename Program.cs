using Figgle;
using AsciiArtSvc;

const StringComparison SCIC = StringComparison.OrdinalIgnoreCase;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", (int? skip, int? take) =>
//     AsciiArt.AllFonts.Value
//         .Skip(skip ?? 0)
//         .Take(take ?? 100));

app.MapGet("/",
    (
        int? skip,
        int? take,
        FiggleTextDirection? dir,
        string? name,
        string? order
    ) => {
        var query = from f in AsciiArt.AllFonts.Value
            where (name == null || f.Name.Contains(name, SCIC)) && (dir == null || f.Font.Direction == dir)
            select f;

        if (string.Equals("desc", order, SCIC))
        {
            query = query.OrderByDescending(f => f.Name);
        }
        else
        {
            query = query.OrderBy(f => f.Name);
        }

        return query
            .Skip(skip ?? 0)
            .Take(take ?? 200)
            .Select(f => f.Name);
    });

app.MapGet("/{text}", (string text, string? font) => AsciiArt.Write(text, font));

app.Run();
