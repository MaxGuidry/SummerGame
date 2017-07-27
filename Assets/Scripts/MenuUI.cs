using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
    public Canvas thisCanvas, otherCanvas;
    public AudioClip MusicClip;
    public Slider loadingSlider;
    public int StartDelay;
    public int SceneToStart;
    public Text MiddleText,ProgressText;
    
    private AudioSource MusicSource;

	// Use this for initialization
	void Start () {
        MusicSource = GetComponent<AudioSource>();
        MusicSource.clip = MusicClip;
        MusicSource.Play();
        otherCanvas.gameObject.SetActive(false);
	}
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        StartCoroutine(Load());
    }
    public void Controls()
    {
        MiddleText.text = "Controls"; 
    }
    public void Credits()
    {
        MiddleText.text = "Credits";
    }
    public IEnumerator Load()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToStart);
        while (!operation.isDone)
        {
            thisCanvas.gameObject.SetActive(false);
            otherCanvas.gameObject.SetActive(true);

            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;
            ProgressText.text = progress * 100 + "%";

            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
