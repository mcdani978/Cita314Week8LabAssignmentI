using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] combinationlock comboLock;

    [SerializeField] Transform doorObject;

    [SerializeField] Vector3 rotationLimits;

    private Transform startRotation;

    private float StartAngleX;

    // Start is called before the first frame update
    protected override void Start() 
    {
        base.Start();   
        startRotation = transform;
        startAngleX = startRotation.localEulerAngles.x;
        if(sartAngleX >= 180)
        {
            startAngleX -= 360;
        }
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

        if(isSelected)
        {
            CheckLimits();
        }

        private void CheckLimits()
    {  
        float localAngleX = transform.localEulerAngles.x;

        if(localAngleX >= 180)
        {
            localAngleX -= 360;
        }
        if(localAngleX >= startAngleX + rotationLimits.x ||
            localAngleX <= StartAngleX - rotationLimits.x)
        {
            ReleaseHinge();
            transform.localEulerAngles = new Vector3(
                StartAngleX,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z
                );
        }
    }
}
