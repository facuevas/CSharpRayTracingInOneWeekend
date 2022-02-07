
using Microsoft.VisualBasic.CompilerServices;

namespace RayTracingWeekendOne;

static class Program
{
    public static void Main()
    {
        // Store our image.ppm file into our My Documents folder for Windows
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Image Size Constants
        const double aspectRatio = 16.0 / 9.0;
        const int imageWidth = 800;
        const int imageHeight = (int) (imageWidth / aspectRatio);

        // Camera configuartions
        var viewportHeight = 2.0;
        var viewportWidth = aspectRatio * viewportHeight;
        var focalLength = 1.0;

        // Camera Location
        var origin = new Vector3(0, 0, 0);
        var horizontal = new Vector3(viewportWidth, 0, 0);
        var vertical = new Vector3(0, viewportHeight, 0);
        var lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focalLength);

        // Append text to our image.ppm file
        using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "image.ppm"));
        
        // Render

        outputFile.Write("P3\n" + imageWidth + " " + imageHeight + "\n255\n");

        for (int j = imageHeight - 1; j >= 0; --j)
        {
            Console.WriteLine("Scanlines remaining: {0}", j);
            for (int i = 0; i < imageWidth; ++i)
            {
                var u = (double) i / (imageWidth - 1);
                var v = (double) j / (imageHeight - 1);
                Ray ray = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
                Vector3 pixelColor = RayColor(ref ray);
                WriteColor(pixelColor, outputFile);
            }
        }

        Console.WriteLine("Done!");
    }

    private static void WriteColor(Vector3 color, StreamWriter output)
    {
        output.WriteLine("{0} {1} {2}", (int) (255.999 * color.R), (int) (255.999 * color.G),
            (int) (255.999 * color.B));
    }

    private static Vector3 RayColor(ref Ray r)
    {
        Vector3 vector3 = new Vector3(0, 0, -1);
        var t = HitSphere(ref vector3, 0.5, ref r);

        if (t > 0.0)
        {
            Vector3 normal = Vector3.UnitVector(r.PointAt(t) - new Vector3(0,0, -1));
            return 0.5 * new Vector3(normal.X + 1, normal.Y + 1, normal.Z + 1);
        }

        Vector3 unitDirection = Vector3.UnitVector(r.Direction);
        t = 0.5 * (unitDirection.Y + 1.0);
        return (1.0 - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5, 0.7, 1.0);
    }

    private static double HitSphere(ref Vector3 center, double radius, ref Ray r)
    {
        Vector3 oc = r.Origin - center;
        var a = r.Direction.SquaredLength();
        var halfB = Vector3.Dot(oc, r.Direction);
        var c = oc.SquaredLength() - radius * radius;
        var discriminant = halfB * halfB - a * c;

        if (discriminant < 0)
        {
            return -1.0;
        }
        else
        {
            return (-halfB - Math.Sqrt(discriminant)) / a;
        }
    }
}