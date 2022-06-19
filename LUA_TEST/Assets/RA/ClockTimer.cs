using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA.UtilMonobehaviours
{
    [MoonSharpUserData] 
    public class ClockTimer : MonoBehaviour
    {
        public bool activeOnLoad;
        public bool reverse;
        public bool loop;

        [SerializeField] private float current  = 0;
        [SerializeField] private float max = 5;
        [SerializeField] private float multiplier = 1;

        private bool _active = false;

        public delegate void ClockEvent(ClockTimer clock);
        public ClockEvent OnStart;
        public ClockEvent OnEnd;
        public ClockEvent OnUpdate;

        public float Current { get { return current; } set { current = value; } }
        public float Max { get { return max; } set { max = value; } }
        public float Multiplier { get { return multiplier; } set { multiplier = value; } }
        public bool IsActive() => _active;

        /// <summary>
        /// Start clock operation.
        /// </summary>
        public void StartClock()
        {
            _active = true;
        }

        /// <summary>
        /// Stop clock operation.
        /// </summary>
        public void StopClock()
        {
            _active = false;
        }

        // Start is called before the first frame update
        private void Start()
        {
            if(activeOnLoad)
            {
                StartClock();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (_active)
            {
                if(!reverse)
                    _Update(0,max,1);
                else
                    _Update(max, 0, -1);
            }
        }

        /// <summary>
        /// Internal Update
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="m"></param>
        private void _Update(float start,float end,int m)
        {
            if (current == start)
                OnStart?.Invoke(this);

            current += Time.deltaTime * multiplier * m;
            if ((m *current) >= (m *end))
            {
                OnEnd?.Invoke(this);
                current = start;

                if (!loop)
                    _active = false;
            }

            OnUpdate?.Invoke(this);
        }
    }
}
