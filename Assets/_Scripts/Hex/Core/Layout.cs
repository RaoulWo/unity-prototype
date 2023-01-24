using System;
using UnityEngine;

namespace _Scripts.Hex.Core
{
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