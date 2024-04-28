using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void DelegateOnFingerDown();
    public static DelegateOnFingerDown OnFingerDown;
    
    public void FingerDown(InputAction.CallbackContext ctx) 
    {
        Debug.Log("on finer");
        OnFingerDown?.Invoke();
    }
   
}
