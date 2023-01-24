using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Hex.Core
{
    public class Map
    {
        private Dictionary<(int q, int r), Hex> _dict;

        public List<Hex> GetAll()
        {
            return _dict.Values.ToList();
        }

        public Hex Get((int q, int r) key)
        {
            return _dict.TryGetValue(key, out var value) ? value : null;
        }

        public bool Insert(Hex hex)
        {
            return _dict.TryAdd((q: hex.Q, r: hex.R), hex);
        }

        public bool Update(Hex hex)
        {
            var result = _dict.TryGetValue((q: hex.Q, r: hex.R), out var value);
            
            if (result)
            {
                _dict[(q: hex.Q, r: hex.R)] = hex;
            }

            return result;
        }

        public Hex Pop((int q, int r) key)
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