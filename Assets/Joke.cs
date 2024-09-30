using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HJoke
{
    public string id;
    public string joke;
    public int status;
}

[System.Serializable]
public class DJoke
{
    public List<string> jokes;
}

public enum Language
{
    ES,
    EN
}

public class Joke : MonoBehaviour
{
    [SerializeField] private float setCharTime;
    [SerializeField] private TextMeshProUGUI _jokeText;

    private List<string> darkJokesSpanish = new();
    private List<string> darkJokesEnglish = new();


    [SerializeField] private TextMeshProUGUI _generateText;
    [SerializeField] private Image _emoticonImage;
    [SerializeField] private Sprite[] _emoticons;

    [SerializeField] private Language _language;

    private UnityEngine.Coroutine jokeCorutine;

    private void Awake()
    {
        GetDarkJokesSpanish();
        GetDarkJokesEnglish();
    }

    void Start()
    {
        SetLanguage(PlayerPrefs.GetInt("Language", 1));

        GenerateDarkJoke();
    }

    public void SetLanguage(int languageCode)
    {
        _language = (Language)languageCode;

        PlayerPrefs.SetInt("Language", languageCode);

        SetGenerateText();
    }

    private void SetGenerateText()
    {
        if (_language.Equals(Language.ES))
        {
            _generateText.text = "Generar";
        }
        else
        {
            _generateText.text = "Generate";
        }
    }

    public void GetDarkJokesSpanish()
    {
        LoadJokes("jokes", ref darkJokesSpanish);
    }
    public void GetDarkJokesEnglish()
    {
        LoadJokes("darkjokes", ref darkJokesEnglish);
    }

    public void LoadJokes(string fileName, ref List<string> jokesList)
    {
        TextAsset jokesJSON = Resources.Load<TextAsset>(fileName);
        DJoke dJokes = JsonUtility.FromJson<DJoke>(jokesJSON.text);
        jokesList = dJokes.jokes;
    }

    public void GenerateDarkJoke()
    {
        List<string> jokes = new();
        switch (_language)
        {
            case Language.ES:
                jokes = darkJokesSpanish;
                break;
            case Language.EN:
                jokes = darkJokesEnglish;
                break;
            default:
                jokes = darkJokesEnglish;
                break ;
        }

        _jokeText.text = string.Empty;

        int emoticonIndex = Random.Range(0, _emoticons.Length);
        _emoticonImage.sprite = _emoticons[emoticonIndex];

        int jokeIndex = Random.Range(0, jokes.Count);
        string joke = jokes[jokeIndex];

        if (jokeCorutine != null)
            StopCoroutine(jokeCorutine);
        jokeCorutine = StartCoroutine(SetText(joke));
    }

    private IEnumerator SetText(string joke)
    {
        for (int i = 0; i < joke.Length; i++)
        {
            _jokeText.text += joke[i];
            yield return new WaitForSeconds(setCharTime);
        }
    }

    //public void GenerateDadyJoke()
    //{
    //    _jokeText.text = "";
    //    StartCoroutine(GetRequest("https://icanhazdadjoke.com/"));
    //}

    //IEnumerator GetRequest(string uri)
    //{
    //    UnityWebRequest request = UnityWebRequest.Get(uri);
    //    request.SetRequestHeader("Accept", "application/json");
    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.LogError("Error: " + request.error);
    //    }
    //    else
    //    {
    //        string responseText = request.downloadHandler.text;
    //        print(responseText);
    //        HJoke joke = JsonUtility.FromJson<HJoke>(responseText);
    //        //_jokeText.text = joke.joke;
    //        StartCoroutine(SetText(joke.joke));
    //        // Accede a las propiedades
    //        Debug.Log("Response: " + request);
    //        Debug.Log("Response: " + request.downloadHandler.text);
    //    }
    //}
}
