using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public string[] message;
    public TextMeshProUGUI ErrorMessage;
    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
    }
    
    public void ShowError(int errorCode)
    {
        transform.localScale = Vector3.one;
        ErrorMessage.text = message[errorCode];
        Time.timeScale = 0.0f;
    }
}
