using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    public Item thisItem;
    private ButtonView buttonView;
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    private void Start()
    {
        buttonView = this;

        thisButton.onClick.AddListener(() => TapCheck.instance.TapTrack(buttonView));
    }

    public void DisableInteraction()
    {
        thisButton.interactable = false;
    }

    public void EnableInteraction()
    {
        thisButton.interactable = true;
    }
}