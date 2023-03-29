using FractalGenerator;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddCommandLine(args);

        var configuration = builder.Build();

        var fractalConfigurations = configuration.GetSection("FractalConfigurations").GetChildren().ToList();

        int id = 0;

        foreach (var config in fractalConfigurations)
        {
            int seed = config.GetValue<int>("Seed");
            int width = config.GetValue<int>("Width");
            int height = config.GetValue<int>("Height");
            int fractal = config.GetValue<int>("Fractal");
            double juliaReal = config.GetValue<double>("JuliaReal");
            double juliaImag = config.GetValue<double>("JuliaImag");
            int maxIterations = config.GetValue<int>("MaxIterations");

            Console.WriteLine($"Generating fractal {id} with settings: Seed {seed}, Width {width}, Height {height}, Fractal {fractal}, JuliaReal {juliaReal}, JuliaImag {juliaImag}, MaxIterations {maxIterations}");

            Fractal fractalGenerator = new Fractal(id, seed, width, height, fractal, juliaReal, juliaImag, maxIterations);
            fractalGenerator.Generate();
            fractalGenerator.SaveBitmap();

            id++;
        }
    }
}