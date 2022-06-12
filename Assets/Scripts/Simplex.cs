using UnityEngine;

public class Simplex
{
    private Vector3[] _points;

    private int _size;

    public Simplex()
    {
        _points = new Vector3[4];
        _size = 0;
    }

    public void Push(Vector3 point)
    {
        _points[0] = point;
        _points[1] = _points[0];
        _points[2] = _points[1];
        _points[3] = _points[2];
  
        _size = Mathf.Min(_size + 1, 4);
    }

    public bool NextSimplex(ref Vector3 direction)
    {
        switch(_size)
        {
            case 2: 
                return Line(ref direction);
            case 3: 
                return Triangle(ref direction);
            case 4: 
                return Tetrahedron(ref direction);
        }
        
        return false;
    }

    private bool Line(ref Vector3 direction)
    {
        var a = _points[0];
        var b = _points[1];

        var ab = b - a;
        var ao = -a;

        if (SameDirection(ab, ao))
            direction = Vector3.Cross(Vector3.Cross(ab, ao), ab);       // (ab x ao) x ab
        else
        {
            SetPoints(a);                                               // [a, 0, 0, 0]
            direction = ao;
        }   
        
        return false;
    }

    private bool Triangle(ref Vector3 direction)
    {
        var a = _points[0];
        var b = _points[1];
        var c = _points[2];

        var ab = b - a;
        var ac = c - a;
        var ao = -a;

        var abc = Vector3.Cross(ab, ac);

        if (SameDirection(Vector3.Cross(abc, ac), ao))                  // (abc x ac) • ao > 0
        {
            if (SameDirection(ac, ao))
            {
                SetPoints(a, c);                                        // [a, c, 0, 0]
                direction = Vector3.Cross(Vector3.Cross(ac, ao), ac);   // (ac x ao) x ac
            }
            else
            {
                SetPoints(a, b);                                        // [a, b, 0, 0]
                return Line(ref direction);
            }
        }
        else
        {
            if (SameDirection(Vector3.Cross(ab, abc), ao))              // (ab x abc) • ao > 0
            {
                SetPoints(a, b);                                        // [a, b, 0, 0]
                return Line(ref direction);
            }
            else
            {
                if (SameDirection(abc, ao))
                    direction = abc;
                else
                {
                    SetPoints(a, c, b);                                 // [a, c, b, 0]
                    direction = -abc;
                }
            }
        }

        return false;
    }

    private bool Tetrahedron(ref Vector3 direction)
    {
        var a = _points[0];
        var b = _points[1];
        var c = _points[2];
        var d = _points[3];

        var ab = b - a;
        var ac = c - a;
        var ad = d - a;
        var ao = -a;

        var abc = Vector3.Cross(ab, ac);
        var acd = Vector3.Cross(ac, ad);
        var adb = Vector3.Cross(ad, ab);

        if (SameDirection(abc, ao)) 
        {
            SetPoints(a, b, c);
            return Triangle(ref direction);
        }

        if (SameDirection(acd, ao))
        {
            SetPoints(a, c, d);
            return Triangle(ref direction);
        }

        if (SameDirection(adb, ao))
        {
            SetPoints(a, d, b);
            return Triangle(ref direction);
        }

        return true;
    }

    private bool SameDirection(Vector3 direction, Vector3 ao)
        => Vector3.Dot(direction, ao) > 0;

    private void SetPoints(Vector3 point)
    {
        _points = new Vector3[4];
        _points[0] = point;
        _size = 1;
    }

    private void SetPoints(Vector3 pointA, Vector3 pointB)
    {
        _points = new Vector3[4];
        _points[0] = pointA;
        _points[1] = pointB;
        _size = 2;
    }

    private void SetPoints(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
        _points = new Vector3[4];
        _points[0] = pointA;
        _points[1] = pointB;
        _points[2] = pointC;
        _size = 3;
    }
}
