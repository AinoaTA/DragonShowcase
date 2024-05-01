using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// Script edited from https://www.youtube.com/watch?v=lYDfV-GaKQA
/// </summary>
[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class RaycastDetectionController : MonoBehaviour
{
    [SerializeField] private float _maxDistanceDetection = 120;
    private ARRaycastManager _ARManager;
    private List<ARRaycastHit> _hits = new();

    public bool InputEnabled { get => _inputEnabled; set => _inputEnabled = value; }

    [SerializeField] private bool _inputEnabled;

    public delegate void DelegatePosDetected(Vector3 pos);
    public static DelegatePosDetected OnPosDetected;

    void Start()
    {
        TryGetComponent(out _ARManager);
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        //#if !UNITY_EDITOR
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        //#endif
    }

    private void OnDisable()
    {
        //#if !UNITY_EDITOR
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        //#endif
    }

    //for test in unity editor without AR foundation.
    //#if UNITY_EDITOR 
    //    public void FingerDown()
    //    {
    //        if (!InputEnabled) return;
    //        Debug.Log("This ray is from Unity Editor");
    //        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
    //        //Debug.Log(ray);
    //        //Debug.DrawRay(ray.origin, ray.direction * _maxDistanceDetection, Color.red, 10);

    //        bool raycasted = Physics.Raycast(ray, out RaycastHit hit, _maxDistanceDetection);

    //        if (raycasted)
    //        {
    //            switch (GameManager.Instance.SelectionState)
    //            {
    //                case Enums.SelectionState.MOVING:

    //                    if (!hit.collider.CompareTag("Element"))
    //                    {
    //                        OnPosDetected?.Invoke(hit.point);
    //                    }
    //                    return;
    //            }

    //            switch (GameManager.Instance.Basestate)
    //            {
    //                case Enums.BaseState.WAITING_INPUT:

    //                    if (hit.collider.CompareTag("Element"))
    //                    {
    //                        ARElement element = hit.collider.GetComponent<ARElement>();

    //                        element.OnSelect();

    //                        GameManager.Instance.ChangeBaseState(Enums.BaseState.SELECTION_ELEMENT);

    //                        return;
    //                    }
    //                    break;
    //                case Enums.BaseState.SELECTION_ELEMENT:
    //                    break;
    //            }
    //        }
    //    }
    //#endif

    //#if !UNITY_EDITOR
    public void FingerDown(Finger obj)
    {
        if (!InputEnabled) return;
        //Debug.Log("raying.. from ar foundation");
        if (obj.index != 0) return;

        //another for detect area plane AR
        _ARManager.Raycast(obj.currentTouch.screenPosition, _hits, TrackableType.PlaneWithinPolygon);


        switch (GameManager.Instance.SelectionState)
        {
            case Enums.SelectionState.MOVING:

                foreach (ARRaycastHit hit in _hits)
                {
                    //DebuggerText.SendDebugg($"hit :{hit}");
                    if (!hit.trackable.CompareTag("Element"))
                    {
                        Pose pose = hit.pose;
                        OnPosDetected?.Invoke(pose.position);
                        //Debug.Log("Hit while moving");
                    }
                }
                return;
        }

    }
    //#endif
    //private void FingerDown(Finger obj)
    //{        
    //    if (!InputEnabled) return;

    //    if (obj.index != 0) return;

    //    if (_ARManager.Raycast(obj.currentTouch.screenPosition, _hits, TrackableType.PlaneWithinPolygon))
    //    {
    //        foreach (ARRaycastHit hit in _hits)
    //        {
    //            Pose pose = hit.pose;
    //            Elements.ARElement element = Instantiate(_elementToPlace, pose.position, pose.rotation);
    //            element.Init();
    //        }
    //    }
    //}

    //#if UNITY_EDITOR
    //    private void Update()
    //    {
    //        if (Input.GetMouseButtonDown(0)) 
    //        { 
    //            //FingerDown();
    //        }
    //    }
    //#endif
}


