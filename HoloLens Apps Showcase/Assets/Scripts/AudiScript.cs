using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;


public class AudiScript : MonoBehaviour {

    // Use this for initialization
    private TextToSpeechManager textToSpeech;

	void Start ()
    {
        var soundManager = GameObject.Find("Audio Manager");
        textToSpeech = soundManager.GetComponent<TextToSpeechManager>(); 
        textToSpeech.Voice = TextToSpeechVoice.Mark;
        textToSpeech.SpeakText("Welcome to the Holographic App ! Finish the scan with an airtap");
    }



    public void speak(string text)
    {
        textToSpeech.SpeakText(text);
    }
 

	
	
	// Update is called once per frame
	void Update () {
		
	}
}
