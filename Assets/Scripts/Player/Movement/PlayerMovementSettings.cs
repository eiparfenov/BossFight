using UnityEngine;

namespace Player.Movement
{
    [CreateAssetMenu(fileName = nameof(PlayerMovementSettings), menuName = "BossFight/Settings/" + nameof(PlayerMovementSettings), order = 0)]
    public class PlayerMovementSettings : ScriptableObject
    {
        [field: SerializeField] 
        [Tooltip("Максимальная скорость в юнитах в секунду")]
        public float MaxSpeed { get; private set; }
        
        [field: SerializeField] 
        [Tooltip("Время ускорения с нуля до максимальной скорости в секундах")]
        public float AccelerationTime { get; private set; }
        
        [field: SerializeField] 
        [Tooltip("Время остановки с максимальной скорости до нуля при отпущенных клавишах движения в секундах")]
        public float DecelerationTimeOnStop { get; private set; }
        
        [field: SerializeField] 
        [Tooltip("Время остановки с максимальной скорости до нуля при нажатой клавише движения в противоположную сторону в секундах")]
        public float DecelerationTimeOnTurn { get; private set; }

        
    }
}