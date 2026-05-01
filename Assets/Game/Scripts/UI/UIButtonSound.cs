using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip hover;
    [SerializeField] private AudioClip click;
    
    private new AudioSource audio;

    private UIButton[] uiButtons;
    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uiButtons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter += OnPointerEnter;
            uiButtons[i].PointerClick += OnPointerClicked;
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter -= OnPointerEnter;
            uiButtons[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerEnter(UIButton arg)
    {
        audio.PlayOneShot(hover);
    }

    private void OnPointerClicked(UIButton arg)
    {
        audio.PlayOneShot(click);
    }
}
