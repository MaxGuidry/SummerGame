using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
    public AudioClip MusicClip;
    public int StartDelay;
    public string SceneToStart;
    public Text MiddleText;
    
    private AudioSource MusicSource;

	// Use this for initialization
	void Start () {
        MusicSource = GetComponent<AudioSource>();
        MusicSource.clip = MusicClip;
        MusicSource.Play();
	}
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        StartCoroutine(Load(StartDelay, SceneToStart));
    }
    public void Controls()
    {
        MiddleText.text = "Controls"; 
    }
    public void Credits()
    {
        MiddleText.text = "Credits";
    }
    public IEnumerator Load(int delay, string scene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
