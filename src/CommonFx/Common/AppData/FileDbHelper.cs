﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonFx.Common.AppData
{
    /// <summary>
    /// 从文件数据源保存和加载数据的帮助类（默认Json）
    /// </summary>
    public interface IFileDbHelper
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        IList<T> Read<T>(string path);
        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="list"></param>
        void Save<T>(string path, IList<T> list);
    }

    /// <summary>
    /// 默认的Json实现
    /// </summary>
    public class FileDbHelper : IFileDbHelper
    {
        #region for di extensions

        private static Func<IFileDbHelper> _resolve = () => ResolveAsSingleton.Resolve<FileDbHelper, IFileDbHelper>();
        public static Func<IFileDbHelper> Resolve
        {
            get { return _resolve; }
            set { _resolve = value; }
        }

        #endregion
        
        public IList<T> Read<T>(string path)
        {
            string jsonValue = ReadFile(path);
            if (string.IsNullOrWhiteSpace(jsonValue))
            {
                return new List<T>();
            }
            var instance = JsonHelper.Deserialize<List<T>>(jsonValue);
            return instance;
        }

        public void Save<T>(string path, IList<T> list)
        {
            IList<T> listFix = new List<T>();
            if (list != null)
            {
                listFix = list;
            }

            var jsonValue = JsonHelper.Serialize(listFix);
            SaveFile(path, jsonValue);
        }

        //helpers
        private string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            var content = File.ReadAllText(filePath);
            return content;
        }
        private void SaveFile(string filePath, string content)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            string dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
            {
                if (string.IsNullOrWhiteSpace(dirPath))
                {
                    throw new ArgumentException("非法的路径:" + filePath);
                }
                Directory.CreateDirectory(dirPath);
            }

            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
    }
}
