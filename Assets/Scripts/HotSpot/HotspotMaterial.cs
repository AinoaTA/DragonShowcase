using System.Collections.Generic;
using UnityEngine;

namespace Hotspots
{
    [CreateAssetMenu(fileName = "POI_Material", menuName = "POI/Material")]
    public class HotspotMaterial : HotspotAction
    {
        [SerializeField] private Material[] _material;
        [SerializeField] private int _indexMaterial = 0;
        private SkinnedMeshRenderer _mesh;

        private int _index = 0;
        public override void Init(Transform target)
        {
            try
            {
                if (_mesh == null)
                {
                    Debug.Log($"Animator value is {target} ");
                    _mesh = target.GetComponent<SkinnedMeshRenderer>();
                }
            }
            catch (System.Exception)
            {                              //for highlight when selected in console!
                Debug.Log($" This target {target.gameObject} doesn't have an mesh renderer component!! ");
                throw;
            }
        }

        public override void Play()
        {
            if (_mesh == null)
            {
                Debug.Log("An Skinned Mesh Renderer was not referenced");
                return;
            }

            _index++;
            
            List<Material> m = new();
            _mesh.GetMaterials(m);

            m[_indexMaterial] = _material[_index % _material.Length];
            _mesh.SetMaterials(m);
        }

        public override void Stop()
        {

        }
    }
}