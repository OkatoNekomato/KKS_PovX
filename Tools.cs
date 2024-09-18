using UnityEngine;
using ActionGame.Chara;
namespace KK_PovX
{
    public static class Tools
    {
        /// <summary>
        /// If the user is in free-roam camera and moving.
        /// </summary>
        /// <returns></returns>
        public static bool InCameraFreeRoamMovement()
        {
            return
                Controller.FreeRoamToggled &&
                (
                    Input.GetKey(KK_PovX.FreeRoamAscendKey.Value.MainKey) ||
                    Input.GetKey(KK_PovX.FreeRoamDescendKey.Value.MainKey) ||
                    Input.GetKey(KK_PovX.FreeRoamUpKey.Value.MainKey) ||
                    Input.GetKey(KK_PovX.FreeRoamDownKey.Value.MainKey) ||
                    Input.GetKey(KK_PovX.FreeRoamLeftKey.Value.MainKey) ||
                    Input.GetKey(KK_PovX.FreeRoamRightKey.Value.MainKey)
                );
        }

        public static bool HasPlayerMovement()
        {
            // Найти объект игрока через тег
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogWarning("Player object not found.");
                return false;
            }

            Player player = playerObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogWarning("Player component not found on the player object.");
                return false;
            }

            // Проверить активен ли игрок и можно ли двигаться
            return !player.isActionNow;
        }

        // Find smallest degrees to rotate in order to get to the next angle.
        public static float GetClosestAngle(float from, float to, out bool clockwise)
        {
            float angle = to - from;
            clockwise = (angle >= 0f && angle <= 180f) || angle <= -180f;

            if (angle < 0)
                angle += 360f;

            return clockwise ? angle : 360f - angle;
        }

        // Modulo without negative.
        public static float Mod2(float value, float mod)
        {
            if (value < 0)
                value = mod + (value % mod);

            return value % mod;
        }

        // Restrict angle where origin is at angle 0.
        public static float AngleClamp(float value, float min, float max)
        {
            if (value > min && value < 360f - max)
                return min;
            else if (value < 360f - max && value > min)
                return 360f - max;

            return value;
        }
    }
}
