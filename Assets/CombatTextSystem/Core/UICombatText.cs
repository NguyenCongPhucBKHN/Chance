using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
namespace CombatTextSystem
{
    public class UICombatText : MonoBehaviour
    {
        /// <summary>
        /// The transform attached to this gameobject
        /// </summary>
        private Transform m_Transform;
        /// <summary>
        /// The cache transform of this gameobject
        /// </summary>
        public Transform Transform { get => m_Transform ??= transform; }
        /// <summary>
        /// The text content
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Content;
        /// <summary>
        /// The offset position when setup
        /// </summary>
        [SerializeField] private Vector3 m_Offset;
        /// <summary>
        /// The lifetime of this gameobject 
        /// </summary>
        [SerializeField] private float m_LifeTime;
        /// <summary>
        /// The offset position y when this gameobject is destroy
        /// </summary>
        [SerializeField] private float m_EndOffsetY;
        /// <summary>
        /// The max value scale when this gameobject useScale on setup
        /// </summary>
        [SerializeField] private float m_ScaleValue;
        /// <summary>
        /// Setup function
        /// </summary>
        /// <param name="position">start spawn position</param>
        /// <param name="text">text content</param>
        /// <param name="color">text color</param>
        /// <param name="useScale">is using scale tween</param>
        public void Setup(Vector3 position, string text, Color color, bool useScale)
        {
            Transform.position = position + m_Offset;
            Transform.rotation = CombatTextManager.Instance.MainCameraTransform.rotation;
            m_Content.text = text;
            m_Content.color = color;

            Transform.DOMoveY(Transform.position.y + m_EndOffsetY, m_LifeTime).SetEase(Ease.OutCirc).OnComplete(SelfDespawn);
            m_Content.DOFade(0, m_LifeTime/2).SetDelay(m_LifeTime/2);
            if (useScale)
            {
                Transform.DOScale(m_ScaleValue, m_LifeTime / 10).SetLoops(2, LoopType.Yoyo);
            }
        }
        /// <summary>
        /// Destroy gameobject
        /// </summary>
        private void SelfDespawn()
        {
            Destroy(gameObject);
        }
       
    }
}

