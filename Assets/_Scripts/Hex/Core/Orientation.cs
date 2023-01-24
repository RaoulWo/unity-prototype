namespace _Scripts.Hex.Core
{
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
}