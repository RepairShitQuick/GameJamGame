using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class OxygenBar : MonoBehaviour
    {
        public Transform Bar;
        public OxygenHandler OxygenHandler;


        public void Start()
        {
            OxygenHandler = GameObject.FindObjectOfType<OxygenHandler>();
        }

        public void Update()
        {
            Bar.localScale = new Vector3(Math.Max(0, (float)OxygenHandler.OxygenLeft / OxygenHandler.Max), 1f);
        }
    }
}
