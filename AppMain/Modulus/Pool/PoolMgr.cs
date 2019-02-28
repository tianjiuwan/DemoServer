/// <summary>
/// 这个命名空间写C#对象的缓存
/// </summary>
namespace Pool
{
    using System;
    using System.Collections.Generic;

    public class PoolMgr : Singleton<PoolMgr>
    {
        private Dictionary<Type, List<PoolObject>> pools;
        protected override void initialize()
        {
            pools = new Dictionary<Type, List<PoolObject>>();
        }

        /// <summary>
        /// 获取一个data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T getData<T>() where T : PoolObject, new()
        {
            Type type = typeof(T);
            if (pools.ContainsKey(type))
            {
                if (pools[type].Count > 0)
                {
                    T data = pools[type][0] as T;
                    pools[type].RemoveAt(0);
                    return data;
                }
            }
            return new T();
        }

        /// <summary>
        /// 回收一个data
        /// </summary>
        /// <param name="data"></param>
        public void recyleData(PoolObject data)
        {
            if (data != null)
            {
                Type type = data.GetType();
                if (!pools.ContainsKey(type))
                {
                    pools.Add(type, new List<PoolObject>());
                }
                if (pools[type].Count < data.maxCount)
                    pools[type].Add(data);
            }
        }
    }
}


