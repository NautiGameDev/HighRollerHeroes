namespace HighRollerHeroes.Blackjack.Data
{
    public class PlayerInput
    {
        public static float mouseXPos { get; set; } = 0f;
        public static float mouseYPos { get; set; } = 0f;


        public static bool mouseClicked { get; set; } = false;
        public static float mouseXClicked { get; set; } = 0f;
        public static float mouseYClicked { get; set; } = 0f;


        public static Queue<string> buttonsPressed = new Queue<string>();

        public static void SetMouseCoords(float x, float y)
        {
            mouseXPos = x;
            mouseYPos = y;
        }

        public static void SetMouseClicked(float x, float y)
        {
            mouseClicked = true;
            mouseXClicked = x;
            mouseYClicked = y;
        }

        public static void ClearMouseClicked()
        {
            mouseClicked = false;
            mouseXClicked = 0f;
            mouseYClicked = 0f;
        }

        public static void AddActionToQueue(string action)
        {
            if (!buttonsPressed.Contains(action))
            {
                buttonsPressed.Enqueue(action);
            }
            
        }
    }
}
