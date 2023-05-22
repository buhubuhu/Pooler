namespace BuhuBuhu.Pooler
{
    public interface IAudioTypeList : IBaseTypeList
    {
    }

    public class AudioTypeList : IAudioTypeList
    {
        public static readonly AudioType None = new AudioType("None");
    }
}


