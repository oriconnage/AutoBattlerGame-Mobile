using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public float minDuration = 1;
    public float maxDuration = 3;

    private float m_MaxLifetime;
    private bool m_EarlyStop;
    private IEnumerator Start()
    {
        var systems = GetComponentsInChildren<ParticleSystem>();

        // find out the maximum lifetime of any particles in this effect
        foreach (var system in systems)
        {
            m_MaxLifetime = Mathf.Max(system.startLifetime, m_MaxLifetime);
        }

        // wait for random duration

        float stopTime = Time.time + Random.Range(minDuration, maxDuration);

        while (Time.time < stopTime || m_EarlyStop)
        {
            yield return null;
        }
        

        // turn off emission
        foreach (var system in systems)
        {
            var emission = system.emission;
            emission.enabled = false;
        }
        BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);

        // wait for any remaining particles to expire
        yield return new WaitForSeconds(m_MaxLifetime);

        Destroy(gameObject);
    }
    public void Stop()
    {
        // stops the particle system early
        m_EarlyStop = true;
    }
}
