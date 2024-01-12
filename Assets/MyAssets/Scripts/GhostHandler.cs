using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhostHandler : MonoBehaviour
{
    [SerializeField] GameObject ghostSightModel, player, creepyDoll, finalGhostModel, finalGhostLookAt, blackScreen, parkGhostHead;
    public static Action TriggerGhostSight, TriggerGhostCrawl, TriggerCreepyDoll,TriggerFinalGhost;
    [SerializeField] AudioClip finalJumpscare;
    [SerializeField] float rotationSpeed = 4f;
    [SerializeField] float crawlDuration = 1f, lookAtGhostDuration = 3f;
    [SerializeField] float crawlSpeed = 1.5f;
    [SerializeField] float waitTillGhostFloats = 3, waitBeforeBlackScreen = 5, waitBeforeGameEnd = 2;

    private void OnEnable()
    {
        TriggerGhostSight += GhostSight;
        TriggerGhostCrawl += GhostCrawl;
        TriggerCreepyDoll += CreepyDoll;
        TriggerFinalGhost += FinalGhost;
    }
    private void OnDisable()
    {
        TriggerGhostSight -= GhostSight;
        TriggerGhostCrawl -= GhostCrawl;
        TriggerCreepyDoll -= CreepyDoll;
        TriggerFinalGhost -= FinalGhost;
    }

    Vector3 dir;
    Quaternion rot;

    private void Update()
    {
        if (ghostSightModel && ghostSightModel.activeInHierarchy)
        {
            dir = parkGhostHead.transform.position - player.transform.position;
            rot = Quaternion.LookRotation(dir);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
        if(finalGhostModel && finalGhostModel.activeInHierarchy)
        {
            dir = finalGhostLookAt.transform.position-player.transform.position;
            rot = Quaternion.LookRotation(dir);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
        if (creepyDoll && creepyDoll.activeInHierarchy)
        {
            creepyDoll.transform.LookAt(player.transform);
        }
    }

    void GhostSight()
    {
        ghostSightModel.SetActive(true);
        StartCoroutine(DisableControlsAtFirstGhost());
    }

    void GhostCrawl()
    {
        ghostSightModel.GetComponent<Animator>().Play("Crawl");
        StartCoroutine(MoveForDuration(crawlDuration));
    }
    void CreepyDoll()
    {
        Destroy(creepyDoll);
    }

    void FinalGhost()
    {
        finalGhostModel.SetActive(true);
        AudioSource.PlayClipAtPoint(finalJumpscare, player.transform.position, 1);
        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(false);
        StartCoroutine(EndGame());
    }

    IEnumerator DisableControlsAtFirstGhost()
    {
        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(false);
        yield return new WaitForSeconds(lookAtGhostDuration);
        FirstPersonController.FPSInstance.EnableDisablePlayerMovement(true);
        Destroy(ghostSightModel);
    }

    IEnumerator MoveForDuration(float duration)
    {
        float timer = 0.0f;

        
        while (timer < duration)
        {
            ghostSightModel.transform.Translate
                (new Vector3(0 ,0 ,ghostSightModel.transform.position.z) * crawlSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(ghostSightModel);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(waitTillGhostFloats);
        finalGhostModel.GetComponent<Animator>().Play("FloatUp");
        yield return new WaitForSeconds(waitBeforeBlackScreen);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(waitBeforeGameEnd);
        GameManager.EndGame?.Invoke();
    }
}
