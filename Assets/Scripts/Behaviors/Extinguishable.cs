using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguishable : Growing
{
    public float ExtinguishRate = 1;
    private bool IsExstinguishing = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (this.IsExstinguishing) {
            this.Health += -this.ExtinguishRate;
        } else {
            base.Update();
        }
    }

    new void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Extinguisher") {
            print("extinguish");
            this.IsExstinguishing = true;
        } else {
            print("Other " + other.gameObject.tag);
            base.OnTriggerEnter(other);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Exstinguisher") {
            this.IsExstinguishing = false;
        }
    }
}
