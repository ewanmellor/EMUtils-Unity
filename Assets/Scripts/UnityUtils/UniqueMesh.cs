using UnityEngine;


namespace EMUtils.UnityUtils
{
    public class UniqueMesh : MonoBehaviour
    {
        [HideInInspector]
        int MeshOwnerID;

        MeshFilter _meshFilter;

        MeshFilter MeshFilter
        {
            get
            {
                if (_meshFilter == null)
                {
                    _meshFilter = GetComponent<MeshFilter>();
                    if (_meshFilter == null)
                    {
                        _meshFilter = gameObject.AddComponent<MeshFilter>();
                    }
                }
                return _meshFilter;
            }
        }

        Mesh _mesh;

        protected Mesh Mesh
        {
            get
            {
                var ownerId = gameObject.GetInstanceID();
                if (MeshFilter.sharedMesh == null || MeshOwnerID != ownerId)
                {
                    var m = new Mesh();
                    m.name = "Mesh@" + ownerId;

                    MeshOwnerID = ownerId;
                    MeshFilter.sharedMesh = m;
                    _mesh = m;
                }
                return _mesh;
            }
        }
    }
}
