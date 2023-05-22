using UnityEngine;

namespace BuhuBuhu.Pooler
{
    public abstract class Poolable : MonoBehaviour
    {
        protected Transform _transform;
        protected GameObject _gameObject;
        protected Transform _parent;
        protected string _tag;
        protected int _id;

        public string Tag => _tag;
        public int ID => _id;

        public void Init(string tag, int id, Transform parent)
        {
            _transform = transform;
            _gameObject = gameObject;
            _parent = parent;
            _gameObject.SetActive(false);
            _tag = tag;
            _id = id;
            OnInit();
        }

        public void Get(Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            _transform.SetPositionAndRotation(position, rotation);
            _transform.parent = parent;
            _gameObject.SetActive(true);
            OnGet();
        }

        public void Set()
        {
            _gameObject.SetActive(false);
            _transform.parent = _parent;
            OnSet();
        }

        protected virtual void OnInit()
        {

        }
        protected virtual void OnGet()
        {

        }
        protected virtual void OnSet()
        {

        }
    }
}


