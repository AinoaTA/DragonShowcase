using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "Element", menuName = "SO/Elements")]
    public class Elements : ScriptableObject
    {
        public string GetName() => _name;
        public ARElement GetPrefab() => _prefab;

        public Sprite GetSprite() => _sp;

        [SerializeField] private string _name;
        [SerializeField] private ARElement _prefab;
        [SerializeField] private Sprite _sp;
    }
}