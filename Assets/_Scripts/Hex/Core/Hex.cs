using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace _Scripts.Hex.Core
{
    public class Hex : IEquatable<Hex>
    {
        private static readonly List<Hex> AdjacentDirections = new()
        {
            new Hex(1, 0 , -1),
            new Hex(1, -1, 0),
            new Hex(0, -1, 1),
            new Hex(-1, 0, 1),
            new Hex(-1, 1, 0),
            new Hex(0, 1, -1)
        };

        private static readonly List<Hex> DiagonalDirections = new()
        {
            new Hex(2, -1, -1),
            new Hex(1, -2, 1),
            new Hex(-1, -1, 2),
            new Hex (-2, 1, 1),
            new Hex(-1, 2, -1),
            new Hex(1, 1, -2)
        };

        public int Q => v.x;
        public int R => v.y;
        public int S => v.z;

        private Vector3Int v;

        public Hex(Vector3Int v)
        {
            Debug.Assert(v.x + v.y + v.z == 0,"q + r + s == 0");
            this.v = v;
        }
        
        public Hex(int q, int r)
        {
            Debug.Assert(v.x + v.y + v.z == 0,"q + r + s == 0");
            v = new Vector3Int(q, r, -q - r);
        }

        public Hex(int q, int r, int s)
        {
            Debug.Assert(v.x + v.y + v.z == 0,"q + r + s == 0");
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
            if (obj.GetType() != GetType()) return false;
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

        public static Hex AdjacentDirection(int dir)
        {
            Debug.Assert(0 <= dir && dir < 6, "0 <= dir < 6");

            return AdjacentDirections[dir];
        }

        public static Hex DiagonalDirection(int dir)
        {
            Debug.Assert(0 <= dir && dir < 6, "0 <= dir < 6");

            return DiagonalDirections[dir];
        }

        public static Hex Neighbor(Hex hex, int dir)
        {
            return Add(hex, AdjacentDirection(dir));
        }

        public static Hex DiagonalNeighbor(Hex hex, int dir)
        {
            return Add(hex, DiagonalDirection(dir));
        }

        public static Hex RotateLeft(Hex hex)
        {
            return new Hex(-hex.S, -hex.Q, -hex.R);
        }

        public static Hex RotateRight(Hex hex)
        {
            return new Hex(-hex.R, -hex.S, -hex.Q);
        }

        public static List<Hex> Range(Hex center, int steps)
        {
            Debug.Assert(steps >= 0, "steps >= 0");

            var results = new List<Hex>();

            for (var q = -steps; q <= steps; q++)
            {
                for (var r = Mathf.Max(-steps, -q - steps); r <= Mathf.Min(steps, -q + steps); r++)
                {
                    results.Add(Add(center, new Hex(q, r, -q - r)));
                }
            }

            return results;
        }

        public static Hex ReflectQ(Hex hex)
        {
            return new Hex(hex.Q, hex.S, hex.R);
        }
        
        public static Hex ReflectR(Hex hex)
        {
            return new Hex(hex.S, hex.R, hex.Q);
        }

        public static Hex ReflectS(Hex hex)
        {
            return new Hex(hex.R, hex.Q, hex.S);
        }
        
        public static List<Hex> Ring(Hex center, int radius)
        {
            Debug.Assert(radius > 0, "radius > 0");

            var results = new List<Hex>();

            var hex = Add(center, Scale(AdjacentDirection(4), radius));

            for (var dir = 0; dir < 6; dir++)
            {
                for (var step = 0; step < radius; step++)
                {
                    results.Add(hex);
                    hex = Neighbor(hex, dir);
                }
            }
            
            return results;
        }

        public static List<Hex> Spiral(Hex center, int radius)
        {
            var results = new List<Hex> { center };

            for (var ring = 1; ring <= radius; ring++)
            {
                results.AddRange(Ring(center, ring));
            }
            
            return results;
        }

        public static List<Hex> DrawLine(Hex source, Hex target)
        {
            var results = new List<Hex>();
            
            var n = source.Distance(target);
            var leftNudge = new FractionalHex(source.Q + (float)1e-06, source.R + (float)1e-6, source.S - (float)2e-6);
            var rightNudge = new FractionalHex(target.Q + (float)1e-06, target.R + (float)1e-6, target.S - (float)2e-6);
            var step = 1f / Mathf.Max(n, 1);
            
            for (var i = 0; i <= n; i++)
            {
                results.Add(FractionalHex.RoundToHex(Lerp(leftNudge, rightNudge, step * i)));
            }

            return results;
        }

        private static FractionalHex Lerp(FractionalHex source, FractionalHex target, float t)
        {
            return new FractionalHex(source.Q * (1f - t) + target.Q * t, source.R * (1f - t) + target.R * t,
                source.S * (1f - t) + target.S * t);
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

        public Hex Neighbor(int dir)
        {
            return Add(AdjacentDirection(dir));
        }

        public Hex DiagonalNeighbor(int dir)
        {
            return Add(DiagonalDirection(dir));
        }

        public Hex RotateLeft()
        {
            return new Hex(-S, -Q, -R);
        }

        public Hex RotateRight()
        {
            return new Hex(-R, -S, -Q);
        }

        public List<Hex> Range(int steps)
        {
            Debug.Assert(steps >= 0, "steps >= 0");

            var results = new List<Hex>();

            for (var q = -steps; q <= steps; q++)
            {
                for (var r = Mathf.Max(-steps, -q - steps); r <= Mathf.Min(steps, -q + steps); r++)
                {
                    results.Add(Add(new Hex(q, r, -q - r)));
                }
            }

            return results;
        }
        
        public Hex ReflectQ()
        {
            return new Hex(Q, S, R);
        }
        
        public Hex ReflectR()
        {
            return new Hex(S, R, Q);
        }

        public Hex ReflectS()
        {
            return new Hex(R, Q, S);
        }

        public List<Hex> Ring(int radius)
        {
            Debug.Assert(radius > 0, "radius > 0");

            var results = new List<Hex>();

            var hex = Add(Scale(AdjacentDirection(4), radius));

            for (var dir = 0; dir < 6; dir++)
            {
                for (var step = 0; step < radius; step++)
                {
                    results.Add(hex);
                    hex = Neighbor(hex, dir);
                }
            }
            
            return results;
        }
        
        public List<Hex> Spiral(int radius)
        {
            var results = new List<Hex> { this };

            for (var ring = 1; ring <= radius; ring++)
            {
                results.AddRange(Ring(ring));
            }
            
            return results;
        }
        
        public List<Hex> DrawLine(Hex target)
        {
            var results = new List<Hex>();
            
            var n = Distance(target);
            var leftNudge = new FractionalHex(Q + (float)1e-06, R + (float)1e-6, S - (float)2e-6);
            var rightNudge = new FractionalHex(target.Q + (float)1e-06, target.R + (float)1e-6, target.S - (float)2e-6);
            var step = 1f / Mathf.Max(n, 1);
            
            for (var i = 0; i <= n; i++)
            {
                results.Add(FractionalHex.RoundToHex(Lerp(leftNudge, rightNudge, step * i)));
            }

            return results;
        }
    }
}