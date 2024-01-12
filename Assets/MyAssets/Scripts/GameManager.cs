using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float initialDelay = 1f;
    [SerializeField] TMP_Text gameText, infoText;
    [SerializeField] GameObject player, fadeObj, gameEndPanel;
    [SerializeField] Transform homeLocation;
    Animator anim;
    bool homeFound;

    public static Action ShowBlockerText, SpottedHome, EndGame;

    private void OnEnable()
    {
        ShowBlockerText += BlockerText;
        SpottedHome += HomeSpotted;
        EndGame += GameEnd;
    }
    private void OnDisable()
    {
        ShowBlockerText -= BlockerText;
        SpottedHome += HomeSpotted;
        EndGame -= GameEnd;
    }
    void Start()
    {
        anim = fadeObj.GetComponent<Animator>();    
        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (!homeFound)
            return;
        Vector3 dir = homeLocation.position - player.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, 4 * Time.deltaTime);
    }

    
    private IEnumerator StartGame()
    {
        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(false);
        yield return new WaitForSeconds(1);
        anim.Play("TextFadeIn");
        gameText.text = "You recently moved to a new town called 'WillowBrook' for work.\n\n\n" +
            "Its late night. \nYou are heading home.";
        yield return new WaitForSeconds(initialDelay);
        anim.Play("FadeOut");
        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(true);
        gameText.text = "";
    }

    void BlockerText()
    {
        StartCoroutine(IEnumBlockerText());
    }

    private IEnumerator IEnumBlockerText()
    {
        infoText.text = "I should head home. Not this way.";
        yield return new WaitForSeconds(2.5f);
        infoText.text = "";
    }

    void HomeSpotted()
    {
        StartCoroutine(ShowHome());
    }

    IEnumerator ShowHome()
    {
        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(false);

        homeFound = true;
        infoText.text = "There's my apartment.";
        yield return new WaitForSeconds(2);
        infoText.text = "";
        homeFound = false;

        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(true);
    }

    void GameEnd()
    {
        gameEndPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
