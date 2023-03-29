# Fractal Generator

Fractal Generator is a console application that was created using ChatGPT-4, it generates various types of fractal images based on user-defined configurations. The application supports multiple fractal types and allows users to customize their properties. Users can provide input through command-line arguments or a JSON configuration file (`appsettings.json`).

## Usage

You can run the application using the following command:

> dotnet run -- [OPTIONS]

The available options are:

| Option        | Description                                            | Default | Required for |
|---------------|--------------------------------------------------------|---------|--------------|
| --seed        | The seed value for randomizing the fractal image       | 0       | All          |
| --width       | The width of the output image                          | 500     | All          |
| --height      | The height of the output image                         | 500     | All          |
| --fractal     | The type of fractal to generate (0-8)                  | 0       | All          |
| --julia-real  | The real part of the complex constant for Julia set    | 0.285   | 1            |
| --julia-imag  | The imaginary part of the complex constant for Julia set| 0.01    | 1            |
| --max-iterations | The maximum number of iterations for the fractal calculation | 1000 | All |

Fractal types:

| Fractal ID | Fractal Type          | Required Parameters  |
|------------|-----------------------|----------------------|
| 0          | Mandelbrot Set        | None                 |
| 1          | Julia Set             | --julia-real, --julia-imag |
| 2          | Burning Ship          | None                 |
| 3          | Newton Fractal        | None                 |
| 4          | Sierpinski Triangle   | None                 |
| 5          | Koch Snowflake        | None                 |
| 6          | Barnsley Fern         | None                 |
| 7          | Lyapunov Fractal      | None                 |
| 8          | Tricorn               | None                 |

### Configuration file

You can provide multiple fractal configurations in the `appsettings.json` file. The application will generate fractals for each configuration in the file. Here's an example `appsettings.json` file:

```json
{
  "FractalConfigurations": [
    {
      "Seed": 0,
      "Width": 500,
      "Height": 500,
      "Fractal": 0,
      "JuliaReal": 0.285,
      "JuliaImag": 0.01,
      "MaxIterations": 1000
    },
    {
      "Seed": 1,
      "Width": 500,
      "Height": 500,
      "Fractal": 1,
      "JuliaReal": 0.285,
      "JuliaImag": 0.01,
      "MaxIterations": 1000
    }
  ]
}

Command-line arguments will override the values provided in the appsettings.json file.

## Output 

The application generates .bmp image files in the current directory with file names based on the seed value (e.g., 0.bmp, 1.bmp).