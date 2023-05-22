using UnityEngine;

namespace BuhuBuhu.Pooler.Sample
{
    public class ObjectPoolableExample : Poolable
    {
        private Rigidbody _rb;
        private ObjectPoolerExample _objectPooler;

        protected override void OnInit()
        {
            _objectPooler = _parent.GetComponent<ObjectPoolerExample>();
            _rb = GetComponent<Rigidbody>();
        }

        protected override void OnGet()
        {
            _rb.AddForce(Vector3.one, ForceMode.Impulse);
        }

        protected override void OnSet()
        {
            _rb.velocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _objectPooler.Set(this);
        }
    }
}

