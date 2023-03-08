using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChanger : MonoBehaviour
{
    public GameObject Settings;
    public GameObject SettingsButton;
    public GameObject MainMenu;

    void Start()
    {
        SettingsButton.SetActive(true);
        Settings.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void whenButtonClicked()
    {
        Settings.SetActive(true);
        MainMenu.SetActive(false);
        SettingsButton.SetActive(false);
    }
}
