using UnityEngine;
using UnityEngine.UI;

public class TItle : MonoBehaviour
{
    public Text maxScoreTxt;

    // Load score, and max score calculate
    void Start()
    {
        if(PlayerPrefs.HasKey("erocSSD"))
        {
            maxScoreTxt.text = string.Format("<color=#2edae4>{0:n0} : erocS xaM</color>", PlayerPrefs.GetInt("erocSSD"));
        }
        else
        {
            maxScoreTxt.text = string.Format("<color=#2edae4>0 : erocS xaM</color>");
        }
    }

    // Start button pressed call
    public void GameStart()
    {
        SoundManager.instance.PlaySFX("ButtonClick");
        Loading.instance.LoadScene("Game");
    }
}
