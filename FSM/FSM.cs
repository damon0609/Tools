using System.Collections.Generic;
using System;

namespace Damon.Tools
{


    public class FSM
    {
        public FSM() { }

        private bool isPause = false;

        private Dictionary<string, FSMState> states = new Dictionary<string, FSMState>();

        public void OnResume()
        {
            isPause = false;
        }
        public void OnPause()
        {
            isPause = true;
        }
        public void Add(FSMState state)
        {
            if (states.ContainsKey(state.stateName))
            {
                states.Add(state.stateName, state);
            }
        }
        public void Remove(FSMState state)
        {
            if (states.ContainsKey(state.stateName))
            {
                states.Remove(state.stateName);
            }
        }

        public void Entity(FSMState state)
        {
            this.curState = state;
            state.OnEtenr();
        }

        public event Action<FSMState> stateChangeEvent;

        private FSMState _curState;
        public FSMState curState
        {
            get => _curState;
            set
            {
                if (_curState != value)
                {
                    _curState = value;
                    if (stateChangeEvent != null)
                    {
                        stateChangeEvent.Invoke(value);
                    }
                }
            }
        }
        public void Update()
        {
            if (curState != null && !isPause)
            {
                curState.Update();
            }
        }
        public void ExitFsm()
        {
            states.Clear();
            stateChangeEvent = null;
            isPause = false;
            _curState = null;
        }
    }
}

