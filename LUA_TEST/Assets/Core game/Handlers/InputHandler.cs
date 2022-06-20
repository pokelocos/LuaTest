using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;

namespace RA.Inputs
{
    public static class InputHandler
    {
        private static float DOUBLE_CLICK_TIME = 0.15f;

        private static Dictionary<int, float> lastInputs = new Dictionary<int, float>();

        public static bool MouseDoubleCLick(int input)
        {
            if (UnityEngine.Input.GetMouseButtonDown(input))
            {
                
                float last;
                if (lastInputs.TryGetValue(input, out last))
                {
                    Debug.Log("B");
                    lastInputs.Add(input,Time.unscaledTime);
                    return false;
                }
                Debug.Log("A: " + last);

                float delta = Time.unscaledTime - last;
                Debug.Log("C: "+ delta);

                if (delta <= DOUBLE_CLICK_TIME)
                {
                    Debug.Log("D");
                    lastInputs.Add(input, Time.unscaledTime);
                    return true;
                }
            }
            return false;
        }
    }


}