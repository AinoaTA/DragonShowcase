using UnityEngine;

namespace Hotspots
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Transform _triggerElement;
        [SerializeField] private Transform _target;
        [SerializeField] private HotspotAction _pointer;

        [Header("Show Line")]
        [SerializeField] private bool _showLine;
        [SerializeField] private Transform _initLine;
        [SerializeField] private Transform _endLine;
        [SerializeField] private LineRenderer _lineRenderer;

        [Header("Show Box")]
        [SerializeField] private GameObject _showBox;
        [SerializeField] private float _minDistanceToShow;
        [SerializeField] private TMPro.TMP_Text _text;

        private void OnEnable()
        {
            DetectMove.OnMove += CanShow;
            Elements.ARElement.OnUpdate += CanShow;
        }

        private void OnDisable()
        {
            DetectMove.OnMove -= CanShow;
            Elements.ARElement.OnUpdate -= CanShow;
        }

        private void Awake()
        {
            Show(false);
        }

        private void Start()
        {
            if (_triggerElement == null)
                _triggerElement = Camera.main.transform;
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

        private void CanShow()
        { 
            if (Vector3.Distance(transform.position, _triggerElement.position) < _minDistanceToShow)
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
            _lineRenderer.gameObject.SetActive(enabled);
        }

        public void StartPointer()
        {
            _pointer.Play();
        }
        private void Update()
        {
            if (_showLine)
            { 
                _lineRenderer.SetPosition(0, _initLine.position);
                _lineRenderer.SetPosition(1, _endLine.position);
            }
        }
    }
}