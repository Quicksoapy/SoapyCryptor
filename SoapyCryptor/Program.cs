using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace SoapyCryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to encrypt? Press 1. Do you want to decrypt? Press 2.");
            //true = encrypting, false = decrypting
            bool userPick = true;
            List<int> randomNumbers = new List<int>();
            List<char> userCharacters = new List<char>();
            string result = null;
            string ASCII = " \"!#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvxyz{|©}»¯~ž½·¾±º®¼¹¶³µ§²«°ª¬Ÿ¥¨£¦¡¢¤Š™œ›š—–•ŒŽ‹‰€ƒˆ„‡†…";
            while (true)
            {
                var encryptordecrypt = Console.ReadLine().Trim();
                if (encryptordecrypt == "1")
                {
                    userPick = true;
                    break;
                }
                else if(encryptordecrypt == "2")
                {
                    userPick = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Choose either 1 or 2.");
                }
            }

            if (userPick)
            {
                Console.WriteLine("Please enter what you'd like to encrypt:");
                string userInput = Console.ReadLine();
                for (int i = 0; i < userInput.Length; i++)
                {
                    if (!userCharacters.Contains(userInput[i]))
                    {
                        userCharacters.Add(userInput[i]);
                    }
                }

                for (int i = 0; i < userCharacters.Count; i++)
                {
                    int rnd = 0;
                    while (randomNumbers.Contains(rnd))
                    {
                        rnd = RandomNumberGenerator.GetInt32(0, 142);
                    }
                    randomNumbers.Add(rnd);
                    result += ASCII[randomNumbers[i]] + " = " + userCharacters[i] + "\n";
                }
                Console.WriteLine(result);
                File.WriteAllText("key.txt", result);
            }
            else
            {
                Console.WriteLine("Please enter what you'd like to decrypt:");
                string userInput = Console.ReadLine();
                Console.WriteLine("Paste your key here:");
                string inputKey = Console.ReadLine();
            }
        }
    }
}