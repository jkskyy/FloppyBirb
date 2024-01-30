using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicScript logic;
    public BirbScript birb;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        birb = GameObject.FindGameObjectWithTag("Player").GetComponent<BirbScript>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && birb.birbIsAlive)
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            logic.addScore(1, highScore);
        }
    }
}