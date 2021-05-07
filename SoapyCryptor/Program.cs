using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
                Dictionary<char, int> dictionary = new Dictionary<char, int>();
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
                        rnd = RandomNumberGenerator.GetInt32(0, 141);
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
                var sb = new StringBuilder();
                foreach (var c in userInput.ToCharArray())
                {
                    string encodedCharacter = Empty;
                    foreach (var s in result)
                    {
                        if (s.Split(" = ")[1] == c.ToString())
                        {
                            encodedCharacter = s.Split(" = ")[0];
                        }
                    }
                    sb.Append(encodedCharacter);
                }
                output = sb.ToString();
                Console.WriteLine("The encrypted text: \n" + output);
                File.WriteAllText("encrypted.txt",output);
            }
            else
            {
                Console.WriteLine("Please enter what you'd like to decrypt:");
                string userInputReadLine = Console.ReadLine();
                string userInput = File.ReadAllText(userInputReadLine);
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

                string output = null;
                int messNmbr = 0;
                foreach (var thing in userInput)
                {
                    for (int i = 0; i < keyChars.Count; i++)
                    {
                        if (keyChars[i] == thing.ToString())
                        {
                            output += messageChars[i];
                        }
                    }
                }
                Console.WriteLine(output);
            }
            Console.WriteLine("\nPress any key to close:");
            Console.ReadLine();
        }
    }
}