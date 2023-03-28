using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShootingGameController : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private int numTargets;
    [SerializeField] private Texture2D cursorTexture;

    [Header("Countdown UI")]
    [SerializeField] private Text countdownText;
    [SerializeField] private string countdownWord;

    [Header("Results Screen")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private Text scoreText, targetsHitText, shotsFiredText, accuracyText;

    public static int score, targetsHit;
    private float shotsFired, accuracy;

    private bool canPlay = false;

    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        countdownText.gameObject.SetActive(false);

        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0f;
    }

    void Update()
    {
        if (canPlay && EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotsFired += 1f;
            }
        }
    }

    private IEnumerator Countdown()
    {
        for (int i = 3; i >= 1; i--)
        {
            countdownText.text = "Get Ready!\n" + i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = countdownWord;
        yield return new WaitForSeconds(1f);

        StartCoroutine("SpawnTargets");
    }

    private IEnumerator SpawnTargets()
    {
        countdownText.gameObject.SetActive(false);
        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0;

        for (int i = numTargets; i >= 1; i--)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            Instantiate(targetPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }

        ShowResults();
    }

    private void ShowResults()
    {
        resultsPanel.SetActive(true);
        scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + numTargets;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        accuracy = targetsHit / shotsFired * 100f;
        accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
        canPlay = false;
    }

    public void StartGame()
    {
        resultsPanel.SetActive(false);
        countdownText.gameObject.SetActive(true);
        StartCoroutine("Countdown");
        canPlay = true;
    }
}
