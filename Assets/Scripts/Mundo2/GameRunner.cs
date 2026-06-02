using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    [Range(0, 2)]
    [SerializeField]
    private int timer = 1;

    [Range(0, 60)]
    [SerializeField]
    private int matchTimer = 30;

    [SerializeField]
    private List<GameObject> listItems; 

    [SerializeField]
    private List<Sprite> fruitSprites;    
    
    [SerializeField]
    private List<Sprite> fastFoodSprites; 

    [Range(0f, 1f)]
    [SerializeField]
    private float fastFoodChance = 0.40f; 

    [SerializeField]
    private TMP_Text timerText;

    private System.Random randomNumberGenerator;
    private float currentTimer;

    void Start()
    {
        randomNumberGenerator = new System.Random();
        currentTimer = 0;
        
        if (GameManager.instancia != null)
        {
            GameManager.instancia.ResetWhackScore(); 
        }
        
        StartCoroutine(ChangeItem());
    }

    IEnumerator ChangeItem()
    {
        while (currentTimer < matchTimer)
        {
            yield return new WaitForSeconds(timer);

            // Esconde apenas as imagens de comida anteriores
            listItems.ForEach(item => item.SetActive(false));

            int randomSpot = randomNumberGenerator.Next(0, listItems.Count);
            GameObject chosenItem = listItems[randomSpot];

            bool isFastFood = (float)randomNumberGenerator.NextDouble() < fastFoodChance;

            Image img = chosenItem.GetComponent<Image>();

            if (isFastFood)
            {
                int idx = randomNumberGenerator.Next(0, fastFoodSprites.Count);
                img.sprite = fastFoodSprites[idx];
            }
            else
            {
                int idx = randomNumberGenerator.Next(0, fruitSprites.Count);
                img.sprite = fruitSprites[idx];
            }

            HitScript hit = chosenItem.GetComponentInParent<HitScript>();
            if (hit != null)
            {
                hit.SetIsFastFood(isFastFood);
            }

            chosenItem.SetActive(true);
        }
    }

    void Update()
    {
        if (GameManager.instancia == null) return;

        // Fim do tempo -> Guarda pontos, guarda a cena atual e vai para a ScoreScene
        if (currentTimer > matchTimer)
        {
            // SceneManager.GetActiveScene().name extrai automaticamente o nome exato da cena aberta
            GameManager.instancia.GuardarPontuacaoAntesDeSair(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("ScoreScene2.1");
        }

        // Pontuação negativa -> Guarda pontos, guarda a cena atual e vai para a ScoreScene
        if (GameManager.instancia.GetScore() < 0)
        {
            GameManager.instancia.GuardarPontuacaoAntesDeSair(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("ScoreScene2.1");
        }

        currentTimer += Time.deltaTime;

        float numberOfSeconds = matchTimer - currentTimer;
        float seconds = Mathf.FloorToInt(numberOfSeconds % 60);
        if (seconds >= 0 && timerText != null)
        {
            timerText.text = $"{seconds:00}";
        }
    }
}