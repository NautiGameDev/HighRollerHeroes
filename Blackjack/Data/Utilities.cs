namespace HighRollerHeroes.Blackjack.Data
{
    public class Utilities
    {

        public static string ConvertBetToString(int bet)
        {

            string adjustedBet = "";

            if (bet >= 1000)
            {
                int thousands = (int)bet / 1000;
                int hundreds = (int)(bet % 1000 / 100);

                if (hundreds > 0)
                {
                    adjustedBet = thousands.ToString() + "." + hundreds.ToString() + "k";
                }
                else
                {
                    adjustedBet = thousands.ToString() + "k";
                }
            }
            else
            {
                adjustedBet = bet.ToString();
            }

            return adjustedBet;
        }
    }
}
