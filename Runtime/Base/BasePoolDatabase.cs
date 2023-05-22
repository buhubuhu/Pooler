using System.Collections.Generic;
using UnityEngine;

namespace BuhuBuhu.Pooler
{
    [System.Serializable]
    public class Pool<T> where T : IBaseType
    {
        public T Tag;
        public Poolable Prefab;
        public bool LimitMaxInstances;
        public int MaxInstances;
    }

    public class BasePoolDatabase<T> : ScriptableObject where T : IBaseType
    {
        [SerializeField] private List<Pool<T>> _pools;

        public List<Pool<T>> Pools => _pools;
    }
}


