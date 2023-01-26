using UnityEngine;

namespace Player
{
    public interface IVelocityEffector
    {
        Vector2 NewVelocity(Vector2 currentVelocity, float deltaTime);
    }
}