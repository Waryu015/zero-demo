﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class PlayerStateMachine : MonoBehaviour
{
    
    private PlayerZero playerZero;
    public BaseState currentState;


    public PlayerStateMachine(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        currentState = new PlayerStandState(playerZero);
    }

    /**
     * 状态切换逻辑
     * 所有状态：站立状态，跑步状态，冲刺状态，跳跃状态，死亡状态
     * 初始状态：站立状态
     * 一、站立状态
     * 1. 跑步：水平速度为跑步速度
     * 2. 冲刺：水平速度为冲刺速度
     * 3. 跳跃：按下跳跃按钮
     * 4. 死亡：hp为0
     * 二、跑步状态
     * 1. 站立：水平速度为0
     * 2. 冲刺：水平速度为冲刺速度
     * 3. 跳跃：按下跳跃按钮
     * 4. 死亡：hp为0
     * 三、冲刺状态
     * 1. 站立：水平速度为0
     * 2. 跑步：水平速度为跑步速度
     * 3. 跳跃：按下跳跃按钮
     * 4. 死亡：hp为0
     * 四、跳跃状态
     * 1. 站立：触碰地面
     * 2. 死亡：hp为0
     */
    public void CheckChangeState()
    {
        if(playerZero.hp <= 0)
        {
            DoChangeState(new PlayerDeathState(playerZero));
            return;
        }

        // 一、站立
        if(currentState is PlayerStandState)
        {
            if(Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.runSpeed)
            {
                DoChangeState(new PlayerMoveState(playerZero));
            }
            else if(Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.dashSpeed)
            {
                DoChangeState(new PlayerDashState(playerZero));
            }
            else if(playerZero.DoJump())
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }
        }

        // 二、跑步
        if(currentState is PlayerMoveState)
        {
            if (playerZero.currentHorizontalSpeed == 0)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
            else if (Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.dashSpeed)
            {
                DoChangeState(new PlayerDashState(playerZero));
            }
            else if (playerZero.DoJump())
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }
        }
        
        // 三、冲刺
        if(currentState is PlayerDashState)
        {
            if (playerZero.currentHorizontalSpeed == 0)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
            else if (Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.runSpeed)
            {
                DoChangeState(new PlayerMoveState(playerZero));
            }
            else if (playerZero.DoJump())
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }
        }

        // 四、跳跃
        if(currentState is PlayerJumpState)
        {
            if(playerZero.isGrounded)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
        }

    }


    public void DoChangeState(BaseState newState)
    {
        bool canChange = currentState.onEndState();
        if (canChange)
        {
            currentState = newState;
        }
    }

}