using Microsoft.AspNetCore.Components;

namespace HighRollerHeroes.Blackjack.Data
{
    public class Graphics
    {
        public static Dictionary<string, ElementReference> sunCards = new Dictionary<string, ElementReference>();
        public static Dictionary<string, ElementReference> moonCards = new Dictionary<string, ElementReference>();
        public static Dictionary<string, ElementReference> Buttons = new Dictionary<string, ElementReference>();
        public static Dictionary<string, ElementReference> Interface = new Dictionary<string, ElementReference>();
        public static Dictionary<string, ElementReference> Backgrounds = new Dictionary<string, ElementReference>();

        public static void AddSunCardToDictionary(string cardName, ElementReference graphic)
        {
            sunCards[cardName] = graphic;
        }

        public static void AddMoonCardToDictionary(string cardName, ElementReference graphic)
        {
            moonCards[cardName] = graphic;
        }

        public static void AddButtonToDictionary(string buttonName, ElementReference graphic)
        {
            Buttons[buttonName] = graphic;
        }

        public static void AddInterfaceToDictionary(string interfaceName, ElementReference graphic)
        {
            Interface[interfaceName] = graphic;
        }

        public static void AddBackgroundToDictionary(string backgroundType, ElementReference graphic)
        {
            Backgrounds[backgroundType] = graphic;
        }

    }
}
