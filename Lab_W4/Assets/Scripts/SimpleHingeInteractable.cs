using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class SimpleHingeInteractable : XRSimpleInteractable
{
    [SerializeField] Vector3 positionLimits;

    private Transform grabHand;

    private Collider hingeCollider;

    private Vector3 hingePositions;

    [SerializeField] bool isLocked;

    private const string Default_Layer = "Default";

    private const string Grab_Layer = "Grab";

   protected virtual void Start()
    {
        hingeCollider = GetComponent<Collider>();
        hingePositions = hingeCollider.bounds.center;
    }

    public void LockHinge()
    {
        isLocked = true;
    }

    public void UnlockHinge()
    {
        isLocked = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(grabHand != null)
        {
            TrackHand();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isLocked)
        {
            base.OnSelectEntered(args);
            grabHand = args.interactorObject.transform;
        }

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        grabHand = null;
        ChangeLayerMask(Grab_Layer);
    }

    private void TrackHand()
    {
        transform.LookAt(grabHand, transform.forward);
        hingePositions = hingeCollider.bounds.center;
        if (grabHand.position.z >= hingePositions.z + positionLimits.z ||
    grandHand.position.z <= hingePositions.z - positionLimits.z)
        {
            ReleaseHinge();
            Debug.Log("****RELEASE HINGE X");
        }

             else if (grabHand.position.z >= hingePositions.z + positionLimits.z ||
            grandHand.position.z <= hingePositions.z - positionLimits.x)
        {
            ReleaseHinge();
            Debug.Log("****RELEASE HINGE Z");
        }

    }

    public void ReleaseHinge()
    {
        ChangeLayerMask(Default_Layer);
    }

    private void ChangeLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }
}
