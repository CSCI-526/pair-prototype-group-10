using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    private static bool hasPlayedBefore = false;
    private bool hasMoved = false;
    private bool alliancePromptShown = false;

    void Start()
    {
        if (!hasPlayedBefore)
        {
            StartCoroutine(ShowMoveInstructions());
        }
        else
        {
            tutorialText.text = "";
        }
    }

    IEnumerator ShowMoveInstructions()
    {
        Time.timeScale = 0;
        tutorialText.text = "Press A/D to Move";

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Return));
        
        tutorialText.text = "";
        Time.timeScale = 1;
        hasMoved = true;
        hasPlayedBefore = true;
    }

    void Update()
    {
        if (hasMoved && !alliancePromptShown && EnemySpawner.enemyCount >= 3 && !GameOverManager.gameOverTriggered)
        {
            StartCoroutine(ShowAllianceInstructions());
        }
    }

    IEnumerator ShowAllianceInstructions()
    {
        Time.timeScale = 0;
        tutorialText.text = "Press E to Form an Alliance!";

        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && EnemySpawner.enemyCount >= 2);
        
        tutorialText.text = "";
        Time.timeScale = 1;
        alliancePromptShown = true;
    }

    public static void ResetTutorial()
    {
        hasPlayedBefore = false;
    }
}
