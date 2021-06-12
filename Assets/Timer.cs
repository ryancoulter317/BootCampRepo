using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    private void Start()
    {
        timerText.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = (Time.time.ToString());

    }
}
