using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame
{
    public class PlayerUI : MonoBehaviour
    {

        [Tooltip("UI Text to sidplay Player's Name")]
        [SerializeField] private Text playerNameText;

        [Tooltip("UI Text to sidplay Player's Health")]
        [SerializeField] private Slider playerHealthSlider;

        [Tooltip("Pixel offset from the player target")]
        [SerializeField] private Vector3 screenOffset = new Vector3(0f, 30f,0f);

        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        private PlayerManager target;

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        private void LateUpdate()
        {
            if(targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }
            if(targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }

        private void Update()
        {
            if(playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }
            if(target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }


        public void SetTarget(PlayerManager _target)
        {
            if(_target == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            target = _target;
            if(playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }

            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            CharacterController characterController = _target.GetComponent<CharacterController>();
            if(characterController != null)
            {
                characterControllerHeight = characterController.height;
            }
        }
    }
}
