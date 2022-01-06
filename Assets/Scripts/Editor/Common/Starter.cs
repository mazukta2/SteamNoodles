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
    [InitializeOnLoad]
    public static class MainStarter
    {
        static MainStarter()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnPlayModeChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredEditMode)
            {
                OpenSavedScenes();
            }

            if (!_startedWithRun)
                return;

            if (obj == PlayModeStateChange.ExitingEditMode)
            {
                SaveOpenedScenes();
                OpenMainScene();
            }

            _startedWithRun = false;
        }

        private static bool _startedWithRun;

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent(!EditorApplication.isPlaying ? "START" : "STOP",
                "Start With Main Scene"), ToolbarStyles.commandButtonStyle))
            {
                if (EditorApplication.isPlaying)
                {
                    Stop();
                }
                else
                {
                    Start();
                }
            }
        }

        public static void Start()
        {
            if (!EditorApplication.isPlaying)
            {
                _startedWithRun = true;
                EditorApplication.isPlaying = true;
            }
        }

        public static void Stop()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
        }

        private static void OpenMainScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity");
        }

        private static void SaveOpenedScenes()
        {
            var openedScenes = new List<string>();
            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                var scene = EditorSceneManager.GetSceneAt(i);
                openedScenes.Add(scene.path);
            }
            EditorPrefs.SetString("LastOpenedScenes", string.Join(",", openedScenes));

            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorPrefs.SetString("LastOpenedPrefab", prefabStage.assetPath);
            }
        }

        private static void OpenSavedScenes()
        {
            var strs = EditorPrefs.GetString("LastOpenedScenes", "");
            var openedScenes = strs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < openedScenes.Length; i++)
            {

                EditorSceneManager.OpenScene(openedScenes[i], i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive);
            }

            EditorPrefs.SetString("LastOpenedScenes", "");

            var prefabs = EditorPrefs.GetString("LastOpenedPrefab", "");
            var openedPrefabs = prefabs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string openedPrefab in openedPrefabs)
            {
                var mainAsset = AssetDatabase.LoadMainAssetAtPath(openedPrefab);
                AssetDatabase.OpenAsset(mainAsset);
            }

            EditorPrefs.SetString("LastOpenedPrefab", "");
        }
    }
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 15,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Normal,
                fixedWidth = 100,
            };
        }
    }
}