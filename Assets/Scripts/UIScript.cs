using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {


	public bool shuffleOver;
	public bool solveOver;
    GameObject scriptHandler;
	void Start () {
        scriptHandler = GameObject.Find("ScriptHandler");
		shuffleOver = true;
		solveOver = true;
	}
	
	void Update () {
		if (!shuffleOver || !solveOver) 
		{
            ButtonEnabler("Shuffle", false);
            ButtonEnabler("Solve", false);
		} 
		else 
		{
            ButtonEnabler("Shuffle", true);
            ButtonEnabler("Solve", true);
		}
	}

    void ButtonEnabler(string buttonName, bool enable)
    {
        UIButton button = GameObject.Find(buttonName).GetComponent<UIButton>();
        button.isEnabled = enable;
    }

    void OnGUI()
    {

    }

	void Pause()
	{
		GameObject.Find ("Camera1").camera.enabled = false;
		GameObject.Find ("Camera").camera.enabled = true;
	}

	void Resume()
	{
		GameObject.Find ("Camera1").camera.enabled = true;
		GameObject.Find ("Camera").camera.enabled = false;
	}
}
