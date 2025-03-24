using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] combinationlock comboLock;

    [SerializeField] private Transform doorObject;

    // Start is called before the first frame update
    void Start()
    {
        if(comboLock != null)
        {
            comboLock.UnlockAction += OnUnlocked;
            comboLock.LockAction += OnLocked;
        }
    }

    private void OnLocked()
    {
        LockHinge();
    }

    private void OnUnlocked()
    {
        UnlockHinge();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (doorObject != null)
        {
            doorObject.localEulerAngles = new Vector3(
                doorObject.localEulerAngles.x,
                transform.localEulerAngles.y,  // Ensures rotation follows the base hinge
                doorObject.localEulerAngles.z
            );
        }
    }
}
