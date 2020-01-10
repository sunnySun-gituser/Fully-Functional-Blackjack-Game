using System;

namespace Blackjack
{
    /*
    This algorithm can be translated into the Main() routine

        Let money = 100
        while (true):
            do {
                Ask the user to enter a input
                Let input = the user's response
            } while input is < 0 or > money
            if input is 0:
                Break out of the loop
            Let userWins = PlayBlackjack()
            if userWins:
                Pay off the user's input (money = money + input)
            else
                Collect the user's input (money = money - input)
            If money == 0:
                Break out of the loop.


    The PlayBlackjack function:

        Create and shuffle a deck of cards
        Create two BlackjackHands, userHand and dealerHand
        Deal two cards into each hand
        if dealer has blackjack
            User loses and the game ends now
        If user has blackjack
            User wins and the game ends now
        Repeat:
            Display all the cards in the user's hand
            Display the user's total
            Display the dealers face-up card, i.e. dealerHand.getCard(0)
            Ask if user wants to hit or stand
            Get user's response, and make sure it's valid

            if user stands:
                break out of loop
            if user hits:
                Give user a card
                if userHand.getBlackjackValue() > 21:
                    User loses and the game ends now
        while dealerHand.getBlackJackValue() <= 16 :
            Give dealer a card
            if dealerHand.getBlackjackValue() > 21:
                User wins and game ends now
        if dealerHand.getBlackjackValue() >= userHand.getBlackjackValue()
            User loses
        else
            User wins
    */

    class Program
    {
        static void Main(string[] args)
        {   
            int money = 100;
            int bet;
            Boolean userWins = false;

            Console.WriteLine("Welcome to the Redwood Casino");
            Console.WriteLine(@"Give me your money!!! Hahahahaha");

            while(true){
                bet = GetBet(money);
                
                if(bet == 0){
                    Console.WriteLine($"No bet. Game Over!!!");
                    break;
                }
                userWins = PlayBlackjack();
                if(userWins){
                    money += bet;
                }else{
                    money -= bet;
                }

                if(money == 0){
                    Console.WriteLine($"No money. Game Over!!!");
                    break;
                }
            }
        }

        static int GetBet(int totalMoney)
        {   
            Console.WriteLine($"\nYou have ${totalMoney}");

            int bet;
            bool betMade = false;

            do
            {
                Console.Write("What is your bet? (Enter 0 to end): ");
                if (Int32.TryParse(Console.ReadLine(), out bet))
                {
                    if(bet >= 0 && bet <= totalMoney)
                    {
                        betMade = true;
                    }
                }
                if (!betMade)
                {
                    Console.WriteLine("Invalid bet");
                }
            } while (!betMade);

            return bet;
        }

        static string GetHitOrStand()
        {   
            string input;
            bool inputMade = false;

            do
            {
                Console.Write("Hit (H/h) or Stand (S/s) ? ");
                input = Console.ReadLine();
                if (input == "H" || input == "S" ||
                    input == "h" || input == "s"){}
                {
                     inputMade = true;
                }
                if (!inputMade)
                {
                    Console.WriteLine("Invalid input");
                }
            } while (!inputMade);

            return input;
        }
        
        static bool PlayBlackjack()
        {
            Deck deck = new Deck();
            deck.Shuffle();  

            //userHand and dealerHand
            BlackjackHand userHand = new BlackjackHand();
            BlackjackHand dealerHand = new BlackjackHand();

            userHand.AddCard(deck.DealCard());
            userHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());

            if(dealerHand.GetBlackjackValue() == 21){
                Console.WriteLine("Dealer's cards are: ");
                dealerHand.Display();
                Console.WriteLine("Your cards are: ");
                userHand.Display();
                Console.WriteLine($"Dealer's total is {dealerHand.GetBlackjackValue()} and your total is {userHand.GetBlackjackValue()}. You lose.");
                return false;
            }
            if(userHand.GetBlackjackValue() == 21){
                Console.WriteLine("Dealer's cards are: ");
                dealerHand.Display();
                Console.WriteLine(" \nYour cards are: ");
                userHand.Display();
                Console.WriteLine($"Dealer's total is {dealerHand.GetBlackjackValue()} and your total is {userHand.GetBlackjackValue()}. You win.");
                return true;
            }

            string hitOrStand;
            while(true){
                Console.WriteLine(" \nYour cards are: ");
                userHand.Display();
                Console.WriteLine($"Your total is {userHand.GetBlackjackValue()}");
                Console.WriteLine("\nDealer is showing: ");
                dealerHand.Display(dealerHand.GetCard(0));
                Console.WriteLine();

                hitOrStand = GetHitOrStand();
                if(hitOrStand == "S" || hitOrStand == "s"){
                    Console.WriteLine("\nUser stands.");
                    break;
                }else{
                    userHand.AddCard(deck.DealCard());
                    if(userHand.GetBlackjackValue() > 21){
                        Console.WriteLine("User lose! Your cards are: ");
                        userHand.Display();
                        Console.WriteLine("Dealer's cards are: ");
                        dealerHand.Display();
                        Console.WriteLine($"Dealer's total is {dealerHand.GetBlackjackValue()} and your total is {userHand.GetBlackjackValue()}. You lose.");

                        return false;
                    }
                }
            }
            Console.WriteLine("Dealer's cards are: ");
            dealerHand.Display();
            Console.WriteLine($"Dealer's total is {dealerHand.GetBlackjackValue()}.");

            while(dealerHand.GetBlackjackValue() <= 16){
                Console.WriteLine("Dealer hits and gets a card: ");
                dealerHand.AddCard(deck.DealCard());
                dealerHand.Display(dealerHand.GetCard(dealerHand.GetCardCount()-1));
                if(dealerHand.GetBlackjackValue() > 21){
                    Console.WriteLine("Dealer busted by going over 21. You win.");
                    return true;
                }
            }
            if(dealerHand.GetBlackjackValue() >= userHand.GetBlackjackValue()){
                Console.WriteLine($"Dealer's total is {dealerHand.GetBlackjackValue()} and your total is {userHand.GetBlackjackValue()}. You lose.");
                return false;
            }else{
                Console.WriteLine($"Dealer's total is {dealerHand.GetBlackjackValue()} and your total is {userHand.GetBlackjackValue()}. You win.");
                return true;
            }
        }
    }
}
