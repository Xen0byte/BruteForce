using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrackerX
{
    class BruteForce
    {
        private static string password;
        private static string result;
        private static long permutations;

        public static ConsoleKeyInfo key;

        private static bool isMatched = false;

        private static int charactersToTestLength = 0;
        private static long computedKeys = 0;

        private static char[] charactersToTest =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0'
    };

        static void Main(string[] args)
        {
            Console.WriteLine("\nPlease enter an alphanumeric password. This program will attempt to crack it.\n");
            while (true)
            {
                do
                {
                    key = Console.ReadKey(true);

                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                while (key.Key != ConsoleKey.Enter);
                Console.WriteLine();

                if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]*$"))
                {
                    Console.WriteLine("Invalid characters detected. Please try again.\n");
                    password = null;
                }
                else
                    break;
            }

            for (int i=1; i<=password.Length; i++)
                {
                permutations += (long)Math.Pow(charactersToTest.Length, i);
                }

            var timeStarted = DateTime.Now;
            Console.WriteLine("\nBrute Force Started - {0}", timeStarted.ToString("dd MMM yyyy, HH:mm:ss.fffffff"));
            Console.WriteLine("Total Permutations: {0}\n", permutations);

            charactersToTestLength = charactersToTest.Length;

            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                startBruteForce(estimatedPasswordLength);
            }

            Console.WriteLine("Password Matched - {0}", DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss.fffffff"));
            Console.WriteLine("Time Passed: {0}", DateTime.Now.Subtract(timeStarted));
            Console.WriteLine("Resolved Password: {0}", result);
            Console.WriteLine("Permutations Computed: {0}", computedKeys);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
        }

        private static void startBruteForce(int keyLength)
        {
            var keyChars = createCharArray(keyLength, charactersToTest[0]);
            var indexOfLastChar = keyLength - 1;
            createNewKey(0, keyChars, keyLength, indexOfLastChar);
        }

        private static char[] createCharArray(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray();
        }

        private static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < charactersToTestLength; i++)
            {
                keyChars[currentCharPosition] = charactersToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar);
                }
                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == password)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }
    }
}