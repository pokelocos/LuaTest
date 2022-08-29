using RA.UtilMonobehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MicroFactory
{
    [RequireComponent(typeof(EffectView))]
    [RequireComponent(typeof(ClockTimer))]
    public class EffectController : MonoBehaviour
    {
        private EffectView view;
        private ClockTimer timer;
        private EffectData data;


        public delegate void EffectEvent(EffectData effect);
        public EffectEvent OnEndEffect;

        public EffectData Data { get => data; }

        private void Awake()
        {
            view = GetComponent<EffectView>();
            timer = GetComponent<ClockTimer>();
        }

        private void Start()
        {
            InitEvents();
        }

        public void Init(EffectData data, float startTime)
        {
            this.data = data;
            timer.Current = startTime;
        }

        private void InitEvents() 
        {
            // On start timer
            timer.OnStart += (clock) =>
            {
                // poner lo que tiene que hacer el efecto cunado inicia (!)
            };

            // on update timer
            timer.OnUpdate += (clock) =>
            {
                view.SetTimer(clock.Current, clock.Max);
            };

            // On end timer
            timer.OnEnd += (c) =>
            {
                view.animator.SetTrigger("close");
                view.animator.SetTrigger("finish");
            };
        }
    }
}
