using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        Debug.Log("Init SDK's, request authentication, getting user info, etc");

        _startButton.onClick.AddListener(LoadGameplayScene);
    }

    private void LoadGameplayScene()
    {
        SceneController.LoadScene(Scenes.Gameplay);
    }
}
