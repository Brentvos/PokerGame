using System;
using System.Collections.Generic;

namespace Poker
{
    public class CombiCheck
    {
        public static int CheckHighestCard(List<Card> cards)
        {
            int maxValue = int.MinValue;

            foreach (Card card in cards)
            {
                if (card.intValue > maxValue) // Update maxValue each time a card has a higher value
                {
                    maxValue = card.intValue;
                }
            }
            return maxValue;
        }

        public static bool CheckOnePair(out int value, List<Card> finalCards, List<Card> remainingCards, List<Card> pair)
        {
            value = 0;

            foreach (Card card in finalCards)
            {
                Card otherCard = finalCards.Find(x => x != card && x.value == card.value); // Get pair

                if (otherCard != null)
                {
                    pair.Add(card);
                    pair.Add(otherCard);
                    remainingCards.Remove(card); // Remove pair from remaining cards
                    remainingCards.Remove(otherCard);

                    value = (int)otherCard.value;
                    return true;
                }
            }
            return false;
        }

        // Same as OnePair but then with remainingCards
        public static bool CheckTwoPair(out int value, List<Card> remainingCards, List<Card> pair)
        {
            value = 0;

            foreach (Card card in remainingCards)
            {
                Card otherCard = remainingCards.Find(x => x != card && x.value == card.value); // Get pair

                if (otherCard != null)
                {
                    pair.Add(card);
                    pair.Add(otherCard);
                    remainingCards.Remove(card);
                    remainingCards.Remove(otherCard);

                    value = (int)otherCard.value;
                    return true;
                }
            }
            return false;
        }

        // Same as OnePair but with an extra card
        public static bool ThreeOfAKind(out int value, List<Card> remainingCards)
        {
            value = 0;

            foreach (Card card in remainingCards)
            {
                Card otherCard = remainingCards.Find(x => x != card && x.value == card.value);

                if (otherCard != null)
                {
                    Card otherCard2 = remainingCards.Find(x => x != card && x != otherCard && x.value == card.value);

                    if (otherCard2 != null)
                    {
                        remainingCards.Remove(card);
                        remainingCards.Remove(otherCard);
                        remainingCards.Remove(otherCard2);

                        value = (int)otherCard.value;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
