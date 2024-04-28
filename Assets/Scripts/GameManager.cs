using Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public ARElement CurrentElementSelected { get => _current; set => _current = value; }
    [SerializeField] private ARElement _current;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }

    public Enums.BaseState Basestate => _baseState;
    public Enums.SelectionState SelectionState => _selectionState;

    private static GameManager _instance;
    [Header("GAME STATUS")]
    [SerializeField] private Enums.BaseState _baseState;
    [SerializeField] private Enums.SelectionState _selectionState;
    [SerializeField] private VisualStatus _visualStatus;

    [Space(10)]
    [SerializeField] private RaycastDetectionController _placeElements;
    [SerializeField] private ARSession _arSession;

    public delegate void DelegateUpdateSelection();
    public DelegateUpdateSelection OnUpdateSelection;
    public DelegateUpdateSelection OnResetInput;


    private void OnEnable()
    {
        ElementReader.OnSelectToChangeState += ChangeBaseState;
    }

    private void OnDisable()
    {
        ElementReader.OnSelectToChangeState -= ChangeBaseState;
    }

    public void BackMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    private void Start()
    {
        ChangeBaseState(Enums.BaseState.WAITING_INPUT);
        ChangeSelectionState(Enums.SelectionState.NONE);

        //_arSession.Reset(); 
    }

    public bool EqualsBaseState(Enums.BaseState state) => state == _baseState;
    public bool EqualsSelectState(Enums.SelectionState state) => state == _selectionState;

    public void ChangeSelectionStateButton(int i)
    {
        ChangeSelectionState((Enums.SelectionState)i);
    }
    public void ChangeBaseState(Enums.BaseState newstate)
    {
        _baseState = newstate;

        switch (newstate)
        {
            case Enums.BaseState.WAITING_INPUT:

                if (CurrentElementSelected != null)
                    CurrentElementSelected.OnDeselect();

                CurrentElementSelected = null;

                _placeElements.InputEnabled = true;

                Debug.Log("Waiting Input");
                OnResetInput?.Invoke();

                _visualStatus.UpdateText(Enums.SelectionState.NONE);

                break;
            case Enums.BaseState.SELECTION_ELEMENT:
                _placeElements.InputEnabled = false;

                Debug.Log("Selection Element");

                OnUpdateSelection?.Invoke();
                break;
        }
    }

    public void ChangeSelectionState(Enums.SelectionState newState)
    {
        //exit previuos state
        switch (_selectionState)
        {
            case Enums.SelectionState.MOVING:
                _placeElements.InputEnabled = false;
                if (CurrentElementSelected != null)
                    CurrentElementSelected.Collider.enabled = true;
                break;
            case Enums.SelectionState.ROTATING:
                break;
            case Enums.SelectionState.SCALING:
                break;
            case Enums.SelectionState.DELETING:
                break;
        }

        //enter new state
        switch (newState)
        {
            case Enums.SelectionState.MOVING:
                if(CurrentElementSelected!=null)
                CurrentElementSelected.Collider.enabled = false;
                _placeElements.InputEnabled = true;
                //Debug.Log(_placeElements.InputEnabled +"moving...");
                break;
            case Enums.SelectionState.ROTATING:
                break;
            case Enums.SelectionState.SCALING:
                break;
            case Enums.SelectionState.DELETING:

                if (CurrentElementSelected != null)
                    Destroy(CurrentElementSelected.gameObject);

                ResetStates();
                break;
            case Enums.SelectionState.NONE:
                 
                break;
        }

        Debug.Log($"Enabled Selection mode: {newState}");

        _visualStatus.UpdateText(newState);

        //update state
        _selectionState = newState;
    }

    public void ResetStates()
    {
        ChangeBaseState(Enums.BaseState.WAITING_INPUT);
        ChangeSelectionState(Enums.SelectionState.NONE);
    }

}
