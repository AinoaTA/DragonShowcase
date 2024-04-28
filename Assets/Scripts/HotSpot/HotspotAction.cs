using UnityEngine;

namespace Hotspots
{
    public class HotspotAction : ScriptableObject
    {
        public string TextDescription => _text;
        [TextArea]
        [SerializeField] private string _text;
        public virtual void Init(Transform target) { }
        public virtual void Play() { }

        public virtual void Stop() { }

    }
}