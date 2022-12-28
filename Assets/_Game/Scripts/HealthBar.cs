using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace HealthBarSystem
{
    public class HealthBar : MonoBehaviour
    {
        /// <summary>
        /// The UI image fill 
        /// </summary>
        [SerializeField] private Image m_FillLine;
        /// <summary>
        /// The UI text value 
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_TextValue;
        /// <summary>
        /// The speed change value
        /// </summary>
        [SerializeField] private float m_LerpSpeed;
        /// <summary>
        /// The min value can reach
        /// </summary>
        [SerializeField] private float m_MinValue;
        /// <summary>
        /// The max value can reach
        /// </summary>
        [SerializeField] private float m_MaxValue;
        /// <summary>
        /// The target value need to reach
        /// </summary>
        [SerializeField] private float m_Value;
        /// <summary>
        /// Defines whether change color on value change
        /// </summary>
        [SerializeField] private bool m_UseGradientColor;
        /// <summary>
        /// The color use when m_UseGradientColor is true
        /// </summary>
        [SerializeField] private Gradient m_Color;
        /// <summary>
        /// The current value
        /// </summary>
        private float m_CurrentValue = -1;
        /// <summary>
        /// The target value need to reach
        /// </summary>
        public float Value { get => m_Value; set => m_Value = value; }
        private void Update()
        {
            HandleHealthBar();
        }
        /// <summary>
        /// Init the health bar
        /// </summary>
        /// <param name="value">start value</param>
        /// <param name="minValue">max value can reach</param>
        /// <param name="maxValue">min value can reach</param>
        public void Init(float value, float minValue, float maxValue)
        {
            m_Value = value;
            m_MinValue = minValue;
            m_MaxValue = maxValue;
            m_CurrentValue = value;

            m_FillLine.fillAmount = Map(m_CurrentValue, m_MinValue, m_MaxValue);
            m_TextValue.text = $"{Mathf.RoundToInt(m_CurrentValue)}/{m_MaxValue}";
            if (m_UseGradientColor)
            {
                m_FillLine.color = m_Color.Evaluate(m_FillLine.fillAmount);
            }
        }
        /// <summary>
        /// Change fill amount, color, text value when value is change 
        /// </summary>
        private void HandleHealthBar()
        {
            if (m_Value == m_CurrentValue) return;
            m_Value = Mathf.Clamp(m_Value, m_MinValue, m_MaxValue);
            m_CurrentValue = Mathf.Lerp(m_CurrentValue, m_Value, m_LerpSpeed * Time.deltaTime);

            m_FillLine.fillAmount = Map(m_CurrentValue, m_MinValue, m_MaxValue);
            m_TextValue.text = $"{Mathf.RoundToInt(m_CurrentValue)}/{m_MaxValue}";
            if (m_UseGradientColor)
            {
                m_FillLine.color = m_Color.Evaluate(m_FillLine.fillAmount);
            }
        }
        /// <summary>
        /// Return fillAmount in range 0-1  
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="minValue">min value can reach</param>
        /// <param name="maxValue">max value can reach</param>
        /// <returns>Return 0 if minValue is equal to maxValue</returns>
        private float Map(float value, float minValue, float maxValue)
        {
            if (minValue == maxValue) return 0;
            return (value - minValue) / (maxValue - minValue);
        }
    }
}

