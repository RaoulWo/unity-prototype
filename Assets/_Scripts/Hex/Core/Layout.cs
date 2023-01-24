using UnityEngine;

namespace _Scripts.Hex.Core
{
    public class Layout
    {
        public readonly Orientation Orientation;
        
        private readonly Vector2 _origin;
        private readonly Vector2 _size;

        public Layout(Orientation orientation, Vector2 origin, Vector2 size)
        {
            Orientation = orientation;
            _origin = origin;
            _size = size;
        }

        public Vector2 HexToPos(Hex hex)
        {
            var x = (float)(Orientation.HexToPixel0 * hex.Q + Orientation.HexToPixel1 * hex.R) * _size.x;
            var y = (float)(Orientation.HexToPixel2 * hex.Q + Orientation.HexToPixel3 * hex.R) * _size.y;
            return new Vector2(x + _origin.x, y + _origin.y);
        }
        
        public Vector2 HexToPos(FractionalHex hex)
        {
            var x = (float)(Orientation.HexToPixel0 * hex.Q + Orientation.HexToPixel1 * hex.R) * _size.x;
            var y = (float)(Orientation.HexToPixel2 * hex.Q + Orientation.HexToPixel3 * hex.R) * _size.y;
            return new Vector2(x + _origin.x, y + _origin.y);
        }

        public Hex PosToHex(Vector2 pos)
        {
            return PosToFractionalHex(pos).RoundToHex();
        }
        
        public FractionalHex PosToFractionalHex(Vector2 pos)
        {
            var tmp = new Vector2((pos.x - _origin.x) / _size.x, (pos.y - _origin.y) / _size.y);
            var q = (float)(Orientation.PixelToHex0 * tmp.x + Orientation.PixelToHex1 * tmp.y);
            var r = (float)(Orientation.PixelToHex2 * tmp.x + Orientation.PixelToHex3 * tmp.y);
            return new FractionalHex(q, r, -q - r);
        }
    }
}