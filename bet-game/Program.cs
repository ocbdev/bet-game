#region Definations

actionTypes selectedAction = actionTypes.deposit;
string entriedAction = "", exceptionMessage = "";
double entriedMoney = 0.0, userBalance = 0;

#endregion

#region Define Static Messages

string welcomeMessage = "Hi, welcome to bet game. \n\r " +
                        "You need choise an action for start the game. \n\r " +
                        "Don't forget if your balence : 0$, you need deposit first. \n\r";

string actionListMessage = "Thats your action list: \n\r " +
                           " -- deposit = for to put money in the your case like deposit [money] \n\r  " +
                           " -- bet = for play game like bet [money]  \n\r  " +
                           " -- withdraw = for to get money from your case withdraw [money] \n\r " +
                           " -- exit = for the exit the game \n\r ";

string balanceMessage = $"Your Balance = {userBalance} $";

string submitMessage = "Please submit your action: ";

#endregion

Console.WriteLine(welcomeMessage);
Console.WriteLine(actionListMessage);
Console.WriteLine(balanceMessage);

while (selectedAction != actionTypes.exit)
{
    Console.Write(submitMessage);
    if (CheckUserInput(Console.ReadLine()))
    {
        switch (selectedAction)
        {
            case actionTypes.deposit:
                userBalance = userBalance + entriedMoney;
                Console.WriteLine("Your deposit of {0}$ was successfull, your current balance : {1}$", entriedMoney, userBalance);
                break;
            case actionTypes.bet:
                if (entriedMoney > userBalance)
                    Console.WriteLine("You cannot bet more money than you have in the case. Your Balance : {0}$", userBalance);
                else
                {
                    Random winRand = new Random();
                    int rate = winRand.Next(100);
                    userBalance = userBalance - entriedMoney;
                    if (rate < 10)
                    {
                        Random randomRate = new Random();
                        int betweenTwoAndTen = randomRate.Next(2, 10);

                        userBalance = userBalance + (entriedMoney * betweenTwoAndTen);
                        Console.WriteLine("Congrats! You win {0}$ , your current balance : {1}$", (entriedMoney * betweenTwoAndTen), userBalance);
                    }
                    else if (rate < 40)
                    {
                        userBalance = userBalance + (entriedMoney * 2);
                        Console.WriteLine("Congrats! You win {0}$ , your current balance : {1}$", (entriedMoney * 2), userBalance);
                    }
                    else

                        Console.WriteLine("No luck now, your current balance : {1}$", entriedMoney, userBalance);
                }

                break;
            case actionTypes.withdraw:
                if (entriedMoney > userBalance)
                    Console.WriteLine("You cannot withdraw more money than you have in the case. Your Balance : {0}$", userBalance);
                else
                {
                    userBalance = userBalance - entriedMoney;
                    Console.WriteLine("Your withdraw of {0}$ was successfull, your current balance : {1}$", entriedMoney, userBalance);
                }

                break;
            default:
                break;
        }
    }
    else
    {
        Console.WriteLine(exceptionMessage);
        Console.WriteLine(actionListMessage);
    }
}

Console.ReadKey();

// taking user input checking for this input entried correctly.
// if user entried an action type saved on enum, and entried correct double format price returning true.
bool CheckUserInput(string userInput)
{
    bool returnValue = false;
    exceptionMessage = "";

    if (String.IsNullOrEmpty(userInput))
        exceptionMessage = "you can not pass null";
    else if (userInput == "exit")
    {
        Console.WriteLine("Thanks for playing.");
        Console.ReadKey();
    }
    else
    {
        string[] inputSplitted = userInput.Split(' ');
        entriedAction = inputSplitted[0];

        #region Check action is true or not

        if (Enum.IsDefined(typeof(actionTypes), entriedAction))
            selectedAction = Enum.Parse<actionTypes>(entriedAction);
        else
            exceptionMessage = "entried action not have any permission on system";

        #endregion

        #region Check money is double or not

        if (!double.TryParse(inputSplitted[1], out double money))
            exceptionMessage = "you need entry an money type like 10 or 10.30 etc";
        else
            entriedMoney = double.Parse(inputSplitted[1]);

        #endregion
    }

    if (exceptionMessage.Length <= 0)
        returnValue = true;

    return returnValue;
}

enum actionTypes
{
    exit,
    deposit,
    bet,
    withdraw
}