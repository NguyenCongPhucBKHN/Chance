using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatTextSystem
{
    public class CombatTextManager : Singleton<CombatTextManager>
    {
        /// <summary>
        /// The main camera transform
        /// </summary>
        private Transform m_MainCameraTransform;
        /// <summary>
        /// The cache main camera transform
        /// </summary>
        public Transform MainCameraTransform { get { return m_MainCameraTransform ??= Camera.main.transform; } }
        /// <summary>
        /// The parent transform
        /// </summary>
        [SerializeField] private Transform m_WorldSpaceCanvas;
        /// <summary>
        /// The UICombatText prefab
        /// </summary>
        [SerializeField] private UICombatText m_UICombatTextPrefab;
        /// <summary>
        /// Spawn UICombatText
        /// </summary>
        /// <param name="position">spawn position</param>
        /// <param name="text">text content</param>
        /// <param name="color">text color</param>
        /// <param name="useScale">is using scale tween</param>
        public void CreateText(Vector3 position, string text, Color color, bool useScale)
        {
            UICombatText uICombatText = Instantiate(m_UICombatTextPrefab, m_WorldSpaceCanvas);
            uICombatText.Setup(position, text, color, useScale);            
        }
    }
}

