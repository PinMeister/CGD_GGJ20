﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    public DAMAGE_STATE damageState = DAMAGE_STATE.FUNCTIONAL;
    public float repairSpeed = 2f;

    protected RepairZone repairZone;
    protected bool canRepair = false;
    protected float repairProgress = 0f;
    protected PLAYER occupyingPlayer = PLAYER.NONE;

    void Start()
    {
        repairZone = GetComponentInChildren<RepairZone>();
    }

    void Update()
    {
        setRepairStatus();
        checkOccupyingPlayer();
        handleInput();
        // handle room stuff like light turning on;
    }

    private void checkOccupyingPlayer()
    {
        occupyingPlayer = repairZone.occupyingPlayer;
    }

    void handleInput()
    {
        //ALSO the button names are placeholders until we decide on the button
        if (Input.GetButton("Player1Repair") && occupyingPlayer == PLAYER.PLAYER1)
        {
            repair();
        }
        if (Input.GetButton("Player2Repair") && occupyingPlayer == PLAYER.PLAYER2)
        {
            repair();
        }
        if (Input.GetButton("Player1Action") && occupyingPlayer == PLAYER.PLAYER1)
        {
            doAction();
        }
        if (Input.GetButton("Player2Action") && occupyingPlayer == PLAYER.PLAYER2)
        {
            doAction();
        }

    }

    virtual protected void doAction()
    {
        // implement this in the extending class
    }

    void repair()
    {
        if (canRepair)
        {
            repairProgress += repairSpeed * Time.deltaTime;
            if (repairProgress >= 1f)
            {
                changeDamageState();
                repairProgress = 0f;
            }
        }
    }

    void changeDamageState()
    {
        if (damageState == DAMAGE_STATE.DESTROYED)
        {
            damageState = DAMAGE_STATE.DAMAGED;
        }
        else if (damageState == DAMAGE_STATE.DAMAGED)
        {
            damageState = DAMAGE_STATE.FUNCTIONAL;
        }
        else
        {
            Debug.Log("HEY WHAT ARE YOU DOING HERE THIS SHOULDNT BE POSSIBLE");
        }
    }

    void setRepairStatus()
    {
        if (damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            canRepair = false;
        }
        else
        {
            canRepair = repairZone.isOccupied;
        }

    }
}