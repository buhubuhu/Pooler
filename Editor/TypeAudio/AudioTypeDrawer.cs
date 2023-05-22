using UnityEditor;

namespace BuhuBuhu.Pooler.Editor
{
    [CustomPropertyDrawer(typeof(AudioType))]
    public class AudioTypeDrawer : BaseTypeDrawer<AudioType, IAudioTypeList>
    {
    }
}