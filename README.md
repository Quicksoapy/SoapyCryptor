# SoapyCryptor 

This encryptor loops through every character in a given text, and whenever it finds a character that isn't in a list, adds it to said list.
When it is done with that, it assigns every character in that list a random character and writes down the character and the assigned character with that in a file (the key).
It then outputs 2 texts, 1 is the encrypted text, which is every character swapped with its respectable assigned character, and a key, which holds which character has been swapped with which.

When you'd like to decrypt, you enter the decrypted text and the key, and it will replace every assigned character with the original character.
