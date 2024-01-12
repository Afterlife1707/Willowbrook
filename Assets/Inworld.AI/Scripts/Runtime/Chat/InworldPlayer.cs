/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld.Runtime;
using Inworld.Sample.UI;
using UnityEngine;
namespace Inworld.Sample
{
    /// <summary>
    ///     This is the class for global text management, by original, it's added in Player Controller.
    ///     And would be called by Keycode.Backquote.
    /// </summary>
    /// 

    public class InworldPlayer : InworldPlayer2D
    {
        #region Inspector Variables
        [SerializeField] InworldCameraController m_CameraController;
        [SerializeField] GameObject m_TriggerCanvas;
        [SerializeField] RecordButton m_RecordButton;
        [SerializeField] RuntimeCanvas m_RTCanvas;
        [SerializeField] Vector3 m_InitPosition;
        [SerializeField] Vector3 m_InitRotation;
        [SerializeField] GameObject HelpText;
        #endregion

        public static bool IsPanelOn = false;
        public static bool canInteract = false;

        #region Public Function
        public void BackToLobby()
        {
            if (!m_RTCanvas)
                return;
            m_GlobalChatCanvas.gameObject.SetActive(false);
            m_CameraController.enabled = true;
            m_RTCanvas.gameObject.SetActive(true);
            m_RTCanvas.BackToLobby();
            Transform trPlayer = transform;
            trPlayer.position = m_InitPosition;
            trPlayer.eulerAngles = m_InitRotation;
        }
        #endregion

        #region Monobehavior Functions
        protected override void Update()
        {
            if (!canInteract)
                return;
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                Cursor.visible = !Cursor.visible;
                Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
                HelpText.SetActive(!HelpText.activeInHierarchy);
                IsPanelOn = !IsPanelOn;
                m_GlobalChatCanvas.SetActive(!m_GlobalChatCanvas.activeSelf);
                if (m_CameraController)
                    m_CameraController.enabled = !m_GlobalChatCanvas.activeSelf;
                if (m_TriggerCanvas)
                    m_TriggerCanvas.SetActive(!m_TriggerCanvas.activeSelf);
                InworldController.Instance.ManualAudioCapture = m_GlobalChatCanvas.activeSelf;
                InworldController.Audio.AutoPush = !m_GlobalChatCanvas.activeSelf;
                if (m_GlobalChatCanvas.activeSelf)
	                InworldController.Instance.EndAudioCapture();
                else
	                InworldController.Instance.StartAudioCapture();
            }
            UpdateSendText();
        }
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "NPC")
            {
                HelpText.SetActive(true);
                canInteract = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "NPC")
            {
                HelpText.SetActive(false);
                canInteract = false;
                InworldController.Instance.EndAudioCapture();
            }
        }
    }
}
