using UnityEngine;

namespace SO.Event
{
    [CreateAssetMenu(menuName = "ScriptableObject/Events/Create InitializeConsumablesEvent")]
    public class InitializeConsumablesEvent : GameEvent<SO.Consumables.HeldConsumables>
    {
        
    }
}
