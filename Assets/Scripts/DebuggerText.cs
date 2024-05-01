using UnityEngine;
/// <summary>
/// this script was used for debug on build because my usb cable didn't work.
/// </summary>
public class DebuggerText : MonoBehaviour
{
    // Start is called before the first frame update
    private static TMPro.TMP_Text _text;
    //private void Awake()
    //{
    //    TryGetComponent(out _text);
    //}
    public static void SendDebugg(string t)
    {
        //_text.text = t;
        _text.text = "";
    }
}
