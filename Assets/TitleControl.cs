using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GameStartPushed()
    {
        SceneManager.LoadScene("STAGE01");
    }
    public void ExplanationPushed()
    {
        SceneManager.LoadScene("Explanation");
    }
}
