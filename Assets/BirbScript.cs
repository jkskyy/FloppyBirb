using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birbIsAlive = true;
    public int highScore;
    public string flapKey;
    public AudioSource FlapSFX;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        highScore = PlayerPrefs.GetInt("HighScore");
        logic.setHighScore(highScore);
        flapKey = PlayerPrefs.GetString("FlapKey");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            logic.gameExit();
        }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), flapKey)) && birbIsAlive) {
            FlapSFX.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().Play("WingFlap", 0, 0.25f);
            myRigidbody.velocity = Vector2.up * flapStrength;
        }

        if(gameObject.transform.position.y > 13 || gameObject.transform.position.y < -13)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            if (birbIsAlive == true) logic.gameEnded = true;
            else logic.gameEnded = false;
            birbIsAlive = false;
            logic.gameOver(logic.gameEnded);        
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        if (birbIsAlive == true) logic.gameEnded = true;
        else logic.gameEnded = false;
        birbIsAlive = false;
        logic.gameOver(logic.gameEnded);  
    }
}
