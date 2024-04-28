using UnityEngine;

public class DetectMove : MonoBehaviour
{
    private Vector3 _position;

    public delegate void DelegateOnMoveUpdate();
    public static DelegateOnMoveUpdate OnMove;
    private void Start()
    {
        _position = transform.position;
    }

    private void Update()
    {
        if (_position != transform.position) 
        {
            _position = transform.position;
            OnMove?.Invoke();
        }
    }
}
