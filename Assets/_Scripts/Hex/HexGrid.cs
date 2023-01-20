using System;
using UnityEngine;

namespace _Scripts.Hex
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private GameObject hexTilePrefab;

        private HexRenderer _hexRenderer;

        private void Awake()
        {
            _hexRenderer = hexTilePrefab.GetComponent<HexRenderer>();
        }

        private void OnEnable()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    var tile = Instantiate(hexTilePrefab, transform, true);
                    tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));

                    var hexRenderer = tile.GetComponent<HexRenderer>();
                    hexRenderer.DrawMesh();
                }
            }
        }

        private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
        {
            var column = coordinate.x;
            var row = coordinate.y;

            float width,
                height,
                xPos,
                yPos,
                horizontalDistance,
                verticalDistance,
                offset, 
                size = _hexRenderer.OuterSize;
            
            bool shouldOffset;

            if (!_hexRenderer.IsFlatTopped)
            {
                shouldOffset = (row % 2) == 0;
                width = Mathf.Sqrt(3f) * size;
                height = 2f * size;

                horizontalDistance = width;
                verticalDistance = height * (3f / 4f);
                
                offset = (shouldOffset) ? width / 2f : 0f;

                xPos = column * horizontalDistance + offset;
                yPos = row * verticalDistance;

            }
            else
            {
                shouldOffset = (column % 2) == 0;

                height = Mathf.Sqrt(3f) * size;
                width = 2f * size;

                verticalDistance = height;
                horizontalDistance = width * (3f / 4f);
                
                offset = (shouldOffset) ? height / 2f : 0f;

                xPos = column * horizontalDistance;
                yPos = row * verticalDistance - offset;
            }

            return new Vector3(xPos, 0, -yPos);
        }
    }
}
