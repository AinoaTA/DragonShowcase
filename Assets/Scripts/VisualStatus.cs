 using UnityEngine;

public class VisualStatus : MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private TMPro.TMP_Text _visualText;

    public void UpdateText(Enums.SelectionState s)
    {
        bool enable = s != Enums.SelectionState.NONE;

        _visual.SetActive(enable);

        if (enable)
            _visualText.text = s.ToString();
    }
}
