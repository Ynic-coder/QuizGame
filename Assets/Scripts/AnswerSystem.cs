using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string question;
    public string[] answers;
    public int correctIndex;
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}

public class AnswerSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _answerButtons;
    [SerializeField] private List<Sprite> _backgorundImages;
    [SerializeField] private GameObject _questionTitle;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _questionCount;

    [SerializeField] private GameObject _truePanel;
    [SerializeField] private GameObject _falsePanel;

    [SerializeField] private AudioClip _correctAnswer;
    [SerializeField] private AudioClip _wrongAnswer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _finalTitle;
    [SerializeField] private GameObject _finalTitleBad;

    private QuestionList _allQuestions;
    private WaitForSeconds _sleepNextQuestion = new WaitForSeconds(1);
    private int _correctCount = 1;

    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "questions.json");
        string json = File.ReadAllText(path);
        _allQuestions = JsonUtility.FromJson<QuestionList>("{\"questions\":" + json + "}");
        SetQuestion();
    }

    private void SetQuestion()
    {
        if (_correctCount >= 10 + 1)
        {
            FinalGame();
            return;
        }
        _questionCount.GetComponent<TMP_Text>().text = _correctCount.ToString();
        int randomBackground = Random.Range(0, _backgorundImages.Count);
        _mainCanvas.GetComponent<Image>().sprite = _backgorundImages[randomBackground];
        int randomIndex = Random.Range(0, _allQuestions.questions.Count);
        Question question = _allQuestions.questions[randomIndex];
        _allQuestions.questions.Remove(question);
        _questionTitle.GetComponent<TMP_Text>().text = question.question;
        for (var i = 0; i < question.answers.Length; i++) {
            Debug.Log(_answerButtons.Count);
            _answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];
            Debug.Log(1);
            Button answerButton = _answerButtons[i].GetComponent<Button>();
            answerButton.onClick.RemoveAllListeners();
            if (i == question.correctIndex)
            {
                answerButton.onClick.AddListener(CorrectAnswer);
            }
            else
            {
                answerButton.onClick.AddListener(WrongAnswer);
            }
        }
    }

    private void CorrectAnswer()
    {
        _correctCount++;
        _truePanel.SetActive(true);
        _audioSource.PlayOneShot(_correctAnswer);
        StartCoroutine(TimerNextQuestion());
    }

    private void WrongAnswer()
    {
        _falsePanel.SetActive(true);
        _audioSource.PlayOneShot(_wrongAnswer);
        StartCoroutine(TimerExit());
    }

    IEnumerator TimerNextQuestion()
    {
        yield return _sleepNextQuestion;
        _truePanel.SetActive(false);
        SetQuestion();
    }


    IEnumerator TimerExit()
    {
        yield return _sleepNextQuestion;
        _falsePanel.SetActive(false);
        FinalGameBad();
    }

    private void FinalGame()
    {
        Object.Instantiate(_finalTitle);
    }

    private void FinalGameBad()
    {
        Object.Instantiate(_finalTitleBad);
    }
}
