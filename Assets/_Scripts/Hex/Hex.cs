using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace _Scripts.Hex
{
    public class Hex : IEquatable<Hex>
    {
        private static readonly List<Hex> Directions = new List<Hex>
        {
            new Hex(1, 0 , -1),
            new Hex(1, -1, 0),
            new Hex(0, -1, 1),
            new Hex(-1, 0, 1),
            new Hex(-1, 1, 0),
            new Hex(0, 1, -1)
        };
        
        public int Q => v.x;
        public int R => v.y;
        public int S => v.z;
        
        private readonly Vector3Int v;

        public Hex(Vector3Int v)
        {
            Debug.Assert(v.x + v.y + v.z == 0,"q + r + s must be 0");
            this.v = v;
        }
        
        public Hex(int q, int r)
        {
            Debug.Assert(v.x + v.y + v.z == 0,"q + r + s must be 0");
            v = new Vector3Int(q, r, -q - r);
        }

        public Hex(int q, int r, int s)
        {
            Debug.Assert(v.x + v.y + v.z == 0,"q + r + s must be 0");
            v = new Vector3Int(q, r, s);
        }

        public bool Equals(Hex other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(v, other.v);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Hex)obj);
        }

        public override int GetHashCode()
        {
            return v.GetHashCode();
        }

        public static bool operator ==(Hex left, Hex right)
        {
            Debug.Assert(left != null, nameof(left) + " != null");
            Debug.Assert(right != null, nameof(right) + " != null");
            return left.Q == right.Q && left.R == right.R && left.S == right.S;
        }

        public static bool operator !=(Hex left, Hex right)
        {
            return !(left == right);
        }

        public static Hex Add(Hex left, Hex right)
        {
            return new Hex(left.v + right.v);
        }

        public static Hex Subtract(Hex left, Hex right)
        {
            return new Hex(left.v - right.v);
        }

        public static Hex Scale(Hex hex, int k)
        {
            return new Hex(hex.v * k);
        }

        public static int Length(Hex hex)
        {
            return (Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) / 2;
        }

        public static int Distance(Hex left, Hex right)
        {
            return Length(Subtract(left, right));
        }

        public static Hex Direction(int direction)
        {
            Debug.Assert(0 <= direction && direction < 6, "direction must be between 0 and 5");

            return Directions[direction];
        }

        public static Hex Neighbor(Hex hex, int direction)
        {
            return Add(hex, Direction(direction));
        }

        public static Hex RotateLeft(Hex hex)
        {
            return new Hex(-hex.S, -hex.Q, -hex.R);
        }

        public static Hex RotateRight(Hex hex)
        {
            return new Hex(-hex.R, -hex.S, -hex.Q);
        }

        public Hex Add(Hex other)
        {
            return new Hex(v + other.v);
        }

        public Hex Subtract(Hex other)
        {
            return new Hex(v - other.v);
        }

        public Hex Scale(int k)
        {
            return new Hex(v * k);
        }

        public int Length()
        {
            return (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;
        }

        public int Distance(Hex other)
        {
            return Length(Subtract(other));
        }

        public Hex Neighbor(int direction)
        {
            return Add(Direction(direction));
        }

        public Hex RotateLeft()
        {
            return new Hex(-S, -Q, -R);
        }

        public Hex RotateRight()
        {
            return new Hex(-R, -S, -Q);
        }
    }

    public class FractionalHex
    {
        public float Q => v.x;
        public float R => v.y;
        public float S => v.z;
        
        private readonly Vector3 v;
        
        public FractionalHex(Vector3 v)
        {
            Debug.Assert(Mathf.RoundToInt(v.x + v.y + v.z) == 0,"q + r + s must be 0");
            this.v = v;
        }
        
        public FractionalHex(float q, float r)
        {
            Debug.Assert(Mathf.RoundToInt(v.x + v.y + v.z) == 0,"q + r + s must be 0");
            v = new Vector3(q, r, -q - r);
        }

        public FractionalHex(float q, float r, float s)
        {
            Debug.Assert(Mathf.RoundToInt(v.x + v.y + v.z) == 0,"q + r + s must be 0");
            v = new Vector3(q, r, s);
        }

        public static Hex Round(FractionalHex hex)
        {
            var q = Mathf.RoundToInt(hex.Q);
            var r = Mathf.RoundToInt(hex.R);
            var s = Mathf.RoundToInt(hex.S);
            var qDiff = Mathf.Abs(q - hex.Q);
            var rDiff = Mathf.Abs(r - hex.R);
            var sDiff = Mathf.Abs(s - hex.S);

            if (qDiff > rDiff && qDiff > sDiff)
            {
                q = -r - s;
            }
            else
            {
                if (rDiff > sDiff)
                {
                    r = -q - s;
                }
                else
                {
                    s = -q - r;
                }
            }

            return new Hex(q, r, s);
        }

        public static FractionalHex Lerp(FractionalHex source, FractionalHex target, float t)
        {
            return new FractionalHex(source.Q * (1f - t) + target.Q * t, source.R * (1f - t) + target.R * t,
                source.S * (1f - t) + target.S * t);
        }

        public static List<Hex> DrawLine(Hex left, Hex right)
        {
            var results = new List<Hex>();
            
            var n = left.Distance(right);
            var leftNudge = new FractionalHex(left.Q + (float)1e-06, left.R + (float)1e-6, left.S - (float)2e-6);
            var rightNudge = new FractionalHex(right.Q + (float)1e-06, right.R + (float)1e-6, right.S - (float)2e-6);
            var step = 1f / Mathf.Max(n, 1);
            
            for (var i = 0; i <= n; i++)
            {
                results.Add(Round(Lerp(leftNudge, rightNudge, step * i)));
            }

            return results;
        }
        
        public Hex Round()
        {
            var q = Mathf.RoundToInt(Q);
            var r = Mathf.RoundToInt(R);
            var s = Mathf.RoundToInt(S);
            var qDiff = Mathf.Abs(q - Q);
            var rDiff = Mathf.Abs(r - R);
            var sDiff = Mathf.Abs(s - S);

            if (qDiff > rDiff && qDiff > sDiff)
            {
                q = -r - s;
            }
            else
            {
                if (rDiff > sDiff)
                {
                    r = -q - s;
                }
                else
                {
                    s = -q - r;
                }
            }

            return new Hex(q, r, s);
        }

        public FractionalHex Lerp(FractionalHex target, float t)
        {
            return new FractionalHex(Q * (1f - t) + target.Q * t, R * (1f - t) + target.R * t,
                S * (1f - t) + target.S * t);
        }
    }

    public class Orientation
    {
        public readonly double HexToPixel0, HexToPixel1, HexToPixel2, HexToPixel3;
        public readonly double PixelToHex0, PixelToHex1, PixelToHex2, PixelToHex3;
        public readonly double StartAngle; // in multiples of 60 deg

        public Orientation(double hexToPixel0, double hexToPixel1, double hexToPixel2, double hexToPixel3, 
            double pixelToHex0, double pixelToHex1, double pixelToHex2, double pixelToHex3, double startAngle)
        {
            HexToPixel0 = hexToPixel0;
            HexToPixel1 = hexToPixel1;
            HexToPixel2 = hexToPixel2;
            HexToPixel3 = hexToPixel3;
            PixelToHex0 = pixelToHex0;
            PixelToHex1 = pixelToHex1;
            PixelToHex2 = pixelToHex2;
            PixelToHex3 = pixelToHex3;
            StartAngle = startAngle;
        }
    }

    public class Layout
    {
        public static Orientation Pointy = new Orientation(Math.Sqrt(3f), Math.Sqrt(3f) / 2f, 0f, 3f / 2f,
            Mathf.Sqrt(3f) / 3f, -1f / 3f, 0f, 2f / 3f, 0.5f);

        public static Orientation Flat = new Orientation(3f / 2f, 0f, Mathf.Sqrt(3f) / 2f, Mathf.Sqrt(3f), 
            2f / 3f, 0f, -1f / 3f, Mathf.Sqrt(3f) / 3f, 0f);

        public readonly Orientation Orientation;
        public readonly Vector2 Origin;
        public readonly Vector2 Size;

        public Layout(Orientation orientation, Vector2 origin, Vector2 size)
        {
            Orientation = orientation;
            Origin = origin;
            Size = size;
        }

        public Vector2 HexToPixel(Hex hex)
        {
            var x = (float)(Orientation.HexToPixel0 * hex.Q + Orientation.HexToPixel1 * hex.R) * Size.x;
            var y = (float)(Orientation.HexToPixel2 * hex.Q + Orientation.HexToPixel3 * hex.R) * Size.y;
            return new Vector2(x + Origin.x, y + Origin.y);
        }
        
        public FractionalHex PixelToHex(Vector2 p)
        {
            var point = new Vector2((p.x - Origin.x) / Size.x, (p.y - Origin.y) / Size.y);
            var q = (float)(Orientation.PixelToHex0 * point.x + Orientation.PixelToHex1 * point.y);
            var r = (float)(Orientation.PixelToHex2 * point.x + Orientation.PixelToHex3 * point.y);
            return new FractionalHex(q, r, -q - r);
        }
    }
}