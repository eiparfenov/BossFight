using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class GroundChecker:MonoBehaviour
    {
        public float Grounded { get; private set; }

        public void FixedUpdate()
        {
            var grounded = Physics2D.OverlapCircleAll(transform.position, 0.05f)
                .Any(col => col.CompareTag("Ground"));
            if (grounded)
            {
                Grounded = 0f;
            }
            else
            {
                Grounded += Time.fixedDeltaTime;
            }
        }
    }
}