using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuhuBuhu.Pooler
{
    [Serializable]
    public struct ObjectType : IBaseType
    {
        [SerializeField] private string _name;

        public ObjectType(string name)
        {
            _name = name;
        }

        public string Name => _name;
        public bool IsUndefined => string.IsNullOrEmpty(_name);

        public override string ToString()
        {
            return _name;
        }

        public static bool operator ==(ObjectType b1, ObjectType b2)
        {
            return b1.Equals(b2);
        }

        public static bool operator !=(ObjectType b1, ObjectType b2)
        {
            return !(b1 == b2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var b2 = (ObjectType)obj;
            return Name == b2.Name;
        }

        public override int GetHashCode()
        {
            return -1125283371 + EqualityComparer<string>.Default.GetHashCode(_name);
        }
    }
}
