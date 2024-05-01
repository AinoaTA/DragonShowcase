using UnityEngine;

public class OptionsSystem : Menus
{
    [SerializeField] private GameObject _move;
    [SerializeField] private GameObject _delete;
    [SerializeField] private GameObject _rotate;
    [SerializeField] private GameObject _scale;

    private HudController _hudController;
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnUpdateSelection += Init;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnUpdateSelection -= Init;
    }

    public void Init()
    {
        OptionsListener l = GameManager.Instance.CurrentElementSelected.Listener;

        Debug.Log(l.CanMove);
        Debug.Log(l.CanBeRotated);
        Debug.Log(l.CanBeScaled);
        Debug.Log(l.CanBeDeleted);

        _delete.SetActive(l.CanBeDeleted);
        _move.SetActive(l.CanMove);
        _scale.SetActive(l.CanBeScaled);
        _rotate.SetActive(l.CanBeRotated);
    }

    public override void Init(HudController h)
    {
        _hudController = h;
    }

    public override void ClosePanel()
    {
        GameManager.Instance.ChangeBaseState(Enums.BaseState.WAITING_INPUT);
    }
}
