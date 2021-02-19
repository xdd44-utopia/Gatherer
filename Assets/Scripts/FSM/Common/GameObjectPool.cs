using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    /// <summary>
    /// 可重置
    /// </summary>
    public interface IResetable
    {
        void OnReset();
    }

    ///<summary>
    ///对象池
    ///</summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        //对象池
        private Dictionary<string, List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 通过对象池
        /// </summary>
        /// <param name="key">类别</param>
        /// <param name="prefab">需要创建实例的预制件</param>
        /// <param name="pos">位置</param>
        /// <param name="rotate">旋转</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            GameObject go = FindUsableObject(key);

            if (go == null)
            {
                go = AddObject(key, prefab); //创建物体 -> Awake设置目标点 ->
            }

            //使用
            UseObject(pos, rotate, go);//设置位置/旋转
            return go;
        }

        //使用对象
        private static void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
            go.SetActive(true);
            foreach (var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        //添加对象
        private GameObject AddObject(string key, GameObject prefab)
        {
            //创建对象
            GameObject go = Instantiate(prefab);
            //加入池中
            //如果池中没有key 则添加记录
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        //查找指定类别中 可以使用对象
        private GameObject FindUsableObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go">需要被回收的游戏对象</param>
        /// <param name="delay">延迟时间 默认为0</param>
        public void CollectObject(GameObject go, float delay = 0)
        {
            if (delay == 0)
                go.SetActive(false);
            else
                StartCoroutine(CollectObjectDelay(go, delay));
        }

        public IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }

        /// <summary>
        /// 清空某个类别
        /// </summary>
        /// <param name="key"></param>
        public void Clear(string key)
        {
            //for (int i = 0; i < cache[key].Count; i++)
            //{
            //    Destroy(cache[key][i]);
            //}
            for (int i = cache[key].Count - 1; i >= 0; i--)
            {
                Destroy(cache[key][i]);
            }

            cache.Remove(key);
        }

        /// <summary>
        /// 清空全部
        /// </summary>
        public void ClearAll()
        {
            //foreach只读元素
            //遍历字典
            //foreach (var key in cache.Keys)
            //{
            //    Clear(key);//删除字典记录 cache.Remove(key); 第一次遍历字典后把字典key删了 List就没了 第二次就报错
            //}

            //将字典所有键存入List集合中 cache.Keys实现了IEnumberable List第三个重载也是IEnumberable
            List<string> keyList = new List<string>(cache.Keys);
            //遍历List集合 删除字典中的key和keyList无关，字典中的key们已经复制到keyList里了
            foreach (var key in keyList)
            {
                Clear(key);
            }
        }
    }
}
