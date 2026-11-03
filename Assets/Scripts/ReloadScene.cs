using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    public Button selectButton;

    void Start()
    {
        if (selectButton != null)
        {
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(OnClickChoice);
        }
    }

    void OnDisable()
    {
        // Clean up listeners when the object/scene is torn down
        if (selectButton != null)
        {
            selectButton.onClick.RemoveAllListeners();
        }
    }

    public void OnClickChoice() 
    {
        Debug.Log("Here");
        SceneManager.LoadScene("GameOverScreenStuff", LoadSceneMode.Single);
    }
}