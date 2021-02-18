using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    ///AI配置文件读取器工厂
    ///</summary>
    public class AIConfigReaderFactory
    {
        private static Dictionary<string, AIConfigReader> cache;

        static AIConfigReaderFactory()
        {
            cache = new Dictionary<string, AIConfigReader>();
        }

        public static Dictionary<string, Dictionary<string,string>> GetMap(string fileName)
        {
            if (!cache.ContainsKey(fileName))
                cache.Add(fileName, new AIConfigReader(fileName));
            return cache[fileName].map;
        }
    }
}
