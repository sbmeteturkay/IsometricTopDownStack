//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;

namespace MeteTurkay{
	public class GameManager : MonoBehaviour
	{
		public Cinemachine.CinemachineVirtualCamera CinemachineVirtualCamera;
        private void Awake()
        {
            CinemachineVirtualCamera.m_Lens.NearClipPlane = -50f;
        }
    }
}
