using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _nextPage;
    [SerializeField] private GameObject _thisPage;
    public void StartGameMethod()
    {
        Object.Instantiate(_nextPage);
        Destroy(_thisPage);
    }

    public void GoToMainMenuMethon()
    {
        GameObject questionPage = GameObject.Find("Question(Clone)");
        if (questionPage != null)
        {
            Destroy(questionPage);
        }
        Object.Instantiate(_nextPage);
        Destroy(_thisPage);
    }

    public void OpenHelp()
    {
        Object.Instantiate(_nextPage);
    }

    public void CloseHelp()
    {
        Destroy(_thisPage);
    }
}
