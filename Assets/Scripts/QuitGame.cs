using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button selectButton;

    void Start()
    {
        if (selectButton != null)
        {
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(doExitGame);
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

    

    void doExitGame() {
    Application.Quit();
}
}
