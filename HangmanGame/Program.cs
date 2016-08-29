using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Hangman
    {
        // Makes IEnumerable of the indexes it picks up
        public static IEnumerable<int> IndexesOfCharInString(string str, char chr)
        {
            return Enumerable.Range(0, str.Length).Where(x => str[x] == chr);
        }

        public static string wordgen()
        {
            var lines = System.IO.File.ReadAllLines("dict.txt");
            var r = new Random();
            var randomLineNumber = r.Next(0, lines.Length - 1);
            var line = lines[randomLineNumber];

            return line.ToLower();
        }

        // Main
        static void Main(string[] args)
        {   
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Hangman! \n \nYou know the rules! You have 10 chances, everything is in lowercase. Good Luck! \n");
            string word = wordgen(); // Word to guess
            int attempt = 0; // # of tries
            char guess = '?';
            string underscores = new String('_', word.Length); // Underscores
            int failcount = 0; // Number of errors

            char[] currentguess = underscores.ToCharArray(); // All Underscores
            char[] answer = word.ToCharArray(); // Solution array

            while (true)
            {
                int length = word.Length;
                ++attempt;
                Console.ResetColor();
                Console.WriteLine("----------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Attempt #{attempt}:\n");

                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (char ch in currentguess)
                {
                    Console.Write($"{ch} ");
                }
                int blanks = currentguess.Count(x => x == '_');

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"({blanks} blanks)\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Number of errors: {failcount}\n");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("What's your guess?");
                while (true)
                {
                    try
                    {
                        guess = Convert.ToChar(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input a valid character please.");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("What's your guess?");
                    }
                }
                // Now we need to check if the guess is in the answer

                if (answer.Contains(guess))
                {
                    var position = IndexesOfCharInString(word, guess);

                    foreach (int index in position)
                    {
                        currentguess[index] = guess;
                    }

                    bool isEqual = Enumerable.SequenceEqual(answer, currentguess);

                    if (isEqual)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nCongratulations! You won in {attempt} moves!");
                        break;
                    }
                }
                else
                {
                    ++failcount;
                }

                if (failcount == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You lost. The word was: {word}");
                    break;
                }
            } // End of while loop
        }
    }
}
