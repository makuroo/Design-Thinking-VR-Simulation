using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BNG
{
    public class ScreenDetector : MonoBehaviour
    {
        public PlayerScript playerScript;
        public GameObject watchTarget;
        public GameObject target;
        public Camera cam;

        private void Start()
        {

        }

        private bool isVisible(Camera c, GameObject target)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(c);
            var point = target.transform.position;

            foreach (var plane in planes)
            {
                if (plane.GetDistanceToPoint(point) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void Update()
        {
        
                if (isVisible(cam, target))
                {
                    if (target == watchTarget) //jika targetnya itu watch
                    {
                        //playerScript.StopRepeatingVibrate();
                        SetTargetNone();
                        Debug.Log("Watch masuk screen");
                    }
                }
                else
                {
                    Debug.Log("Malah masuk else dah");
                }
            

        }

        public void SetTargetAsWatch()
        {
            target = watchTarget;
            Debug.Log("set target as watch");
        }

        public void SetTargetNone()
        {
            target = null;
        }
    }
}

