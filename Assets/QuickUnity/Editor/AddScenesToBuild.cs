﻿/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuickUnityEditor
{
    internal static class AddScenesToBuild
    {
        [MenuItem("Assets/Add Scenes To Build", true)]
        public static bool ValidateScenes()
        {
            Object[] objects = Selection.objects;

            if (objects.Length > 0)
            {
                for (int i = 0, length = objects.Length; i < length; i++)
                {
                    Object obj = objects[i];
                    string path = AssetDatabase.GetAssetOrScenePath(obj);

                    if (Utils.EditorUtil.IsSceneAsset(path))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        [MenuItem("Assets/Add Scenes To Build", false, 500)]
        public static void AddBuildScenes()
        {
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);

            Object[] objects = Selection.objects;

            if (objects.Length > 0)
            {
                for (int i = 0, length = objects.Length; i < length; i++)
                {
                    Object obj = objects[i];
                    string path = AssetDatabase.GetAssetOrScenePath(obj);

                    if (Utils.EditorUtil.IsSceneAsset(path))
                    {
                        if (!scenes.Exists(item => item.path == path))
                        {
                            EditorBuildSettingsScene scene = new EditorBuildSettingsScene();
                            scene.enabled = true;
                            scene.path = path;
                            scenes.Add(scene);
                        }
                    }
                }
            }

            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}