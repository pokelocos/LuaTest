using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;

namespace RA.Inputs
{
    public static class InputUtils
    {
        private static float DOUBLE_CLICK_TIME = 0.15f;

        private static Dictionary<int, float> lastInputs = new Dictionary<int, float>();

        public static bool MouseDoubleCLick(int input)
        {
            if (UnityEngine.Input.GetMouseButtonDown(input))
            {
                if (!lastInputs.ContainsKey(input))
                {
                    lastInputs.Add(input,Time.unscaledTime);
                    return false;
                }

                float last;
                lastInputs.TryGetValue(input,out last);
                float delta = Time.unscaledTime - last;

                if (delta <= DOUBLE_CLICK_TIME)
                {
                    return true;
                }
                else
                {
                    lastInputs[input] = Time.unscaledTime;
                    return false;
                }
            }
            return false;
        }

        public static GameObject GetOverObject2D()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var rayhit = Physics2D.Raycast(point, Vector2.zero);
            if (rayhit.collider != null)
            {
                return rayhit.collider.gameObject;
            }
            return null;
        }
    }
}