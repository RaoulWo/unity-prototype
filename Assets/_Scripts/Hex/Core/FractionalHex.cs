using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace _Scripts.Hex.Core
{
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
}