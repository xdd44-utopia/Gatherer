using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace AI.FSM
{
    ///<summary>
    ///AI配置文件读取器
    ///</summary>
    public class AIConfigReader
    {
        //数据结构
        //大字典：key 状态  value 映射
        //小字典：key 条件编号  value 状态编号
        public Dictionary<string, Dictionary<string, string>> map { get; private set; }

        public AIConfigReader(string fileName)
        {
            map = new Dictionary<string, Dictionary<string, string>>();
            //读取配置文件
            string fileContent = ConfigurationReader.GetConfigFile(fileName);
            //解析配置文件
            ConfigurationReader.Reader(fileContent, BuildMap);
        }

        private string mainKey;

        private void BuildMap(string line)
        {
            //1. 去掉空白（如果空行 则为空字符串）
            line = line.Trim();
            //if (line == "" || line == null) return;
            if (string.IsNullOrEmpty(line)) return;

            if(line.StartsWith("["))//2.状态
            {
                //[Idle] ==> Idle
                mainKey = line.Substring(1, line.Length - 2);
                map.Add(mainKey, new Dictionary<string, string>());
            }
            else //3.映射 NoHealth>Dead
            {
                string[] keyValue = line.Split('>');
                map[mainKey].Add(keyValue[0], keyValue[1]);
            }
        }
    }
}
