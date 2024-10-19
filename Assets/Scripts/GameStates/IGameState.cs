using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates
{
    public interface IGameState
    {
        public void OnPhaseStarted();

        public void OnPhaseEnded();
    }
}
