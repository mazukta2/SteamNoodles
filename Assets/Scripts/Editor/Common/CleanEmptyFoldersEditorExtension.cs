using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LocalScripts.CleanFolders
{
    public class CleanEmptyFoldersEditorExtension : EditorWindow
    {
        private static string deleted;

        [MenuItem("Tools/Clean Empty Folders")]
        private static void Cleanup()
        {
            deleted = string.Empty;

            var directoryInfo = new DirectoryInfo(Application.dataPath);


            // Remove emptydirectories of direcotries with meta only
            var checkList = new Queue<DirectoryInfo>(directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories));
            var toDeleteList = new Dictionary<string, DirectoryInfo>();

            while (checkList.Any())
            {
                var directory = checkList.Dequeue();
                
                if (!directory.Exists)
                    continue;

                if (toDeleteList.ContainsKey(directory.FullName))
                    continue;

                // has subdirectories
                if (directory.GetDirectories().Count(x => !toDeleteList.ContainsKey(x.FullName)) > 0)
                    continue;

                // has anything except meta
                if (directory.GetFiles().Any(f => f.Extension != ".meta"))
                    continue;

                if (directory.Parent != null)
                    checkList.Enqueue(directory.Parent);

                toDeleteList.Add(directory.FullName, directory);
            }

            foreach (var item in toDeleteList)
            {
                DeleteFolder(item.Value);
            }

            AssetDatabase.Refresh();
        }

        private static void DeleteFolder(DirectoryInfo directory)
        {
            Debug.Log("Deleting: " + directory.FullName);
            directory.Delete(true);
            var meta = directory.Parent.GetFiles().FirstOrDefault(f => f.FullName == directory.FullName + ".meta");
            meta.Delete();
        }

    }
}