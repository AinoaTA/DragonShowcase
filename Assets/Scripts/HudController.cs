using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField] private OptionsSystem _optionSystem;
    [SerializeField] private AddSystem _addSystem;

    private void OnEnable()
    {
        GameManager.Instance.OnUpdateSelection += ()=> { ChangeHud(Enums.HudState.OPTIONS); };
        GameManager.Instance.OnResetInput += () => { ChangeHud(Enums.HudState.ADD); };
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnUpdateSelection -= () => { ChangeHud(Enums.HudState.OPTIONS); };
            GameManager.Instance.OnResetInput -= () => { ChangeHud(Enums.HudState.ADD); };
        }
    }
 
    public void ChangeHud(Enums.HudState hud)
    {
        _addSystem.gameObject.SetActive(hud == Enums.HudState.ADD);
        _optionSystem.gameObject.SetActive(hud == Enums.HudState.OPTIONS); 
    }
}
