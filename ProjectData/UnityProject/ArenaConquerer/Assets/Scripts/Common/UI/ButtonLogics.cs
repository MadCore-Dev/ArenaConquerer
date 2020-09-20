using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonLogics : MonoBehaviour
{
    [SerializeField] private Button increment;
    [SerializeField] private Button decrement;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int MaxValue;
    public ValueChangedCallback OnValueChanged; 
    private void Start()
    {
        increment.onClick.AddListener(() =>
        {
            OnValueChanged.Invoke(IncrementValue());
        });

        decrement.onClick.AddListener(() =>
        {
            OnValueChanged.Invoke(DecrementValue());
        });
    }

    /// <summary>
    /// Increment
    /// </summary>
    /// <param name="MaxValue">Use this parameter to set max value for that input field</param>
    public int IncrementValue()
    {
        int value = int.Parse(inputField.text);
        value += 1;
        inputField.text = value.ToString();
        return value;
    }

    public int DecrementValue()
    {
        int value = int.Parse(inputField.text);
        if (value > 0)
        {
            value -= 1;
            inputField.text = value.ToString();
        }
        return value;
    }
}