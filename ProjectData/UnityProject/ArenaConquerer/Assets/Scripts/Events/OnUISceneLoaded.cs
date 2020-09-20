using UnityEngine;

public class OnUISceneLoaded : MonoBehaviour
{
    public static OnUISceneLoaded Instance;
    public UISceneLoadedCallback OnUiSceneLoaded;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            OnUiSceneLoaded = OnUISceneLoaded.Instance.OnUiSceneLoaded;
            Instance = this;
        }
        OnUiSceneLoaded.Invoke();
    }
}