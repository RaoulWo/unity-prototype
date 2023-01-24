using UnityEngine;

namespace _Scripts.Hex
{
    public class Tile : MonoBehaviour
    {
        public Core.Hex Hex;
        public Material gray;
        public Material yellow;

        private bool _isInitialized;

        public void Init(Vector3Int v)
        {
            if (_isInitialized) return;

            Hex = new Core.Hex(v);
            
            _isInitialized = true;
        }
        
        public void Init(int q, int r)
        {
            if (_isInitialized) return;

            Hex = new Core.Hex(q, r);
            
            _isInitialized = true;
        }
        
        public void Init(int q, int r, int s)
        {
            if (_isInitialized) return;

            Hex = new Core.Hex(q, r, s);
            
            _isInitialized = true;
        }
    }
}