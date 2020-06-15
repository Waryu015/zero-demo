﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMachineDeadState : BaseState
{

    private BombMachine bombMachine;

    public BombMachineDeadState(BombMachine bombMachine)
    {
        this.bombMachine = bombMachine;
    }

    public override void execute()
    {
        Destroy(bombMachine.gameObject);
    }

    public override bool onEndState()
    {
        return true;
    }
}
