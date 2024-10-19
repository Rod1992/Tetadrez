using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    public void OnPhaseStarted();

    public void OnPhaseEnded();
}
