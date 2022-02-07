using System.Runtime.CompilerServices;

namespace RayTracingWeekendOne;

public struct HitRecord
{
    public Vector3 Point;
    public Vector3 Normal;
    public double T;
    public bool FrontFace;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetFaceNormal(ref Ray r, ref Vector3 outwardNormal)
    {
        FrontFace = Vector3.Dot(r.Direction, outwardNormal) < 0;
        Normal = FrontFace ? outwardNormal : -1 * outwardNormal;
    }
}

public abstract class Hittable
{
    public abstract bool Hit(ref Ray r, double tMin, double tMax, ref HitRecord record);
}