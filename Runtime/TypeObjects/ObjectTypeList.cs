namespace BuhuBuhu.Pooler
{
    public interface IObjectTypeList : IBaseTypeList
    {
    }

    public class ObjectTypeList : IObjectTypeList
    {
        public static readonly ObjectType None = new ObjectType("None");
    }
}
