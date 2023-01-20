using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Hex
{
    public struct Face
    {
        public List<Vector3> vertices { get; private set; }
        public List<int> triangles { get; private set; }
        public List<Vector2> uvs { get; private set; }

        public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            this.vertices = vertices;
            this.triangles = triangles;
            this.uvs = uvs;
        }
    }
    
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class HexRenderer : MonoBehaviour
    {
        public Material Material => material;
        public bool IsFlatTopped => isFlatTopped;
        public float InnerSize => innerSize;
        public float OuterSize => outerSize;
        public float Height => height;
        
        [SerializeField] private Material material;
        [SerializeField] private bool isFlatTopped;
        [SerializeField] private float innerSize;
        [SerializeField] private float outerSize;
        [SerializeField] private float height;

        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private List<Face> _faces;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _mesh = new Mesh();
            _mesh.name = "Hex";

            _meshFilter.mesh = _mesh;
            _meshRenderer.material = material;
        }

        private void OnEnable()
        {
            DrawMesh();
        }

        public void DrawMesh()
        {
            DrawFaces();
            CombineFaces();
        }

        private void DrawFaces()
        {
            _faces = new List<Face>();

            // Top faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
            }
            
            // Bottom faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
            }
            
            // Outer faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
            }
            
            // Inner faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point));
            }
        }

        private Face CreateFace(float innerRadius, float outerRadius, float heightA, float heightB, int point,
            bool reverse = false)
        {
            var pointA = GetPoint(innerRadius, heightB, point);
            var pointB = GetPoint(innerRadius, heightB, (point < 5) ? point + 1 : 0);
            var pointC = GetPoint(outerRadius, heightA, (point < 5) ? point + 1 : 0);
            var pointD = GetPoint(outerRadius, heightA, point);

            var vertices = new List<Vector3> { pointA, pointB, pointC, pointD };
            var triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
            var uvs = new List<Vector2>()
            {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
            };

            if (reverse)
            {
                vertices.Reverse();
            }

            return new Face(vertices, triangles, uvs);
        }

        private Vector3 GetPoint(float size, float height, int index)
        {
            var angleDeg = isFlatTopped ? 60 * index : 60 * index - 30;
            var angleRad = Mathf.PI / 180f * angleDeg;

            return new Vector3(size * Mathf.Cos(angleRad), height, size * Mathf.Sin(angleRad));
        }

        private void CombineFaces()
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            for (var i = 0; i < _faces.Count; i++)
            {
                vertices.AddRange(_faces[i].vertices);
                uvs.AddRange(_faces[i].uvs);
                
                // Offset the triangles
                var offset = 4 * i;

                foreach (var triangle in _faces[i].triangles)
                {
                    triangles.Add(triangle + offset);
                }
            }

            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = triangles.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }
    }

}