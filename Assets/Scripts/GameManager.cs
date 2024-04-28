using Elements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ARElement CurrentElementSelected { get; set; }
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
                CurrentElementSelected.Collider.enabled = false;
                _placeElements.InputEnabled = true;
                break;
            case Enums.SelectionState.ROTATING:
                break;
            case Enums.SelectionState.SCALING:
                break;
            case Enums.SelectionState.DELETING:
                DestroyElement();
                break;
            case Enums.SelectionState.NONE:
                break;
        }

        Debug.Log($"Enabled Selection mode: {newState}");

        _visualStatus.UpdateText(newState);

        //update state
        _selectionState = newState;
    }

    private void DestroyElement()
    {
        ChangeBaseState(Enums.BaseState.WAITING_INPUT);
        ChangeSelectionState(Enums.SelectionState.NONE);

        Destroy(CurrentElementSelected.gameObject); 
    }
}
