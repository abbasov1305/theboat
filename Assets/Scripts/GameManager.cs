using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject aiPrefab;
    public GameObject giftPrefab;
    public float spawnRadius = 10f;
    public GameObject[] spawnPoints;


    [Header("Spawn Rate")]
    public float aiRate = 1f;
    public float giftRate = 10f;

    public float nextSpawn;


    [Header("UI")]
    public GameObject pGameOver;
    public Text tTimer;
    public Text tTimerEnd;

    [Header("LeanTween")]
    public GameObject GameOver;
    public GameObject ResultTime;

    public float timer;


    private void Start()
    {
        nextSpawn = aiRate;

        InvokeRepeating("SpawnGift", giftRate, giftRate);

    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) LoadMenu();




        if (player == null) {

            if (!pGameOver.activeSelf)
            {
                LeanTween.moveLocalY(GameOver, 500f, 1f).setEase(LeanTweenType.easeSpring);
                //LeanTween.scale(GameOver, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.animationCurve);
                LeanTween.scale(ResultTime, new Vector3(1.1f, 1.1f, 1.1f), 1f).setEase(LeanTweenType.easeInCirc);

                tTimerEnd.text = timer.ToString("00") + "s";
                pGameOver.SetActive(true);
            }

            return;
        }

        if (nextSpawn <= 0f)
        {
            SpawnAI();
            nextSpawn = aiRate;
        }
        else
            nextSpawn -= Time.deltaTime;


        timer += Time.deltaTime;
        tTimer.text = timer.ToString("00");

    }

    private void SpawnAI()
    {
        GameObject point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(aiPrefab, point.transform.position, point.transform.rotation);
    }

    private void SpawnGift()
    {
        Instantiate(giftPrefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), 5f, Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
