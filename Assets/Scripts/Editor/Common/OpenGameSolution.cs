using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LocalScripts.CleanFolders
{
    public class OpenGameSolution : EditorWindow
    {
        [MenuItem("Game/Open Game Project")]
        private static void OpenGameProject()
        {
            var directoryInfo = new DirectoryInfo(Application.dataPath);
            var game = directoryInfo.Parent.GetFiles().First(x => x.Name == "Game.sln");
            System.Diagnostics.Process.Start(game.FullName);
        }
    }
}