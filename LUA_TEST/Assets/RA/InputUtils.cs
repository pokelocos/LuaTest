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
        private static float DOUBLE_CLICK_TIME = 0.25f;

        //private static Dictionary<int, float> lastInputs = new Dictionary<int, float>();
        private static Dictionary<object, Dictionary<int, float>> lastInputs = new Dictionary<object, Dictionary<int, float>>();

        public static bool MouseDoubleCLick(int input,object obj)
        {
            if (UnityEngine.Input.GetMouseButtonDown(input))
            {
                Dictionary<int, float> dic;
                if (!lastInputs.ContainsKey(obj))
                {
                    dic = new Dictionary<int, float>();
                    lastInputs.Add(obj, dic);
                }
                else
                {
                    lastInputs.TryGetValue(obj, out dic);
                }

                if (!dic.ContainsKey(input))
                {
                    dic.Add(input,Time.unscaledTime);
                    return false;
                }

                float last;
                dic.TryGetValue(input,out last);
                float delta = Time.unscaledTime - last;

                lastInputs[obj][input] = Time.unscaledTime;
                return delta <= DOUBLE_CLICK_TIME;
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