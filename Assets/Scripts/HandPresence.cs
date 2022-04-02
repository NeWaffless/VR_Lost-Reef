using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    // some code borrowed from 'Valem' on youtube
    public InputDeviceCharacteristics controllerCharacteristics;
    [SerializeField] bool isRight;
    public GameObject handModelPrefab;
    public GameObject propellerPrefab;


    InputDevice targetDevice;
    GameObject spawnedHandModel;
    GameObject spawnedPropellerModel;
    Animator handAnimator;

    void Start ()
    {
        TryInitialise();
    }

    void TryInitialise()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if(devices.Count > 0)
        {
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();

            if(isRight)
            {
                spawnedPropellerModel = Instantiate(propellerPrefab, transform);
            }
        }
    }

    void UpdateHandAnimation()
    {
        if(!targetDevice.isValid)
        {
            TryInitialise();
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
