using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogics : MonoBehaviour
{
    [SerializeField] private Button increment;
    [SerializeField] private Button decrement;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int MaxValue;

    private void Start()
    {
        increment.onClick.AddListener(() =>
        {
            IncrementValue();
        });

        decrement.onClick.AddListener(() =>
        {
            DecrementValue();
        });
    }

    /// <summary>
    /// Increment
    /// </summary>
    /// <param name="MaxValue">Use this parameter to set max value for that input field</param>
    public void IncrementValue()
    {
        int value = int.Parse(inputField.text);
        value += 1;
        inputField.text = value.ToString();
    }

    public void DecrementValue()
    {
        int value = int.Parse(inputField.text);
        if (value > 0)
        {
            value -= 1;
            inputField.text = value.ToString();
        }
    }
}
