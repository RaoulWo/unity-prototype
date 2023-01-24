using System;
using UnityEngine;

namespace _Scripts.Hex.Core
{
    public class Orientation
    {
        public static readonly Orientation Flat = new Orientation(3f / 2f, 0f, Mathf.Sqrt(3f) / 2f, Mathf.Sqrt(3f), 
            2f / 3f, 0f, -1f / 3f, Mathf.Sqrt(3f) / 3f, 30);
        public static readonly Orientation Pointy = new Orientation(Math.Sqrt(3f), Math.Sqrt(3f) / 2f, 0f, 3f / 2f,
            Mathf.Sqrt(3f) / 3f, -1f / 3f, 0f, 2f / 3f, 0);

        public readonly double HexToPixel0, HexToPixel1, HexToPixel2, HexToPixel3;
        public readonly double PixelToHex0, PixelToHex1, PixelToHex2, PixelToHex3;
        public readonly int StartAngleDeg;

        public Orientation(double hexToPixel0, double hexToPixel1, double hexToPixel2, double hexToPixel3, 
            double pixelToHex0, double pixelToHex1, double pixelToHex2, double pixelToHex3, int startAngleDeg)   
        {
            HexToPixel0 = hexToPixel0;
            HexToPixel1 = hexToPixel1;
            HexToPixel2 = hexToPixel2;
            HexToPixel3 = hexToPixel3;
            PixelToHex0 = pixelToHex0;
            PixelToHex1 = pixelToHex1;
            PixelToHex2 = pixelToHex2;
            PixelToHex3 = pixelToHex3;
            StartAngleDeg = startAngleDeg;
        }
    }
}