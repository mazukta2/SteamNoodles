using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameUnity.Assets.Scripts.Unity.Views.Common.Materials
{
    public class MaterialCopy : MonoBehaviour
    {
        [SerializeField] MeshRenderer _mesh;

        protected void Awake()
        {
            var materials = _mesh.materials;
            var copies = new List<Material>();
            foreach (var item in materials)
            {
                copies.Add(new Material(item));
            }

            _mesh.materials = copies.ToArray();
        }
    }

}
