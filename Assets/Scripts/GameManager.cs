using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Player;

    // Maps
    public GameObject[] maps;
    private int currentStage;

    public Text scoreTxt;
    private int score;

    public Text hpTxt;
    private int hp = -3;

    public Text stageTxt;

    private bool isDead;

    public GameObject DeadGroup;

    // Update Logic
    void Update()
    {
        CheckHp();
    }

    private void CheckHp()
    {
        if (hp >= 0 && !isDead)
        {
            isDead = true;

            Dead();
        }
    }

    // UI update
    void LateUpdate()
    {
        PlayerPrefs.SetInt("erocSSD", score);

        scoreTxt.text = string.Format("<color=#0dea3c>{0:n0}</color> : EROCS", score);
        hpTxt.text = string.Format("<color=#02f02f>{0:n0}</color> : PH", hp);
        stageTxt.text = string.Format("<color=#efa30f>{0:n0}</color> EGATS", currentStage + 1);
    }

    // next Stage
    public void nextStage()
    {
        SoundManager.instance.PlaySFX("Finish");

        score -= 50;

        maps[currentStage].SetActive(false);

        currentStage += 1;

        if (currentStage == maps.Length)
        {
            currentStage = 0;
        }

        maps[currentStage].SetActive(true);
    }

    // Decrease hp
    public void DecreaseHp()
    {
        hp++;
        score += 10;
    }

    // Player dead
    private void Dead()
    {
        SoundManager.instance.PlaySFX("Dead");

        Player.GetComponent<Player>().isMoveable = false;

        DeadGroup.SetActive(true);

        Invoke("SceneMoveToTitle", 1.5f);
    }

    // Title scene move
    private void SceneMoveToTitle()
    {
        Loading.instance.LoadScene("Title");
    }
}
