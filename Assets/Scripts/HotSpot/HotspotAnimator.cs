
using UnityEngine;
namespace Hotspots
{
    [CreateAssetMenu(fileName = "POI_Animator", menuName = "POI/Animator")]
    public class HotpotAnimator : HotspotAction
    {
        private Animator _animator;
        [SerializeField] private string _specificAnimToPlay;

        public override void Init(Transform target)
        {
            try
            {
                if (_animator == null)
                {
                    Debug.Log($"Animator value is {target} ");
                    _animator = target.GetComponent<Animator>();
                }
            }
            catch (System.Exception)
            {                              //for highlight when selected in console!
                Debug.Log($" This target {target.gameObject} doesn't have an animator component!! ");
                throw;
            }
        }

        public override void Play()
        {
            if (_animator == null)
            {
                Debug.Log("An Animator was not referenced");
                return;
            }

            _animator.Play(_specificAnimToPlay);
        }

        public override void Stop()
        {

        }
    }
}