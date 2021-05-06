using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            string userInput;
            List<int> randomNumbers = null;
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
                userInput = Console.ReadLine();
                for (int i = 0; i < userInput.Length; i++)
                {
                    int rnd = RandomNumberGenerator.GetInt32(0, 142);
                    
                    randomNumbers[i] = rnd;
                    result += ASCII[randomNumbers[i]] + " = " + userInput[i] + "\n";
                }
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Please enter what you'd like to decrypt:");
            }
        }
    }
}