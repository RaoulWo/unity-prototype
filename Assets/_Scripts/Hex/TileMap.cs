using System.Collections.Generic;
using System.Linq;
using _Scripts.Hex.Core;
using UnityEngine;

namespace _Scripts.Hex
{
    public class TileMap : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;

        public readonly Layout Layout = new Layout(Orientation.Flat, Vector2.zero, Vector2.one);
        
        private readonly Dictionary<(int q, int r), Tile> _dict = new();


        private void Awake()
        {
            var qLimit = 3;

            for (var i = -qLimit; i <= qLimit; i++)
            {
                var r1 = Mathf.Max(-qLimit, -i - qLimit);
                var r2 = Mathf.Min(qLimit, -i + qLimit);

                for (var j = r1; j <= r2; j++)
                {
                    var obj = Instantiate(tilePrefab);
                    var tile = obj.GetComponent<Tile>();
                    
                    tile.Init(i, j, -i - j);
                    Insert(tile);

                    var pos = Layout.HexToPos(tile.Hex);
                    obj.transform.position = new Vector3(pos.x, 0f, pos.y);


                    obj.transform.RotateAround(obj.transform.position, Vector3.up, Layout.Orientation.StartAngleDeg);
                }
            }

            var tiles = GetAll();

            foreach (var tile in tiles)
            {
                Debug.Log((tile.Hex.Q, tile.Hex.R, tile.Hex.S));
            }
        }


        public List<Tile> GetAll()
        {
            return _dict.Values.ToList();
        }

        public Tile Get((int q, int r) key)
        {
            return _dict.TryGetValue(key, out var value) ? value : null;
        }

        public bool Insert(Tile tile)
        {
            return _dict.TryAdd((q: tile.Hex.Q, r: tile.Hex.R), tile);
        }

        public bool UpdateTile(Tile tile)
        {
            var result = _dict.TryGetValue((q: tile.Hex.Q, r: tile.Hex.R), out var value);
            
            if (result)
            {
                _dict[(q: tile.Hex.Q, r: tile.Hex.R)] = tile;
            }

            return result;
        }

        public Tile Pop((int q, int r) key)
        {
            var result = _dict.TryGetValue(key, out var value);

            if (result)
            {
                _dict.Remove(key);
            }

            return result ? value : null;
        }

        public bool Remove((int q, int r) key)
        {
            return _dict.Remove(key);
        }

        public void Clear()
        {
            _dict.Clear();
        }
    }
}