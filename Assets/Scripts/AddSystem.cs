using System.Collections.Generic;
using UnityEngine;

public class AddSystem : MonoBehaviour
{
    [SerializeField] private GameObject _buttonAdd;
    [SerializeField] private GameObject _panel;


    [SerializeField] private Elements.Elements[] _allElements;
    [SerializeField] private ElementReader _readerPrefab;
    [SerializeField] private Transform _content;

    private List<ElementReader> _readerPrefabList = new();

    private void Awake()
    {
        _allElements = Resources.LoadAll<Elements.Elements>("Elements");
    }

    private void OnEnable()
    {
        if (_readerPrefabList != null)
            _readerPrefabList.ForEach(n => n.OnButtonChangeState -= CheckListener);
    }

    private void Start()
    {
        for (int i = 0; i < _allElements.Length; i++)
        {
            var pf = Instantiate(_readerPrefab, _content);
            _readerPrefabList.Add(pf);
            pf.SetUp(_allElements[i]);
        }

        ClosePanelElements();

        _readerPrefabList.ForEach(n => n.OnButtonChangeState += CheckListener); 
    }

    private void CheckListener(ElementReader current)
    {
        Debug.Log($"Current: {current.GetComponentInChildren<TMPro.TMP_Text>().text}");

        _readerPrefabList.ForEach(n =>
        {
            if (!n.Equals(current))
                n.ResetButton();
        });
    }

    #region Panels
    public void OpenPanelElements()
    {
        _buttonAdd.SetActive(false);
        _panel.SetActive(true);
    }

    public void ClosePanelElements()
    {
        _buttonAdd.SetActive(true);
        _panel.SetActive(false);
    }
    #endregion
}
