using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace RayTracingWeekendOne;

/*
 * Vector3 will be a struct.
 * In C#, struct types are allocated in the stack.
 */

public readonly struct Vector3
{
    // X, Y, Z Coords for Space coordinates
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    // R, G, B Values for Color coordinates
    public double R => X;
    public double G => Y;
    public double B => Z;

    // Vector3 Constructor
    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double SquaredLength() => X * X + Y * Y + Z * Z;

    /*
     * When overloading C# binary operators,
     * it automatically overloads its compound equivalent.
     * I.E., we get both Vec3 + Vec3 and Vec += with a single overload
     */

    // Vector addition
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator +(Vector3 v1, Vector3 v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

    // Vector subtraction
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator -(Vector3 v1, Vector3 v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

    // Vector multiplication
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator *(Vector3 v1, Vector3 v2) => new(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);

    // Scalar multiplication
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator *(Vector3 v, double t) => new(v.X * t, v.Y * t, v.Z * t);

    // Scalar multiplication
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator *(double t, Vector3 v) => new(v.X * t, v.Y * t, v.Z * t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator /(Vector3 v, double t) => new(v.X / t, v.Y / t, v.Z / t);

    // Unit Vector
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 UnitVector(Vector3 v) => v / v.Length();

    // Dot Product
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Dot(Vector3 v1, Vector3 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    // Cross Product
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Cross(Vector3 v1, Vector3 v2) =>
        new(v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X);

    public void PrintVector3()
    {
        Console.WriteLine("{0} {1} {2}", X, Y, Z);
    }
}