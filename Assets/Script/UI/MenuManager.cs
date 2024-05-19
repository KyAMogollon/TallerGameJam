using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header ("ScripTableObjects")]
    [SerializeField] public SoundSelectionSO _soundSelection;
    [Header ("Menu Buttons")]
    [SerializeField] Button[] _btnMenu;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _btnMenu[0].onClick.AddListener(() =>
        {
            _soundSelection.StartSoundSelection();
        });
        _btnMenu[1].onClick.AddListener(() =>
        {
            _soundSelection.StartSoundSelection();
            //HideButtonsMenu();
        });
        _btnMenu[2].onClick.AddListener(() =>
        {
            _soundSelection.StartSoundSelection();
            Application.Quit();
        });
    }



    void HideButtonsMenu()
    {
        foreach(Button buttons in _btnMenu)
        {
            buttons.gameObject.SetActive(false);
        }
    }
    void ShowButtonsMenu()
    {
        foreach (Button buttons in _btnMenu)
        {
            buttons.gameObject.SetActive(true);
        }
    }
}
