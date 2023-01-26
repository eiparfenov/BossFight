using UnityEngine;

namespace Player.Jumping
{
    [CreateAssetMenu(
        fileName = nameof(PlayerJumpingSettings), 
        menuName = "BossFight/Settings/" + nameof(PlayerJumpingSettings), 
        order = 0)]
    public class PlayerJumpingSettings: ScriptableObject
    {
        [field: SerializeField]
        [field: Tooltip("Высота прыжка в юнитах")]
        [field: Range(2, 5.5f)]
        public float Height { get; set; }
        
        [field: SerializeField]
        [field: Tooltip("Время прыжка в секундах (от начальной точки до максимальной высоты с зажатой кнопкой прыжка)")]
        [field: Range(0.01f, 5)]
        public float Duration { get; set; }
        
        [field: SerializeField]
        [field: Tooltip("Множитель при падении")]
        [field: Range(1, 10)]
        public float DownGravity { get; set; }
        
        [field: SerializeField]
        [field: Tooltip("Множитель при отпускании кнопки прыжка")]
        [field: Range(1, 10)]
        public float JumpCutoff { get; set; }
        
        [field: SerializeField]
        [field: Tooltip("Размер буфера прыжка в секундах")]
        [field: Range(0.01f, 1)]
        public float JumpBuffer { get; set; }
        
        [field: SerializeField]
        [field: Tooltip("\"Время кайота\" в секундах")]
        [field: Range(0, 1)]
        public float CoyoteTime { get; set; }
    }
}