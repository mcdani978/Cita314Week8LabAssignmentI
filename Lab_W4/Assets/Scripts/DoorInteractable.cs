using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] private CombinationLock comboLock;
    [SerializeField] private Transform doorObject;
    [SerializeField] private Vector3 rotationLimits;
    [SerializeField] private Collider closedCollider;

    private bool isClosed;
    private Vector3 startRotation;
    private float startAngleX;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        startRotation = transform.localEulerAngles;  // Store the initial rotation
        startAngleX = startRotation.x;

        if (startAngleX >= 180)
        {
            startAngleX -= 360;
        }

        if (comboLock != null)
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

        if (isSelected)
        {
            CheckLimits();
        }
    }

    private void CheckLimits()
    {
        isClosed = false;
        float localAngleX = transform.localEulerAngles.x;

        if (localAngleX >= 180)
        {
            localAngleX -= 360;
        }

        if (localAngleX >= startAngleX + rotationLimits.x ||
            localAngleX <= startAngleX - rotationLimits.x)
        {
            ReleaseHinge();
        }
    }

    protected override void ResetHinge()
    {
        if (isClosed)
        {
            transform.localEulerAngles = startRotation; // Fix: Assigning Vector3 instead of Transform
        }
    }

    // Assuming these methods exist in the base class or need to be implemented
    protected abstract void LockHinge();
    protected abstract void UnlockHinge();
    protected abstract void ReleaseHinge();
}
