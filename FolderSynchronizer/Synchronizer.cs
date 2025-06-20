﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class Synchronizer
    {
        private readonly string sourcePath;
        private readonly string replicaPath;
        private readonly Logger logger;
        private readonly IFileComparer fileComparer;

        public Synchronizer(string sourcePath, string replicaPath, Logger logger, IFileComparer fileComparer)
        {
            this.sourcePath = sourcePath;
            this.replicaPath = replicaPath;
            this.logger = logger;
            this.fileComparer = fileComparer;
        }

        public void Synchronize() 
        {
            CreateDirectory(sourcePath);
            CreateDirectory(replicaPath);
            UpdateFiles();
            DeleteFiles();
            logger.Log("[Info] Folders are same!");
        }

        private void CreateDirectory(string path)
        {
            var folderName = new DirectoryInfo(path).Name;

            var message = Directory.Exists(path) ?
                $"Folder {folderName} exists: {path}" :
                $"Created folder: {Directory.CreateDirectory(path).FullName}";

            logger.Log($"[Task] {message}");
        }

        private void UpdateFiles()
        {
            foreach(var sourceFile in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(sourcePath, sourceFile);
                var replicaFile = Path.Combine(replicaPath, relativePath);

                var targetDir = Path.GetDirectoryName(replicaFile);
                
                if (!string.IsNullOrEmpty(targetDir) && !Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                    logger.Log($"[Task] Created directory: {targetDir} in {replicaPath}");
                }

                if (!fileComparer.AreEqual(sourceFile, replicaFile))
                {
                    File.Copy(sourceFile, replicaFile, true);
                    logger.Log($"[Task] Copied/updated: {relativePath} into {replicaPath}");
                }
            }
        }

        private void DeleteFiles()
        {
            foreach (var replicaFile in Directory.GetFiles(replicaPath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(replicaPath, replicaFile);
                var sourceFile = Path.Combine(sourcePath, relativePath);

                if (!File.Exists(sourceFile))
                {
                    File.Delete(replicaFile);
                    logger.Log($"[Task] Deleted: {relativePath}");
                }
            }

            var replicaDirs = Directory.GetDirectories(replicaPath, "*", SearchOption.AllDirectories).OrderByDescending(d => d.Length);

            foreach (var dir in replicaDirs)
            {
                var relativePath = Path.GetRelativePath(replicaPath, dir);
                var sourceDir = Path.Combine(sourcePath, relativePath);

                if (!Directory.Exists(sourceDir) && Directory.Exists(dir) && !Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    Directory.Delete(dir);
                    logger.Log($"[Task] Deleted directory: {relativePath}");
                }
            }
        }
    }
}
