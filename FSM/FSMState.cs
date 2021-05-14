using System;
using System.Collections.Generic;

namespace Damon.Tools
{
    public class FSMState
    {
        public event Action enterEvent;
        public event Action exitEvent;

        private FSM fsm;
        private string mStateName;
        public string stateName { get { return mStateName; } }

        private Dictionary<FSMState, Func<bool>> states = new Dictionary<FSMState, Func<bool>>();

        public FSMState UnRegister(FSMState state)
        {
            if (states.ContainsKey(state))
            {
                states.Remove(state);
            }
            return state;
        }
        public FSMState Register(FSMState state, Func<bool> func)
        {
            states.Add(state, func);
            return this;
        }
        public FSMState(string name, FSM fsm)
        {
            this.mStateName = name;
            this.fsm = fsm;
        }
        public void Update()
        {

            foreach (FSMState key in states.Keys)
            {
                if (states[key]())
                {
                    this.OnExit();
                    fsm.curState = key;
                    key.OnEtenr();
                }
            }
        }

        public void OnEtenr()
        {
            enterEvent?.Invoke();
        }

        public void OnExit()
        {
            exitEvent?.Invoke();
        }
    }
}