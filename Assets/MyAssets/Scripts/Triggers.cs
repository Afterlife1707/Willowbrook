using UnityEngine;

public class Triggers : MonoBehaviour
{
    public enum _ETriggers
    {
        Footsteps, Knock, GhostFirstSight, GhostCrawl, CreepyDoll, Flicker, Final, HomeSpotted
    }

    [SerializeField] _ETriggers trigger;
    [SerializeField] AudioSource aSource;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartTrigger();
        }
    }

    void StartTrigger()
    {

        if (trigger==_ETriggers.Footsteps || trigger == _ETriggers.Knock)
        {
            AudioSource.PlayClipAtPoint(aSource.clip, aSource.gameObject.transform.position, 1);
        }
        else if (trigger == _ETriggers.GhostFirstSight)
        {
            AudioSource.PlayClipAtPoint(aSource.clip, aSource.gameObject.transform.position, 1);
            GhostHandler.TriggerGhostSight?.Invoke();
        }
        else if(trigger==_ETriggers.GhostCrawl)
        {
            AudioSource.PlayClipAtPoint(aSource.clip, aSource.gameObject.transform.position, 1);
            GhostHandler.TriggerGhostCrawl?.Invoke();
        }
        else if (trigger == _ETriggers.CreepyDoll)
        {
            AudioSource.PlayClipAtPoint(aSource.clip, aSource.gameObject.transform.position, 1);
            GhostHandler.TriggerCreepyDoll?.Invoke();
        }
        else if (trigger == _ETriggers.HomeSpotted)
        {
            GameManager.SpottedHome?.Invoke();
        }
        else if (trigger == _ETriggers.Flicker)
        {
            LightFlicker.TriggerFlicker?.Invoke();
        }
        else if (trigger == _ETriggers.Final)
        {
            AudioSource.PlayClipAtPoint(aSource.clip, aSource.gameObject.transform.position, 1);
            GhostHandler.TriggerFinalGhost?.Invoke();
        }

        Destroy(gameObject);
    }
}
