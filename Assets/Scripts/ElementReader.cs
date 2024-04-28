using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ElementReader : MonoBehaviour
{
    [SerializeField] private Image _baseImage;
    [SerializeField] private Image _sp;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private Color _selectColor;

    private Elements.Elements _element;

    public enum ButtonState { WAITING_INPUT, SELECTION }
    [SerializeField] private ButtonState _state;

    public delegate void DelegateOnChangeInternalState(ElementReader current);
    public DelegateOnChangeInternalState OnButtonChangeState; 

    public delegate void DelegateElement(Enums.BaseState state);
    public static DelegateElement OnSelectToChangeState;

    public void SetUp(Elements.Elements e)
    {
        _element = e;

        _sp.sprite = _element.GetSprite();
        _name.text = _element.GetName();
    }

    public void PressButton()
    {
        ButtonSetState(); 
    }

    private void ButtonSetState()
    {  
        switch (_state)
        {
            //button was waiting for player interaction
            //then sets to selection visual feedback
            case ButtonState.WAITING_INPUT:
                _baseImage.color = _selectColor;
                _state = ButtonState.SELECTION;

                var e = Instantiate(_element.GetPrefab(), Camera.main.transform.position + new Vector3(0,0, 30), Quaternion.identity);
                e.OnSelect();

                OnSelectToChangeState?.Invoke(Enums.BaseState.SELECTION_ELEMENT);

                ResetButton(); 

                break; 
        }
    }
    public void ResetButton()
    {
        _state = ButtonState.WAITING_INPUT;
        _baseImage.color = Color.white;
    }
}
