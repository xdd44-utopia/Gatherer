using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    ///<summary>
    ///配置文件读取器
    ///</summary>
    public class ConfigurationReader
    {
        public static string GetConfigFile(string fileName)
        {
            string url;

            #region 分平台判断StreamingAssets路径
            //ConfigMap.txt
            //string url = "file://" + Application.streamingAssetsPath + "/ConfigMap.txt";

            //如果在编译器下或者PC端……
            //if(Application.platform==RuntimePlatform.WindowsEditor)
            //unity宏标签
#if UNITY_EDITOR || UNITY_STANDALONE
            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
            //否则如果在iPhone下……
#elif UNITY_IPHONE
    url = "file://" + Application.dataPath + "/Raw/" + fileName;
            //否则如果在安卓下……
#elif UNITY_ANDROID
    url = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#endif
            #endregion

            //WWW www = new WWW(url);
            //while (true)
            //{
            //    if (www.isDone)
            //        return www.text;
            //}
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();
            while (true)
            {
                if (www.downloadHandler.isDone)
                    return www.downloadHandler.text;
            }
        }

        public static void Reader(string fileContent, Action<string> handler)
        {
            //文件名=路径\r\n文件名=路径
            //StringReader字符串读取器，提供了逐行读取字符串的功能
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                //读一行，满足条件则解析
                while ((line = reader.ReadLine()) != null)
                {
                    //解析行数据
                    handler(line);
                }

                //1先读一行
                //string line = reader.ReadLine();
                //2不为空则解析 4再判断条件
                //while (line != null)
                //{
                //    string[] keyValue = line.Split('=');
                //    //keyValue[0]文件名 keyValue[1]路径
                //    configMap.Add(keyValue[0], keyValue[1]);
                //3再读一行
                //    line = reader.ReadLine();
                //}
            }
            //当程序推出using代码块，将自动调用reader.Dispose();方法
        }
    }
}
