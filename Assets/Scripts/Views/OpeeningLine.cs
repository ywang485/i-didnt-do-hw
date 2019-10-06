using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class OpeeningLine : MonoBehaviour {
    
    public void goToNextLine()
    {
        GetComponent<Image>().DOFade(0.0f, 0.5f).OnComplete(deactivateSelf);
        GetComponentInChildren<Text>().DOFade(0.0f, 0.5f);
    }

    public void deactivateSelf()
    {
        gameObject.SetActive(false);
    }

    public void gameStart()
    {
        SceneManager.LoadScene("GamePlay");
    }

	// Use this for initialization
	void Start () {
        Screen.SetResolution(600, 1000, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
