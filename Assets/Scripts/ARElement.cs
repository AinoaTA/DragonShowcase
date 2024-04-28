using UnityEngine;

namespace Elements
{
    public class ARElement : MonoBehaviour, IDelete, IMove, IScale, IRotate
    {
        public OptionsListener Listener => _listener;
        private OptionsListener _listener = new();

        private bool _pressed;
        private Vector2 _firstPoint;

        [SerializeField] private bool _enableInteraction;
        public Collider Collider => _col;
        private Collider _col;

        private void OnEnable()
        {
            RaycastDetectionController.OnPosDetected += Move;
        }

        private void OnDisable()
        {
            RaycastDetectionController.OnPosDetected -= Move;
        }

        private void Start()
        {
            TryGetComponent(out _col);
            AddListener();
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
        }

        public void OnSelect()
        {
            GameManager.Instance.CurrentElementSelected = this;
            _enableInteraction = true;
        }

        public void OnDeselect()
        {
            _enableInteraction = false;
        }

        private void OnMouseDown()
        {
            if (!_enableInteraction) return;

            _pressed = true;
            _firstPoint = Input.mousePosition;
        }

        private void OnMouseUp()
        {
            _pressed = false;
        } 

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
                //if (Input.touchCount == 2) 
                //{
                //    var t1 = Input.touches[0];
                //    var t2 = Input.touches[1];

                //    var newDistance = Vector2.Distance(t1.position, t2.position);

                //    if (_prevDistance < newDistance) 
                //    {
                    
                //    }

                //}
            }
        }

        public void Rotate()
        {
            if (_pressed)
            {
                float dirX = (Input.mousePosition.x - _firstPoint.x) * Time.deltaTime;
                transform.Rotate(Vector3.up, -dirX);
            }
        }
    }
}