// Copyright (c) 2017 Doozy Entertainment / Marlink Trading SRL and Ez Entertainment / Ez Entertainment SRL. All Rights Reserved.
// This code is a collaboration between Doozy Entertainment and Ez Entertainment and is not to be used in any other assets other then the ones created by their respective companies.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickEngine.IO
{
    public static class File
    {
        /// <summary>
        /// Returns true if the file exists at the specified path.
        /// </summary>
        public static bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }

        /// <summary>
        /// Creates a Directory at the specified path.
        /// </summary>
        public static void CreateDirectory(string path)
        {
            FileInfo file = new FileInfo(path);
            file.Directory.Create();
        }

        /// <summary>
        /// Searches for the directoryName in all the project's directories and returns the absolute path of the first one it encounters.
        /// </summary>
        public static string GetAbsoluteDirectoryPath(string directoryName, bool debug = false)
        {
            string[] directoryPath = Directory.GetDirectories(Application.dataPath, directoryName, SearchOption.AllDirectories);
            if (directoryPath == null)
            {
                if (debug) { Debug.LogError("[QuickEngine.IO] You searched for the [" + directoryName + "] folder, but no folder with that name exists in the current project."); }
                return "ERROR";
            }
            else if (directoryPath.Length > 1)
            {
                if (debug) { Debug.LogWarning("[QuickEngine.IO] You searched for the [" + directoryName + "] folder. There are " + directoryPath.Length + " folders with that name. Returned the folder location for the first one, but it might not be the one you're looking for. Give the folder you are looking for an unique name to avoid any issues."); }
            }
            return directoryPath[0];
        }

        /// <summary>
        /// Searches for the directoryName in all the project's directories and returns the relative path of the first one it encounters.
        /// </summary>
        public static string GetRelativeDirectoryPath(string directoryName)
        {
            string directoryPath = GetAbsoluteDirectoryPath(directoryName);
            directoryPath = directoryPath.Replace(Application.dataPath, "Assets");
            return directoryPath;
        }

        public static void WriteFile<T>(string filePath, T obj, Action<FileStream, T> serializeMethod)
        {
            CreateDirectory(filePath);
            FileStream stream = new FileStream(filePath, FileMode.Create);
            serializeMethod(stream, obj);
            stream.Close();
        }

        public static void Delete(string path)
        {
            System.IO.File.Delete(path);
        }

        public static void Move(string sourceFileName, string destFileName)
        {
            System.IO.File.Move(sourceFileName, destFileName);
        }

        public static void Rename(string sourceFileName, string destFileName)
        {
            System.IO.File.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// Returns a FileInfo array of all the files found at the specified path.
        /// </summary>
        public static FileInfo[] GetFiles(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            return directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Returns a FileInfo array of all the files, with the given fileExtension, found at the specified path.
        /// </summary>
        public static FileInfo[] GetFiles(string directoryPath, string fileExtension)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            return directoryInfo.GetFiles("*." + fileExtension, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Returns a string array of all the filenames found at the specified path.
        /// </summary>
        public static string[] GetFilesNames(string directoryPath)
        {
            List<string> list = new List<string>();
            FileInfo[] fileInfo = GetFiles(directoryPath);
            if (fileInfo != null)
            {
                for (int i = 0; i < fileInfo.Length; i++) { list.Add(fileInfo[i].Name.Replace(fileInfo[i].Extension, "")); }
                list.Sort();
            }
            return list.ToArray();
        }

        /// <summary>
        /// Returns a string array of all the filenames, of the files with the given fileExtension, found at the specified path.
        /// </summary>
        public static string[] GetFilesNames(string directoryPath, string fileExtension)
        {
            List<string> list = new List<string>();
            FileInfo[] fileInfo = GetFiles(directoryPath, fileExtension);
            if (fileInfo != null)
            {
                for (int i = 0; i < fileInfo.Length; i++) { list.Add(fileInfo[i].Name.Replace(fileInfo[i].Extension, "")); }
                list.Sort();
            }
            return list.ToArray();
        }

        /// <summary>
        /// Returns a DirectoryInfo array of all the directories (subfolders) found at the specified path.
        /// </summary>
        public static DirectoryInfo[] GetDirectories(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
            return directoryInfoArray;
        }

        /// <summary>
        /// Returns a string array of all the directories names (subfolders) found at the specified path.
        /// </summary>
        public static string[] GetDirectoriesNames(string directoryPath)
        {
            List<string> list = new List<string>();
            DirectoryInfo[] directoryInfo = GetDirectories(directoryPath);
            if (directoryInfo != null)
            {
                for (int i = 0; i < directoryInfo.Length; i++) { list.Add(directoryInfo[i].Name); }
                list.Sort();
            }
            return list.ToArray();
        }
    }
}
