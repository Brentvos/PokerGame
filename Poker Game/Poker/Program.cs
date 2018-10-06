/*

    - Dawn of the sequel of the prequel of the reboot of the planet of the apes -

© Jackie "Brent" Chan - Tjeard van Zonneveld™ 

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        Database database = new Database();

        public int player1Score = 0;
        public int player2Score = 0;

        public static int index = 0;

        public static int player1FinalScore = 0;
        public static int player2FinalScore = 0;
        public static int draw = 0;

        public string player1Hand; // All cards in player hand
        public string player2Hand; // All cards in player hand

        public static List<string> currentCardsPlayer1; // List of all individual cards
        public static List<string> currentCardsPlayer2; // List of all individual cards

        public static List<Card> player1FinalCards = new List<Card>(); // All cards converted to card class
        public static List<Card> player2FinalCards = new List<Card>(); // All cards converted to card class

        public static List<Card> player1RemainingCards = new List<Card>(); // Cards without the combinations
        public static List<Card> player2RemainingCards = new List<Card>(); // Cards without the combinations

        public static List<Card> Player1Pairs = new List<Card>();
        public static List<Card> Player2Pairs = new List<Card>();

        public CardSet player1; // Result
        public CardSet player2; // Result

        static void Main()
        {

            for (int i = 0; i < 1000; i++)
            {
                Program program = new Program();
                program.GetGame();
                program.GetCardValues(currentCardsPlayer1, player1FinalCards, player1RemainingCards);
                program.GetCardValues(currentCardsPlayer2, player2FinalCards, player2RemainingCards);
                program.PrintSomeStuff();
                program.CheckHighCardResult();
                program.CheckResult();
                Console.WriteLine("________________________________________\n");
                program.ResetData();

                index++;
            }
            PrintResult();
            WriteToDisk.WriteRoundToDisk();
        }

        public void ResetData()
        {
            player1Score = 0;
            player2Score = 0;
            player1Hand = null; // All cards in player hand
            player2Hand = null; // All cards in player hand
            player1FinalCards = new List<Card>();
            player2FinalCards = new List<Card>();
            player1RemainingCards = new List<Card>();
            player2RemainingCards = new List<Card>();
            Player1Pairs = new List<Card>();
            Player2Pairs = new List<Card>();
            player1 = new CardSet(); // Result
            player2 = new CardSet(); // Result
        }

        private void GetGame()
        {
            string game = database.Combinations[index]; // Get random combination


            string[] allCombi = game.Split('|'); // Split hands into player1 and player2 hands

            foreach (string str in allCombi)
            {
                for (int i = 0; i < allCombi.Length; i++)
                {
                    if (!IsOdd(i)) // Split cards and give the right cards to the right player
                    {
                        player1Hand = allCombi[i];
                        currentCardsPlayer1 = player1Hand.Split(' ').ToList(); // Make list with all individual cards for player 1
                    }
                    else
                    {
                        player2Hand = allCombi[i];
                        currentCardsPlayer2 = player2Hand.Split(' ').ToList(); // Make list with all individual cards for player 2
                    }
                }
            }
        }

        private void GetCardValues(List<string> currentCards, List<Card> finalCards, List<Card> remainingCards)
        {
            foreach (string str in currentCards)
            {
                Card newCard = new Card();

                finalCards.Add(newCard);

                if (str.Contains("D"))
                {
                    newCard.type = CardType.DIAMONDS;
                }
                else if (str.Contains("S"))
                {
                    newCard.type = CardType.SPADES;
                }
                else if (str.Contains("H"))
                {
                    newCard.type = CardType.HEARTS;
                }
                else if (str.Contains("C"))
                {
                    newCard.type = CardType.CLUBS;
                }
                if (str.Contains("2"))
                {
                    newCard.value = CardValue.TWO;
                    newCard.intValue = 2;
                }
                else if (str.Contains("3"))
                {
                    newCard.value = CardValue.THREE;
                    newCard.intValue = 3;
                }
                else if (str.Contains("4"))
                {
                    newCard.value = CardValue.FOUR;
                    newCard.intValue = 4;
                }
                else if (str.Contains("5"))
                {
                    newCard.value = CardValue.FIVE;
                    newCard.intValue = 5;
                }
                else if (str.Contains("6"))
                {
                    newCard.value = CardValue.SIX;
                    newCard.intValue = 6;
                }
                else if (str.Contains("7"))
                {
                    newCard.value = CardValue.SEVEN;
                    newCard.intValue = 7;
                }
                else if (str.Contains("8"))
                {
                    newCard.value = CardValue.EIGHT;
                    newCard.intValue = 8;
                }
                else if (str.Contains("9"))
                {
                    newCard.value = CardValue.NINE;
                    newCard.intValue = 9;
                }
                else if (str.Contains("T"))
                {
                    newCard.value = CardValue.TEN;
                    newCard.intValue = 10;
                }
                else if (str.Contains("Q"))
                {
                    newCard.value = CardValue.QUEEN;
                    newCard.intValue = 12;
                }
                else if (str.Contains("K"))
                {
                    newCard.value = CardValue.KING;
                    newCard.intValue = 13;
                }
                else if (str.Contains("J"))
                {
                    newCard.value = CardValue.JACK;
                    newCard.intValue = 11;
                }
                else if (str.Contains("A"))
                {
                    newCard.value = CardValue.ACE;
                    newCard.intValue = 14;
                }
            }

            foreach (Card everyCard in finalCards) // Copy all cards to remaining cards
            {
                remainingCards.Add(everyCard);
            }

            player1 = GetPlayer1Set();
            player2 = GetPlayer2Set();

            AssignPoints();
        }

        public void PrintSomeStuff()
        {
            //  Console.WriteLine("Player 1: " + player1Hand + "\nPlayer 2: " + player2Hand);

            //if (player1 == CardSet.HIGH_CARD && player1 == player2)
            //{
            //    Console.WriteLine("HighestcardP1: " + CombiCheck.CheckHighestCard(player1FinalCards));
            //    Console.WriteLine("HighestcardP2: " + CombiCheck.CheckHighestCard(player2FinalCards));
            //}

            //Console.WriteLine("Round: " + (index + 1));
            foreach (Card card in Player1Pairs)
            {
                Console.WriteLine("Player1: " + card.type + " " + card.value);
            }

            foreach (Card card in Player2Pairs)
            {
                Console.WriteLine("Player2: " + card.type + " " + card.value);
            }

            Console.WriteLine("\nPlayerComboP1: " + player1);
            Console.WriteLine("PlayerComboP2: " + player2 + "\n");
        }

        public void Printresult()
        {
            Console.WriteLine("Player1Final Score: " + player1FinalScore);
            Console.WriteLine("Player2Final Score: " + player2FinalScore);
            Console.WriteLine("Draw Score: " + draw);
        }

        public CardSet GetPlayer1Set()
        {
            int value;

            if (CombiCheck.ThreeOfAKind(out value, player1RemainingCards))
            {
                return CardSet.THREE_OF_A_KIND;
            }
            else if (CombiCheck.CheckOnePair(out value, player1FinalCards, player1RemainingCards, Player1Pairs))
            {
                if (CombiCheck.CheckTwoPair(out value, player1RemainingCards, Player1Pairs))
                {
                    return CardSet.TWO_PAIR;
                }
                return CardSet.ONE_PAIR;
            }
            else
            {
                return CardSet.HIGH_CARD;
            }
        }

        public CardSet GetPlayer2Set()
        {
            int value;


            if (CombiCheck.ThreeOfAKind(out value, player2RemainingCards))
            {
                return CardSet.THREE_OF_A_KIND;
            }
            else if (CombiCheck.CheckOnePair(out value, player2FinalCards, player2RemainingCards, Player2Pairs))
            {
                if (CombiCheck.CheckTwoPair(out value, player2RemainingCards, Player2Pairs))
                {
                    return CardSet.TWO_PAIR;
                }
                return CardSet.ONE_PAIR;
            }
            else
            {
                return CardSet.HIGH_CARD;
            }
        }

        public void CheckHighCardResult()
        {
            if (player1 == CardSet.HIGH_CARD && player2 == CardSet.HIGH_CARD)
            {
                if (CombiCheck.CheckHighestCard(player1FinalCards) > CombiCheck.CheckHighestCard(player2FinalCards))
                {
                    Console.WriteLine("Player1 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER1);
                    player1FinalScore++;
                }
                else if (CombiCheck.CheckHighestCard(player1FinalCards) < CombiCheck.CheckHighestCard(player2FinalCards))
                {
                    Console.WriteLine("Player2 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER2);
                    player2FinalScore++;
                }
                else
                {
                    Console.WriteLine("Draw round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.TIE);
                    draw++;
                }
            }
            else if (player1 == CardSet.ONE_PAIR && player2 == CardSet.ONE_PAIR)
            {
                if (CombiCheck.CheckHighestCard(Player1Pairs) > CombiCheck.CheckHighestCard(Player2Pairs))
                {
                    Console.WriteLine("Player1 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER1);
                    player1FinalScore++;
                }
                else if (CombiCheck.CheckHighestCard(Player1Pairs) < CombiCheck.CheckHighestCard(Player2Pairs))
                {
                    Console.WriteLine("Player2 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER2);
                    player2FinalScore++;
                }
                else
                {
                    Console.WriteLine("Draw round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.TIE);
                    draw++;
                }
            }
            else if (player1 == CardSet.TWO_PAIR && player2 == CardSet.TWO_PAIR)
            {
                if (CombiCheck.CheckHighestCard(Player1Pairs) > CombiCheck.CheckHighestCard(Player2Pairs))
                {
                    Console.WriteLine("Player1 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER1);
                    player1FinalScore++;
                }
                else if (CombiCheck.CheckHighestCard(Player1Pairs) < CombiCheck.CheckHighestCard(Player2Pairs))
                {
                    Console.WriteLine("Player2 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER2);
                    player2FinalScore++;
                }
                else
                {
                    Console.WriteLine("Draw round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.TIE);
                    draw++;
                }
            }
            else if (player1 == CardSet.THREE_OF_A_KIND && player2 == CardSet.THREE_OF_A_KIND)
            {
                if (CombiCheck.CheckHighestCard(Player1Pairs) > CombiCheck.CheckHighestCard(Player2Pairs))
                {
                    Console.WriteLine("Player1 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER1);
                    player1FinalScore++;
                }
                else if (CombiCheck.CheckHighestCard(Player1Pairs) < CombiCheck.CheckHighestCard(Player2Pairs))
                {
                    Console.WriteLine("Player2 won round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER2);
                    player2FinalScore++;
                }
                else
                {
                    Console.WriteLine("Draw round " + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.TIE);
                    draw++;
                }
            }
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public void PrintDebug(string input)
        {
            Console.Write("\n\n" + input + "\n\n");
        }

        private void AssignPoints()
        {
            switch (player1)
            {
                case CardSet.THREE_OF_A_KIND:
                    player1Score = 3;
                    break;
                case CardSet.TWO_PAIR:
                    player1Score = 2;
                    break;
                case CardSet.ONE_PAIR:
                    player1Score = 1;
                    break;
                case CardSet.HIGH_CARD:
                    player1Score = 0;
                    break;
                default:
                    break;
            }

            switch (player2)
            {
                case CardSet.THREE_OF_A_KIND:
                    player2Score = 3;
                    break;
                case CardSet.TWO_PAIR:
                    player2Score = 2;
                    break;
                case CardSet.ONE_PAIR:
                    player2Score = 1;
                    break;
                case CardSet.HIGH_CARD:
                    player2Score = 0;
                    break;
                default:
                    break;
            }
        }

        #region results
        public static void PrintResult()
        {
            Console.WriteLine("Player1Final Score: " + player1FinalScore);
            Console.WriteLine("Player2Final Score: " + player2FinalScore);
            Console.WriteLine("Draw Score: " + draw);
        }
        #endregion

        private void CheckResult()
        {
            if (player1 != player2)
            {
                if (player1Score > player2Score)
                {
                    Console.WriteLine("Player1 won round" + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER1);
                    player1FinalScore++;
                }

                else if (player1Score < player2Score)
                {
                    Console.WriteLine("Player2 won round" + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.PLAYER2);
                    player2FinalScore++;
                }

                else if (player1Score == player2Score)
                {
                    Console.WriteLine("Draw round" + (index + 1));
                    WriteToDisk.AddOutput(index, WinState.TIE);
                    draw++;
                }
            }
        }
    }

    public enum CardSet
    {
        ROYAL_FLUSH,
        STRAIGHT_FLUSH,
        FOUR_OF_A_KIND,
        FULL_HOUSE,
        FLUSH,
        STRAIGHT,
        THREE_OF_A_KIND,
        TWO_PAIR,
        ONE_PAIR,
        HIGH_CARD
    }

    public class Card
    {
        public CardType type;
        public CardValue value;
        public int intValue;
    }

    public enum CardType
    {
        DIAMONDS,
        HEARTS,
        CLUBS,
        SPADES
    }
    public enum CardValue
    {
        TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
    }
}
