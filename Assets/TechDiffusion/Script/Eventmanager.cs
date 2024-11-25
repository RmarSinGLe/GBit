using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public UnityEvent onEventTriggered;

    public Player player;
    public Monster monster;

    public UnityEvent unityEvent;
    public UnityEvent<bool> onMonsterActivate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void TriggerHealthMonsterTurnOn(bool isActive)
    {
        if (onMonsterActivate != null)
        {
            onMonsterActivate.Invoke(isActive);
        }
    }

}
