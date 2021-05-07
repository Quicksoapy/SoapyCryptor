using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using MiscUtil.IO;
using static System.String;

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
            string ASCII = "\"!#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvxyz{|©}»¯~ž½·¾±º®¼¹¶³µ§²«°ª¬Ÿ¥¨£¦¡¢¤Š™œ›š—–•ŒŽ‹‰€ƒˆ„‡†…";
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
                List<string> result = new List<string>();
                for (int i = 0; i < userCharacters.Count; i++)
                {
                    int rnd = 0;
                    while (randomNumbers.Contains(rnd))
                    {
                        rnd = RandomNumberGenerator.GetInt32(0, 142);
                    }
                    randomNumbers.Add(rnd);
                    result.Add(ASCII[randomNumbers[i]] + " = " + userCharacters[i]);
                }

                string writtenResult = null;
                for (int i = 0; i < result.Count; i++)
                {
                    writtenResult += result[i] + "\n";
                    
                }
                File.WriteAllText("key.txt", writtenResult);
                string output = userInput;
                for (int j = 0; j < userCharacters.Count; j++)
                {
                    output = output.Replace(result[j].Split(" = ")[1], result[j].Split(" = ")[0]);
                }
                Console.WriteLine("The encrypted text: \n" + output);
            }
            else
            {
                Console.WriteLine("Please enter what you'd like to decrypt:");
                string userInput = Console.ReadLine();
                Console.WriteLine("Paste the address of your key here:");
                string textKey = Console.ReadLine();
                string inputKey = File.ReadAllText(textKey);
                List<string> keyChars = new List<string>();
                List<string> messageChars = new List<string>();
                foreach (string line in new LineReader(() => new StringReader(inputKey)))
                {
                    string keyChar = line.Split(" = ")[0];
                    string messageChar = line.Split(" = ")[1];
                    keyChars.Add(keyChar);
                    messageChars.Add(messageChar);
                }

                string output = userInput;
                for (int j = 0; j < keyChars.Count; j++)
                {
                    output = output.Replace(keyChars[j], messageChars[j]);
                }

                int messNmbr = 0;
                foreach (var thing in output)
                {
                    if (keyChars.Contains(thing.ToString()))
                    {
                        output[messNmbr] = messageChars[messNmbr];
                        messNmbr += 1;
                    }
                }
                Console.WriteLine(output);
            }
            Console.WriteLine("\nPress any key to close:");
            Console.ReadLine();
        }
    }
}