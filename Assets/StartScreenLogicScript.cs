using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenLogicScript : MonoBehaviour
{
    public Canvas controlMenu;
    public Canvas mainMenu;
    public Canvas keyChange;
    public Text flapKeyText;
    public string flapKey;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music.Play();
        controlMenu = GameObject.FindGameObjectWithTag("Control Menu").GetComponent<Canvas>();
        mainMenu = GameObject.FindGameObjectWithTag("Main Menu").GetComponent<Canvas>();
        keyChange = GameObject.FindGameObjectWithTag("Key Change").GetComponent<Canvas>();
        if (PlayerPrefs.GetString("FlapKey") != null && PlayerPrefs.GetString("FlapKey") != "")
        {
            flapKey = PlayerPrefs.GetString("FlapKey");
        }
        else {
            PlayerPrefs.SetString("FlapKey", "Space"); 
            flapKey = "Space";
        }
        flapKeyText.text = flapKey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameExit()
    {
        Application.Quit();
    }

    public void gameStart()
    {
        SceneManager.LoadScene("ActualGame", LoadSceneMode.Single);
    }

    public void gameControls()
    {
        mainMenu.enabled = false;
        controlMenu.enabled = true;
    }

    public void backToMenu()
    {
        controlMenu.enabled = false;
        mainMenu.enabled = true;
    }

    public void changeFlapKeyScreen()
    {
        controlMenu.enabled = false;
        keyChange.enabled = true;
        StartCoroutine(waitForFlapKey());
    }

    public IEnumerator waitForFlapKey()
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        for (KeyCode key = KeyCode.None; key < KeyCode.Joystick8Button19; key++)
        {
            if (Input.GetKey(key))
            {
                flapKey = key.ToString();
                flapKeyText.text = flapKey;
                PlayerPrefs.SetString("FlapKey", flapKey);
                break;
            }
        }
        keyChange.enabled = false;
        controlMenu.enabled = true;
        StopCoroutine(waitForFlapKey());
        yield break;
    }

    public void resetControls()
    {

    }
}
