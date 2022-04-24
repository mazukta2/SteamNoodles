using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;


namespace Assets.Scripts.Editor.Common
{
    public static class Nesting
    {
        private static GameObject _base;

        [MenuItem("Assets/Prefab Nesting/Set As Base")]
        private static void SetAsBase()
        {
            _base = (GameObject)Selection.activeObject;
        }
        
        [MenuItem("Assets/Prefab Nesting/Set As Base", true)]
        private static bool SetAsBaseValidation()
        {
            return Selection.activeObject is GameObject;
        }

        [MenuItem("Assets/Prefab Nesting/Set As Child", true)]
        private static bool SetAsChildValidation()
        {
            if (_base == null)
                return false;

            foreach (var item in Selection.objects)
            {
                if (item is not GameObject)
                    return false;
            }

            return true;
        }

        [MenuItem("Assets/Prefab Nesting/Set As Child")]
        private static void SetAsChild()
        {
            foreach (var child in Selection.gameObjects)
            {
                var childPath = AssetDatabase.GetAssetPath(child.GetInstanceID());

                // future variant
                var tempVariantInstance = PrefabUtility.InstantiatePrefab(_base) as GameObject;
                // instance of origin prefab
                var tempChildCopyInstance = PrefabUtility.InstantiatePrefab(child) as GameObject;
                PrefabUtility.UnpackPrefabInstance(tempChildCopyInstance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                // move all children from origin to variant
                foreach (Transform item in tempChildCopyInstance.transform)
                    item.parent = tempVariantInstance.transform;

                UnityEditorInternal.ComponentUtility.ReplaceComponentsIfDifferent(tempChildCopyInstance, tempVariantInstance, IsDesiredComponent);
                PrefabUtility.RecordPrefabInstancePropertyModifications(tempChildCopyInstance);

                PrefabUtility.SaveAsPrefabAssetAndConnect(tempVariantInstance, childPath, InteractionMode.AutomatedAction);

                GameObject.DestroyImmediate(tempVariantInstance);
                GameObject.DestroyImmediate(tempChildCopyInstance);

                bool IsDesiredComponent(Component component)
                {
                    return component is not Transform;
                }
            }
        }

    }
}