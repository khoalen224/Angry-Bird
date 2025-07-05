using UnityEngine;
using Unity.Cinemachine;
using System.Xml.Serialization;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera idleCam;
    [SerializeField] private CinemachineCamera followCam;

    private void Awake()
    {
        SwitchToIdleCam();
    }

    public void SwitchToIdleCam()
    {
        idleCam.enabled = true;
        followCam.enabled = false;
    }

    public void SwitchToFollowCam(Transform followTransform)
    {
        followCam.Follow = followTransform;

        followCam.enabled = true;
        idleCam.enabled = false;
    }
}
