using UnityEngine;

namespace HotPot
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Transform _triggerElement;
        [SerializeField] private Transform _target;
        [SerializeField] private HotPotAction _pointer;


        [Header("Shower Box")]
        [SerializeField] private GameObject _showBox;
        [SerializeField] private float _minDistanceToShow;
        [SerializeField] private TMPro.TMP_Text _text;
        private void OnEnable()
        {
            DetectMove.OnMove += CanShow;
        }

        private void OnDisable()
        {
            DetectMove.OnMove += CanShow;
        }

        private void Awake()
        {
            Show(false);
        }

        private void Start()
        {
            //we want a copy for avoid modifing a generic so! 
            _pointer = Instantiate(_pointer);

            _text.text = _pointer.TextDescription;

            _pointer.Init(_target);
        }

        private void OnMouseDown()
        {
            Debug.Log("on mouse down");
            StartPointer();
        }

        private void CanShow(Vector3 pos)
        {
            Debug.Log("checkin? : " + pos);

            Debug.Log(Vector3.Distance(transform.position, pos));
            if (Vector3.Distance(transform.position, pos) < _minDistanceToShow)
            {
                Show(true);

                Vector3 dirToLook = _triggerElement.position - transform.position;

                dirToLook.y = 0;

                if (dirToLook != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(dirToLook);
                    transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                }
            }
            else
            {
                Show(false);
            }
        }

        private void Show(bool enabled)
        {
            _showBox.SetActive(enabled);
        }

        public void StartPointer()
        {
            _pointer.Play();
        }
    }
}