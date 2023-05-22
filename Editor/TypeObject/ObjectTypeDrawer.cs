using UnityEditor;

namespace BuhuBuhu.Pooler.Editor
{
    [CustomPropertyDrawer(typeof(ObjectType))]
    public class ObjectTypeDrawer : BaseTypeDrawer<ObjectType, IObjectTypeList>
    {
    }
}
