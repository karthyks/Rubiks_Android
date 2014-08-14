using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

    GameObject scriptHandler;
	void Start () {
        scriptHandler = GameObject.Find("ScriptHandler");
	}
	
	void Update () {
	
	}


    void OnGUI()
    {
        if(GUI.Button(new Rect(20,20,120,30), "Right"))
        {
            scriptHandler.BroadcastMessage("RotationType", "Top", SendMessageOptions.DontRequireReceiver);
            scriptHandler.BroadcastMessage("SetAxis", Vector3.up, SendMessageOptions.DontRequireReceiver);
            scriptHandler.BroadcastMessage("Rotate", 30, SendMessageOptions.DontRequireReceiver);
        }
    }
}
