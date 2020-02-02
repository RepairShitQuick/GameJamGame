using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing : MonoBehaviour
{
    public float GrowRate = 0.5f;
    public float MaxVolume = 5f;

    public float Health {
        get {
            return m_ParticleSystem.emission.rateOverTime.constant;
        }
        set {
            var emission = m_ParticleSystem.emission;
            emission.rateOverTime = value;
        }
    }
    protected float CurrentVolume;
    protected ParticleSystem m_ParticleSystem;
    protected BoxCollider m_BoxCollider;
    protected bool Collided = false;

    // Start is called before the first frame update
    protected void Start()
    {
        this.SetCurrentVolume();
        m_ParticleSystem = gameObject.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
        m_BoxCollider = gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
    }

    // Update is called once per frame
    protected void Update()
    {
        this.UpdateGrowth();
    }

    protected void OnTriggerEnter(Collider other)
    {
        Collided = true;
    }

    private void SetCurrentVolume() {
        var localScale = gameObject.transform.localScale;
        CurrentVolume = localScale.x * localScale.y * localScale.z;
    }

    protected void UpdateGrowth() {
        if (!Collided && CurrentVolume < MaxVolume) {
            var main = m_ParticleSystem.main;
            var emission = m_ParticleSystem.emission;
            main.startLifetime = main.startLifetime.constant + GrowRate/10 * Time.deltaTime;
            emission.rateOverTime = emission.rateOverTime.constant + 100f * Time.deltaTime;
            Vector3 scaleVector = new Vector3(GrowRate * Time.deltaTime, 0f, GrowRate * Time.deltaTime);
            gameObject.transform.localScale += scaleVector;
            m_BoxCollider.size += scaleVector;
            
            this.SetCurrentVolume();
        }
    }
}
