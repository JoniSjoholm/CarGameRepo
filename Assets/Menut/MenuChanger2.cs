using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChanger2 : MonoBehaviour
{
    public GameObject Settings;
    public GameObject SettingsButton;
    public GameObject MainMenu;

    void Start()
    {
        SettingsButton.SetActive(false);
        Settings.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void whenButtonClicked()
    {
        Settings.SetActive(false);
        MainMenu.SetActive(true);
        SettingsButton.SetActive(true);
    }
}
