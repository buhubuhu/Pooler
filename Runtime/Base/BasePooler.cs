using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuhuBuhu.Pooler
{
    public struct PoolableData
    {
        public bool LimitMaxInstances;
        public int MaxInstances;
        public Stack<Poolable> Poolables;
    }

    public abstract class BasePooler<T> where T : IBaseType
    {
        private bool _initialized = false;
        private GameObject _poolerHandler;
        private Dictionary<string, Poolable> _library;
        private Dictionary<string, PoolableData> _cached;
        private Dictionary<string, Dictionary<int, Poolable>> _pooled;

        public bool Initialized => _initialized;

        public void Init(GameObject poolerHandler, BasePoolDatabase<T> database)
        {
            _poolerHandler = poolerHandler;
            _cached = new Dictionary<string, PoolableData>();
            _pooled = new Dictionary<string, Dictionary<int, Poolable>>();
            _library = new Dictionary<string, Poolable>();

            foreach (var pool in database.Pools)
            {
                var dataCache = new PoolableData
                {
                    LimitMaxInstances = pool.LimitMaxInstances,
                    MaxInstances = pool.MaxInstances,
                    Poolables = new Stack<Poolable>()
                };

                _library.Add(pool.Tag.Name, pool.Prefab);
                _cached.Add(pool.Tag.Name, dataCache);
                _pooled.Add(pool.Tag.Name, new Dictionary<int, Poolable>());
            }

            _initialized = true;
        }

        public Poolable Get(T tag, Vector3 position = default, Quaternion rotation = default, Transform parent = null) 
        {
            if (!_initialized)
            {
                throw new System.Exception($"Pooler is not initialized");
            }
            if (_cached.TryGetValue(tag.Name, out var poolables))
            {
                if (poolables.Poolables.Count == 0)
                {
                    if (poolables.LimitMaxInstances)
                    {
                        if (_pooled[tag.Name].Values.Count >= poolables.MaxInstances)
                        {
                            Debug.LogWarning($"Max instances of {tag.Name} is reached");
                            return null;
                        }
                    }

                    SpawnToStack(tag, poolables.Poolables);
                }

                var objectToSpawn = poolables.Poolables.Pop();
                objectToSpawn.Get(position, rotation, parent);
                _pooled[tag.Name].Add(objectToSpawn.ID, objectToSpawn);
                return objectToSpawn;
            }
            else
            {
                Debug.LogError($"Pool with tag {tag} doesn't exist in database");
                return null;
            }
        }

        public void Set(Poolable poolable)
        {
            if (!_initialized)
            {
                throw new System.Exception($"Pooler is not initialized");
            }
            if (_pooled.TryGetValue(poolable.Tag, out var pooled))
            {
                if (_cached.TryGetValue(poolable.Tag, out var poolables))
                {
                    poolable.Set();
                    pooled.Remove(poolable.ID);
                    poolables.Poolables.Push(poolable);
                }
            }
        }

        public void Clear()
        {
            _initialized = false;

            foreach (var p in _cached.Values)
            {
                var l = p.Poolables.ToList();

                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i] && l[i].gameObject)
                        Object.Destroy(l[i].gameObject);
                }

                p.Poolables.Clear();
            }
            _cached.Clear();

            var k = _pooled.Values.ToList();
            foreach (var p in k)
            {
                foreach (var i in p.Values)
                {
                    if (i && i.gameObject)
                        Object.Destroy(i.gameObject);
                }
            }
            _pooled.Clear();
        }

        private void SpawnToStack(T tag, Stack<Poolable> poolList)
        {
            var obj = Object.Instantiate(_library[tag.Name], _poolerHandler.transform);
            poolList.Push(obj);
            obj.Init(tag.Name, obj.GetHashCode(), _poolerHandler.transform);
        }
    }
}


