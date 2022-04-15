using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine.Helpers
{
    public static class TransformExtentions
    {
        public static void DestroyChildren(this Transform transform, bool onlyActive = false)
        {
            foreach (Transform child in transform)
                if (!onlyActive || (onlyActive && child.gameObject.activeSelf))
                    DestroyImpl(child.gameObject);
        }

        public static Transform GetParentWithTag(this Transform transform, string tag)
        {
            while (transform != null)
            {
                if (transform.CompareTag(tag))
                {
                    return transform;
                }
                transform = transform.parent;
            }
            return null; // Could not find a parent with given tag.
        }

        public static void PrepareContainer(this RectTransform container, params Component[] prefabs)
        {
            foreach (var prefab in prefabs)
            {
                prefab.gameObject.SetActive(false);
            }

            container.DestroyChildren(true);
        }

        private static void DestroyImpl(GameObject gameObject)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }
    }
}
