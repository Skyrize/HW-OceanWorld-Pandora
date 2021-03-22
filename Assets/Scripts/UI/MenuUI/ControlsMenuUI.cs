using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class KeyBind
{
    public string key;
    public string description;
}

public class ControlsMenuUI : MonoBehaviour
{
    [SerializeField] protected RectTransform itemPanelContentControls = null;
    [SerializeField] protected GameObject itemCardPrefab = null;
    [SerializeField] public List<KeyBind> keybind = null;


    public void CreateCard(KeyBind item, RectTransform container)
    {
        KeyBindUI cardUI = GameObject.Instantiate(itemCardPrefab, container).GetComponent<KeyBindUI>();
        cardUI.UpdateUI(item);
    }


    public void ClearUI()
    {
        itemPanelContentControls.ClearChilds();
    }

    public void BuildUI()
    {
        ClearUI();
        foreach (KeyBind item in keybind)
            CreateCard(item, itemPanelContentControls);
    }

    private void OnEnable()
    {
        BuildUI();
    }

    private void OnDisable()
    {
        ClearUI();
    }
}
