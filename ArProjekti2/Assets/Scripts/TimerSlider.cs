using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class TimerSlider : MonoBehaviour
{

    public bool colorChange = true;

    TextMeshProUGUI timerText;
    Slider timerSlider;

    public float gameTime;

    Image fillImage;
   // private Color32 normalFillColor;
    //public Color32 warningFillColor;
    public float warningLimit;

    public bool stopTimer;

    TextMeshProUGUI gameOverText;

    void Start()
    {
        

        stopTimer = false;
        gameObject.GetComponent<Shoot>().enabled = true;

        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>();
        gameOverText.gameObject.SetActive(false);

        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>();

        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>();

        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        fillImage.color =Color.green;

    }

    void Update()
    {
        gameTime -= Time.deltaTime;

        string textTime = "Time left: " + gameTime.ToString("f0") + "s";
        if (stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = gameTime;
        }
        //if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue)){
        //    fillImage.color = warningFillColor;
        //}
        if(gameTime < 10.0f && colorChange==true)
        {
            ChangeColor();
        }
        
        if(gameTime <= 0 && stopTimer == false)
        {
            gameObject.GetComponent<Shoot>().enabled = false;
            Destroy(timerSlider.gameObject);
            gameOverText.gameObject.SetActive(true);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Rabbit");
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);

            }
            StartCoroutine(SceneChange());
            
           
        }
    }
    private void ChangeColor()
    {
        fillImage.color = Color.red;
        colorChange = false;

    }
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
