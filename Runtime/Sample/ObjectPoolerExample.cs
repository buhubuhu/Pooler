using UnityEngine;

namespace BuhuBuhu.Pooler.Sample
{
    public class ObjectPoolerExample : MonoBehaviour
    {
        [SerializeField] private ObjectsDatabase _objectsDatabase;
        private ObjectsPooler _objectsPooler;

        private void Awake()
        {
            _objectsPooler = new ObjectsPooler();
            _objectsPooler.Init(gameObject, _objectsDatabase);
        }

        public Poolable Get(ObjectType tag, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            return _objectsPooler.Get(tag, position, rotation, parent);
        }

        public void Set(Poolable poolable)
        {
            _objectsPooler.Set(poolable);
        }

        private void OnDestroy()
        {
            _objectsPooler.Clear();
        }
    }
}

