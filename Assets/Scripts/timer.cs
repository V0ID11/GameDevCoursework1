using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public Spawner spawner;
    private TextMeshProUGUI myText;

    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int) spawner.round_length / 60;
        int seconds = (int) spawner.round_length - (minutes * 60);
        string timerText = minutes.ToString("00") + ":" + seconds.ToString("00");

        myText.text = timerText;
    }
}
