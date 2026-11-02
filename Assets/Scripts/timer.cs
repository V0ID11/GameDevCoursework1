using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public OrchSpawner spawner;
    private TextMeshProUGUI myText;

    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int) spawner.round_timer / 60;
        int seconds = (int) spawner.round_timer - (minutes * 60);
        string timerText = minutes.ToString("00") + ":" + seconds.ToString("00");

        myText.text = timerText;
    }
}
