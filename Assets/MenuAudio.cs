using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(Click);
        }
    }

    public void Click()
    {
        _audioSource.PlayOneShot(_clickClip);
    }
}
