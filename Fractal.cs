using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace FractalGenerator
{
    public class Fractal
    {
        private Bitmap _bitmap;
        private float _seed;
        private FractalType _fractalType;
        private int _id;
        private double _juliaReal;
        private double _juliaImag;
        private int _maxIterations;
        private int _width;
        private int _height;


        public Fractal(int id, int seed, int width, int height, int fractal, double juliaReal, double juliaImag, int maxIterations)
        {
            _bitmap = new Bitmap(width, height);
            _seed = seed;
            _fractalType = (FractalType)fractal;
            _id = id;
            _maxIterations = maxIterations;
            _juliaReal = juliaReal;
            _juliaImag = juliaImag;
            _width = width;
            _height = height;
        }

        public void SaveBitmap()
        {
            string fileName = $"fractal_{_id}_seed_{_seed}.bmp";
            _bitmap.Save(fileName, ImageFormat.Bmp);
            Console.WriteLine($"Fractal {_id} saved as {fileName}");
        }

        public void Generate()
        {
            // Use a switch statement to call the appropriate fractal generation method
            switch (_fractalType)
            {
                case FractalType.Mandelbrot:
                    GenerateMandelbrot();
                    break;
                case FractalType.Julia:
                    GenerateJulia();
                    break;
                case FractalType.BurningShip:
                    GenerateBurningShip();
                    break;
                case FractalType.Newton:
                    GenerateNewton();
                    break;
                case FractalType.SierpinskiTriangle:
                    GenerateSierpinskiTriangle();
                    break;
                case FractalType.KochSnowflake:
                    GenerateKochSnowflake();
                    break;
                case FractalType.BarnsleyFern:
                    GenerateBarnsleyFern();
                    break;
                case FractalType.Lyapunov:
                    GenerateLyapunov();
                    break;
                case FractalType.MengerSponge:
                    GenerateMengerSponge();
                    break;
                case FractalType.HilbertCurve:
                    GenerateHilbertCurve();
                    break;
                case FractalType.Tricorn:
                    GenerateTricorn();
                    break;
            }
        }

        private void GenerateMandelbrot()
        {
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 1000;

            for (int x = 0; x < _bitmap.Width; x++)
            {
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    double a = MapRange(x, 0, _bitmap.Width, -2, 2);
                    double b = MapRange(y, 0, _bitmap.Height, -2, 2);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);

                    int iterations = 0;
                    while (iterations < maxIterations && z.Magnitude < 4)
                    {
                        z = z * z + c;
                        iterations++;
                    }

                    double colorValue = MapRange(iterations, 0, maxIterations, 0, 1);
                    Color color = Color.FromArgb((int)(colorValue * 255), 0, (int)((1 - colorValue) * 255));
                    _bitmap.SetPixel(x, y, color);
                }
            }
        }

        private void GenerateJulia()
        {
            Complex c = new Complex(_juliaReal, _juliaImag);
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 5000;
            double xMin = -2.0;
            double xMax = 2.0;
            double yMin = -2.0;
            double yMax = 2.0;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    double xCoord = xMin + (xMax - xMin) * x / (_width - 1);
                    double yCoord = yMin + (yMax - yMin) * y / (_height - 1);

                    Complex z = new Complex(xCoord, yCoord);

                    int iteration = 0;
                    while (iteration < _maxIterations && z.Magnitude < 2)
                    {
                        z = Complex.Pow(z, 2) + c;
                        iteration++;
                    }

                    double hue = iteration < _maxIterations ? iteration / (double)_maxIterations : 0;
                    Color color = ColorUtils.ColorFromHue(hue);
                    _bitmap.SetPixel(x, y, color);
                }
            }
        }

        private void GenerateBurningShip()
        {
            int maxIterations = 1000;

            for (int x = 0; x < _bitmap.Width; x++)
            {
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    double a = MapRange(x, 0, _bitmap.Width, -2, 2);
                    double b = MapRange(y, 0, _bitmap.Height, -2, 2);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);

                    int iterations = 0;
                    while (iterations < maxIterations && z.Magnitude < 4)
                    {
                        double real = Math.Abs(z.Real);
                        double imaginary = Math.Abs(z.Imaginary);
                        z = new Complex(real, imaginary) * new Complex(real, imaginary) + c;
                        iterations++;
                    }

                    double colorValue = MapRange(iterations, 0, maxIterations, 0, 1);
                    Color color = Color.FromArgb((int)(colorValue * 255), 0, (int)((1 - colorValue) * 255));
                    _bitmap.SetPixel(x, y, color);
                }
            }
        }

        private void GenerateNewton()
        {
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 1000;
            double tolerance = 1e-6;
            Complex[] roots = { new Complex(1, 0), new Complex(-0.5, Math.Sqrt(3) / 2), new Complex(-0.5, -Math.Sqrt(3) / 2) };

            for (int x = 0; x < _bitmap.Width; x++)
            {
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    double a = MapRange(x, 0, _bitmap.Width, -2, 2);
                    double b = MapRange(y, 0, _bitmap.Height, -2, 2);
                    Complex z = new Complex(a, b);

                    int iterations = 0;
                    while (iterations < maxIterations)
                    {
                        Complex fz = z * z * z - new Complex(1, 0);
                        Complex dfz = 3 * z * z;
                        z = z - fz / dfz;

                        if (roots.Any(root => (z - root).Magnitude < tolerance))
                        {
                            break;
                        }

                        iterations++;
                    }

                    double colorValue = MapRange(iterations, 0, maxIterations, 0, 1);
                    Color color = Color.FromArgb((int)(colorValue * 255), 0, (int)((1 - colorValue) * 255));
                    _bitmap.SetPixel(x, y, color);
                }
            }
        }


        private void GenerateSierpinskiTriangle()
        {
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 100000;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            // Define the vertices of the triangle
            PointF A = new PointF(width / 2f, 0);
            PointF B = new PointF(0, height);
            PointF C = new PointF(width, height);

            // Choose a random starting point inside the triangle
            Random random = new Random();
            PointF point = new PointF(random.Next(width), random.Next(height));

            for (int i = 0; i < maxIterations; i++)
            {
                // Choose a random vertex and move halfway towards it
                int vertex = random.Next(3);
                switch (vertex)
                {
                    case 0:
                        point = new PointF((point.X + A.X) / 2, (point.Y + A.Y) / 2);
                        break;
                    case 1:
                        point = new PointF((point.X + B.X) / 2, (point.Y + B.Y) / 2);
                        break;
                    case 2:
                        point = new PointF((point.X + C.X) / 2, (point.Y + C.Y) / 2);
                        break;
                }

                // Color the pixel at the new position
                _bitmap.SetPixel((int)point.X, (int)point.Y, Color.Black);
            }
        }


        private void GenerateKochSnowflake()
        {
            int iterations = _maxIterations is not 0 ? _maxIterations : 5;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            // Define the vertices of the triangle
            PointF A = new PointF(width / 2f, 0);
            PointF B = new PointF(0, height);
            PointF C = new PointF(width, height);

            // Generate the Koch curves
            List<Line> lines = GenerateKochCurve(A, B, iterations);
            lines.AddRange(GenerateKochCurve(B, C, iterations));
            lines.AddRange(GenerateKochCurve(C, A, iterations));

            // Draw the Koch Snowflake
            using (Graphics graphics = Graphics.FromImage(_bitmap))
            {
                foreach (Line line in lines)
                {
                    graphics.DrawLine(Pens.Black, line.Start, line.End);
                }
            }
        }

        private List<Line> GenerateKochCurve(PointF start, PointF end, int iterations)
        {
            if (iterations == 0)
            {
                return new List<Line> { new Line(start, end) };
            }

            PointF A = start;
            PointF B = new PointF((2 * start.X + end.X) / 3, (2 * start.Y + end.Y) / 3);
            PointF D = new PointF((start.X + 2 * end.X) / 3, (start.Y + 2 * end.Y) / 3);

            double angle = -Math.PI / 3;
            double dx = D.X - B.X;
            double dy = D.Y - B.Y;
            PointF C = new PointF((float)(B.X + dx * Math.Cos(angle) - dy * Math.Sin(angle)), (float)(B.Y + dx * Math.Sin(angle) + dy * Math.Cos(angle)));

            List<Line> lines = GenerateKochCurve(A, B, iterations - 1);
            lines.AddRange(GenerateKochCurve(B, C, iterations - 1));
            lines.AddRange(GenerateKochCurve(C, D, iterations - 1));
            lines.AddRange(GenerateKochCurve(D, end, iterations - 1));

            return lines;
        }


        private void GenerateBarnsleyFern()
        {
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 50000;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            PointF point = new PointF(0, 0);
            Random random = new Random();

            for (int i = 0; i < maxIterations; i++)
            {
                float rnd = (float)random.NextDouble();

                float newX, newY;

                if (rnd < 0.01f)
                {
                    newX = 0;
                    newY = 0.16f * point.Y;
                }
                else if (rnd < 0.86f)
                {
                    newX = 0.85f * point.X + 0.04f * point.Y;
                    newY = -0.04f * point.X + 0.85f * point.Y + 1.6f;
                }
                else if (rnd < 0.93f)
                {
                    newX = 0.2f * point.X - 0.26f * point.Y;
                    newY = 0.23f * point.X + 0.22f * point.Y + 1.6f;
                }
                else
                {
                    newX = -0.15f * point.X + 0.28f * point.Y;
                    newY = 0.26f * point.X + 0.24f * point.Y + 0.44f;
                }

                point = new PointF(newX, newY);

                int x = (int)((point.X + 2.5) * (width / 5.5));
                int y = (int)((height - 20) - (point.Y * (height / 11)));

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    _bitmap.SetPixel(x, y, Color.Green);
                }
            }
        }

        private void GenerateLyapunov()
        {
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 1000;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            double aMin = 2.0, aMax = 4.0, bMin = 2.0, bMax = 4.0;
            double aStep = (aMax - aMin) / width;
            double bStep = (bMax - bMin) / height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double a = aMin + x * aStep;
                    double b = bMin + y * bStep;

                    double x0 = 0.5;
                    double sum = 0;

                    for (int i = 0; i < maxIterations; i++)
                    {
                        double r = (i % 2 == 0) ? a : b;
                        x0 = r * x0 * (1 - x0);

                        if (i > 100) // Skip first 100 iterations for stability
                        {
                            double derivative = Math.Abs(r * (1 - 2 * x0));
                            sum += Math.Log(derivative);
                        }
                    }

                    double lambda = sum / (maxIterations - 100);

                    if (lambda >= 0)
                    {
                        int colorValue = (int)Math.Min(255, 5 * lambda);
                        _bitmap.SetPixel(x, y, Color.FromArgb(colorValue, 0, 0));
                    }
                    else
                    {
                        int colorValue = (int)Math.Min(255, -5 * lambda);
                        _bitmap.SetPixel(x, y, Color.FromArgb(0, 0, colorValue));
                    }
                }
            }
        }


        private void GenerateMengerSponge()
        {
            int iterations = _maxIterations is not 0 ? _maxIterations : 4;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (IsMengerSpongePixel(x, y, iterations))
                    {
                        _bitmap.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        _bitmap.SetPixel(x, y, Color.White);
                    }
                }
            }
        }

        private bool IsMengerSpongePixel(int x, int y, int iterations)
        {
            while (iterations > 0)
            {
                if ((x % 3 == 1) && (y % 3 == 1))
                {
                    return false;
                }

                x /= 3;
                y /= 3;
                iterations--;
            }

            return true;
        }


        private void GenerateHilbertCurve()
        {
            int iterations = _maxIterations is not 0 ? _maxIterations : 5;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            int n = 1 << iterations;
            float cellSize = (float)width / n;

            using (Graphics graphics = Graphics.FromImage(_bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.Clear(Color.White);
                Pen pen = new Pen(Color.Black, 1);

                PointF prevPoint = new PointF(cellSize / 2, cellSize / 2);
                for (int i = 1; i < n * n; i++)
                {
                    int x, y;
                    HilbertIndexToXY(i, iterations, out x, out y);
                    PointF nextPoint = new PointF(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);

                    graphics.DrawLine(pen, prevPoint, nextPoint);
                    prevPoint = nextPoint;
                }
            }
        }

        private void HilbertIndexToXY(int index, int iterations, out int x, out int y)
        {
            x = y = 0;
            for (int s = 1, t = index; s < (1 << iterations); s <<= 1, t >>= 2)
            {
                int rx = 1 & (t >> 1);
                int ry = 1 & (t ^ rx);
                Rotate(s, ref x, ref y, rx, ry);
                x += s * rx;
                y += s * ry;
            }
        }

        private void Rotate(int n, ref int x, ref int y, int rx, int ry)
        {
            if (ry == 0)
            {
                if (rx == 1)
                {
                    x = n - 1 - x;
                    y = n - 1 - y;
                }

                int temp = x;
                x = y;
                y = temp;
            }
        }


        private void GenerateTricorn()
        {
            int maxIterations = _maxIterations is not 0 ? _maxIterations : 1000;
            int width = _bitmap.Width;
            int height = _bitmap.Height;

            double xMin = -2, xMax = 1, yMin = -1.5, yMax = 1.5;
            double xRange = xMax - xMin;
            double yRange = yMax - yMin;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double real = xMin + x * xRange / width;
                    double imag = yMin + y * yRange / height;

                    Complex c = new Complex(real, imag);
                    Complex z = Complex.Zero;

                    int iterations;
                    for (iterations = 0; iterations < maxIterations; iterations++)
                    {
                        z = Complex.Conjugate(z) * Complex.Conjugate(z) + c;

                        if (z.Magnitude > 2.0)
                        {
                            break;
                        }
                    }

                    if (iterations == maxIterations)
                    {
                        _bitmap.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        int colorValue = (int)(255 * (double)iterations / maxIterations);
                        _bitmap.SetPixel(x, y, Color.FromArgb(colorValue, colorValue, colorValue));
                    }
                }
            }
        }


        private static double MapRange(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }
    }
}
