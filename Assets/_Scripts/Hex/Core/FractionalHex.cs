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

        public static FractionalHex Round(FractionalHex hex)
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

            return new FractionalHex(q, r, s);
        }

        public static Hex RoundToHex(FractionalHex hex)
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
        
        public FractionalHex Round()
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

            return new FractionalHex(q, r, s);
        }

        public Hex RoundToHex()
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
    }
}