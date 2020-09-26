using UnityEngine;
using UnityEngine.UI;

public class SliderWeights : MonoBehaviour
{
    public WeightChangedCallback OnValueChanged;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public virtual void ToggleValue(int value)
    {
        OnValueChanged.Invoke(value, slider.value);
    }
}
