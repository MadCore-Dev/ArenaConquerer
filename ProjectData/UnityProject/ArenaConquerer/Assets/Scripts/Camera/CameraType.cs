using Cinemachine;
using UnityEngine;

public enum ThirdPersonCamera
{
    ThirdPersonCamera = 0,
    ThirdPersonCameraWithoutFollow, // do we also need to remove rotation in this type??
    ThirdPersonCameraWithoutRotation
}
//Will need to save default values of FreeLook camera before changing it to some other type
public class CameraType : MonoBehaviour
{
    [SerializeField] private ThirdPersonCamera TPCamera;
    [SerializeField] private Transform LookTarget;
    [SerializeField] private Transform FollowTarget;
    [SerializeField] private Transform ArenaPrefab;

    private CinemachineFreeLook thirdPersonCamera;
    private const string xAxisInput = "Mouse X";
    private const string yAxisInput = "Mouse Y";


    private void Awake()
    {
        if (!Camera.main.GetComponent<CinemachineBrain>())
        {
            Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
        
        thirdPersonCamera = GetComponent<CinemachineFreeLook>();
        ResetAttributes();
        SetCameraAttributes(TPCamera);
    }

    private void SetCameraAttributes(ThirdPersonCamera TPCamera)
    {
        switch (TPCamera)
        {
            case ThirdPersonCamera.ThirdPersonCamera:
                //Do NOTHING FOR NOW
                break;
            case ThirdPersonCamera.ThirdPersonCameraWithoutFollow:
                thirdPersonCamera.Follow = null;
                thirdPersonCamera.LookAt = ArenaPrefab;
                break;
            case ThirdPersonCamera.ThirdPersonCameraWithoutRotation:
                thirdPersonCamera.m_XAxis.m_InputAxisName = null;
                thirdPersonCamera.m_YAxis.m_InputAxisName = null;
                break;
            default:
                break;
        }
    }

    private void ResetAttributes()
    {
        thirdPersonCamera.m_XAxis.m_InputAxisName = xAxisInput;
        thirdPersonCamera.m_YAxis.m_InputAxisName = yAxisInput;

        thirdPersonCamera.LookAt = LookTarget;
        thirdPersonCamera.Follow = FollowTarget;
    }

}
