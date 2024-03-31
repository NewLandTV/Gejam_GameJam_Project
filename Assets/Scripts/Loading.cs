using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    private string loadSceneName;

    public CanvasGroup canvasGroup;
    public Image progressBar;

    // Singleton
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Scene load
    public void LoadScene(string sceneName)
    {
        loadSceneName = sceneName;

        canvasGroup.gameObject.SetActive(true);

        SceneManager.sceneLoaded += OnSceneLoaded;

        StartCoroutine(LoadSceneProcess());
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name.Equals(loadSceneName))
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // Scene load logic
    IEnumerator LoadSceneProcess()
    {
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        float timer = 0f;

        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;

                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;

                    yield break;
                }
            }

            yield return null;
        }
    }

    // Smooth scene move
    IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while(timer <= 1f)
        {
            timer += Time.unscaledDeltaTime * 3f;

            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);

            yield return null;
        }

        if(!isFadeIn)
        {
            canvasGroup.gameObject.SetActive(false);
        }
    }
}
