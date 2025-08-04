using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private Sprite _unmuteSprite;
    [SerializeField] private Button _muteButton;

    private void Start()
    {
        _musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        if (_musicSource.isPlaying)
        {
            _muteButton.GetComponent<Image>().sprite = _unmuteSprite;
            _muteButton.GetComponent<Image>().SetNativeSize();
        }
        else
        {
            _muteButton.GetComponent<Image>().sprite = _muteSprite;
            _muteButton.GetComponent<Image>().SetNativeSize();
        }
    }

    public void ChangeMusicState()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Pause();
            _muteButton.GetComponent<Image>().sprite = _muteSprite;
            _muteButton.GetComponent<Image>().SetNativeSize();
        }
        else
        {
            _musicSource.Play();
            _muteButton.GetComponent<Image>().sprite = _unmuteSprite;
            _muteButton.GetComponent<Image>().SetNativeSize();
        }
    }
}
