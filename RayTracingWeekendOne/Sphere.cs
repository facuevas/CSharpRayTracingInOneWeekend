namespace RayTracingWeekendOne;

public class Sphere : Hittable
{
    public Vector3 Center { get; }
    public double Radius { get; }

    public Sphere()
    {

    }

    public Sphere(Vector3 center, double radius)
    {
        Center = center;
        Radius = radius;
    }
    
    public override bool Hit(ref Ray r, double tMin, double tMax, ref HitRecord record)
    {
        Vector3 oc = r.Origin - Center;
        var a = r.Direction.SquaredLength();
        var halfB = Vector3.Dot(oc, r.Direction);
        var c = oc.SquaredLength() - Radius * Radius;

        var discriminate = halfB * halfB - a * c;
        if (discriminate < 0) return false;

        var squareRootDiscriminate = Math.Sqrt(discriminate);
        
        // Find the nearest root that lies in the acceptable range.
        var root = (-halfB - squareRootDiscriminate) / a;
        if (root < tMin || tMax < root)
        {
            root = (-halfB + squareRootDiscriminate) / a;
            if (root < tMin || tMax < root) return false;
        }

        record.T = root;
        record.Point = r.PointAt(record.T);
        Vector3 outwardNormal = (record.Point - Center) / Radius;
        record.SetFaceNormal(ref r, ref outwardNormal);
        
        

        return true;
    }
}