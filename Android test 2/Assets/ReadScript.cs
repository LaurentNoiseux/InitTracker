using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ReadScript : MonoBehaviour {

    public Button buttonWrite;
	// Use this for initialization
	void Start () {
        buttonWrite.onClick.AddListener(Write);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void Write()
    {
        StreamWriter writer = new StreamWriter("test.txt", true);
        writer.WriteLine("Test");
        writer.WriteLine("Idrianna");
        writer.Close();
    }
}
