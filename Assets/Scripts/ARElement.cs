using UnityEngine;
using UnityEngine.EventSystems;

namespace Elements
{
    public class ARElement : MonoBehaviour, IDelete, IMove, IScale, IRotate, IPointerClickHandler, IPointerUpHandler
    {
        public OptionsListener Listener => _listener;
        private OptionsListener _listener = new();

        private bool _pressed;
        private Vector2 _firstPoint;

        [SerializeField] private bool _enableInteraction;
        public Collider Collider => _col;
        private Collider _col;

        public delegate void DelegateRequiresUpdate();
        public static DelegateRequiresUpdate OnUpdate;

        private void OnEnable()
        {
            RaycastDetectionController.OnPosDetected += Move;
        }

        private void OnDisable()
        {
            RaycastDetectionController.OnPosDetected -= Move;
        }

        private void Awake()
        {
            AddListener();
        }
        private void Start()
        {
            if (_col == null)
                TryGetComponent(out _col);
        }

        public void Move(Vector3 pos)
        {
            if (_enableInteraction)
            {
                if (GameManager.Instance.EqualsSelectState(Enums.SelectionState.MOVING))
                {
                    transform.position = pos;
                    GameManager.Instance.ChangeSelectionState(Enums.SelectionState.NONE);
                }
            }
        }

        public void AddListener()
        {
            _listener.AddDelete(this);
            _listener.AddMove(this);
            _listener.AddRotate(this);
            _listener.AddScale(this);
        }

        public void OnSelect()
        {
            if (_col == null)
                TryGetComponent(out _col);

            GameManager.Instance.ChangeBaseState(Enums.BaseState.SELECTION_ELEMENT);
            GameManager.Instance.CurrentElementSelected = this;
            _enableInteraction = true;
        }

        public void OnDeselect()
        {
            _enableInteraction = false;
            _col.enabled = true;
        }

        //private void OnMouseDown()
        //{
        //    ////if (!_enableInteraction) return;

        //    //OnSelect();

        //    //_pressed = true;
        //    //_firstPoint = Input.mousePosition;
        //}

        //private void OnMouseUp()
        //{
        //    _pressed = false;
        //}

        private void Update()
        {
            if (!_enableInteraction) return;

            switch (GameManager.Instance.SelectionState)
            {
                case Enums.SelectionState.ROTATING:
                    Rotate();
                    break;
                case Enums.SelectionState.SCALING:
                    Scale();
                    break;
            }
        }
        private float _prevDistance = 0;
        public void Scale()
        {
            if (_pressed)
            {
                float dirY = (Input.mousePosition.y - _firstPoint.y) * Time.deltaTime;

                dirY = Mathf.Clamp(dirY, -0.05f, 0.05f);

                Vector3 scaleEnd;

                scaleEnd = transform.localScale;

                scaleEnd += Vector3.one * dirY;

                var v = Mathf.Clamp(scaleEnd.x, 0.5f, 2.0f);

                scaleEnd = Vector3.one * v;

                transform.localScale = scaleEnd;

                OnUpdate?.Invoke();
            }
        }


        public void Rotate()
        {
            if (_pressed)
            {
                float dirX = (Input.mousePosition.x - _firstPoint.x) * Time.deltaTime;

                dirX = Mathf.Clamp(dirX, -1, 1);

                transform.Rotate(Vector3.up, -dirX);

                OnUpdate?.Invoke();
            }
        }

        //onmousedown didn't work. Replaced it by inputs handler 
        public void OnPointerClick(PointerEventData eventData)
        {
            //if (!_enableInteraction) return;
            OnSelect(); 
            _pressed = true;
            _firstPoint = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
        }
    }
}