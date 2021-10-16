using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour {
    
    public UnityEvent onInteract = new UnityEvent();

    public void OnClick()
    {
        AudioManager.audiomanager.Play("select");
        // Trigger the event
        onInteract.Invoke();
    }
}
