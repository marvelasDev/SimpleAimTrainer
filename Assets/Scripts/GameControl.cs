using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private GameObject target;
    [SerializeField] private int targetsAmount;
    [SerializeField] private Texture2D cursorTexture;

    [Header("Countdown UI")]
    [SerializeField] private Text getReadyText;
    [SerializeField] private string getReadyWord;

    [Header("Results Screen")]
    [SerializeField] private GameObject statsPanel;

    [SerializeField]
    private Text scoreText, targetsHitText, shotsFiredText, accuracyText; //UI

    public static int score, targetsHit;
     
    private float shotsFired, accuracy;

    private bool canPlay = false;

    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        getReadyText.gameObject.SetActive(false);

        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0f;
    }

    void Update()
    {
        if (canPlay && EventSystem.current.currentSelectedGameObject == null) //exclude start button as raycast target when shooting
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotsFired += 1f;
            }
        }
    }

    private IEnumerator GetReady()
    {
        for (int i = 3; i >= 1; i--) //countdown from 3
        {
            getReadyText.text = "Get Ready!\n" + i.ToString();
            yield return new WaitForSeconds(1f);
        }

        getReadyText.text = getReadyWord;
        yield return new WaitForSeconds(1f);

        StartCoroutine("SpawnTargets");
        
    }

    private IEnumerator SpawnTargets()
    {
        getReadyText.gameObject.SetActive(false);
        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0;

        for (int i = targetsAmount; i >= 1; i--)
        {
            Vector2 targetRandomPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            Instantiate(target, targetRandomPosition, Quaternion.identity);

            yield return new WaitForSeconds(1f); // spawn in one second increments
        }

        ShowResults();
    }

    private void ShowResults()
    {
        statsPanel.SetActive(true);
        scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmount;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        accuracy = targetsHit / shotsFired * 100f;
        accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %"; //N2 means limit to two decimal places only
        canPlay = false;
    }

    public void StartGetReadyCoroutine()
    {
        statsPanel.SetActive(false);
        getReadyText.gameObject.SetActive(true);
        StartCoroutine("GetReady");
        canPlay = true;
    }
}
