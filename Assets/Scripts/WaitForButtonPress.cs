using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForButtonPress : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(WaitForButtonDown());
	}

    IEnumerator WaitForButtonDown()
    {
        while (!Input.GetButtonDown("Submit"))
            yield return null;
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
