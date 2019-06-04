﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CriptografiaAES
{
    class KeyMatrix
    {

        private List<byte[,]> keySchedule = new List<byte[,]>();

        private byte[,] Sbox = new byte[16, 16] {
                   /* 0     1     2     3     4     5     6     7     8     9     10    11    12    13    14    15 */
            /*00*/  {0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76},
            /*01*/  {0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0},
            /*02*/  {0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15},
            /*03*/  {0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75},
            /*04*/  {0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84},
            /*05*/  {0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf},
            /*06*/  {0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8},
            /*07*/  {0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2},
            /*08*/  {0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73},
            /*09*/  {0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb},
            /*10*/  {0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79},
            /*11*/  {0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08},
            /*12*/  {0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a},
            /*13*/  {0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e},
            /*14*/  {0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf},
            /*15*/  {0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16}
        };

        private byte[] roundConstantList = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36 };

        public KeyMatrix() { }

        public byte[,] GenerateStateMatrix(string keySeparetedByComma)
        {
            string[] keyBytes = keySeparetedByComma.Split(',');

            byte[,] stateMatrix = new byte[4, 4];

            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    stateMatrix[j, i] = byte.Parse(keyBytes[index++]);
            }

            //PrintMatrix(stateMatrix);
            return stateMatrix;
        }

        public void CreateKeySchedule(byte[,] initialKey)
        {
            keySchedule.Add(initialKey);

            //CreateFirstWord(initialKey);

            while(keySchedule.Count < 11)
            {
                byte[,] newRoundKey = CreateRoundKey(keySchedule.Last());
                keySchedule.Add(newRoundKey);
            }

            PrintKeySchedule(keySchedule);
        }

        private byte[,] CreateRoundKey(byte[,] previousRoundKey)
        {
            int size = previousRoundKey.GetLength(0);
            byte[,] newRoundKey = new byte[size, size];

            byte[] firstNewWord = CreateFirstWord(previousRoundKey);
            byte[] secondNewWord = XorOperation(GetColumnFromMatrix(previousRoundKey, 1), firstNewWord);
            byte[] thirdNewWord  = XorOperation(GetColumnFromMatrix(previousRoundKey, 2), secondNewWord);
            byte[] fourthNewWord = XorOperation(GetColumnFromMatrix(previousRoundKey, 3), thirdNewWord);

            for(int i = 0; i < size; i++)
            {
                newRoundKey[i, 0] = firstNewWord[i];
                newRoundKey[i, 1] = secondNewWord[i];
                newRoundKey[i, 2] = thirdNewWord[i];
                newRoundKey[i, 3] = fourthNewWord[i];
            }

            return newRoundKey;
        }

        public byte[] CreateFirstWord(byte[,] previousRoundKey)
        {
            byte[] lastWord = CopyLastWord(previousRoundKey);
            //PrintWord(lastWord, "1. CopyLastWord");

            RotWord(lastWord);
            //PrintWord(lastWord, "2. RotWord");

            SubWord(lastWord);
            //PrintWord(lastWord, "3. SubWord");

            byte[] roundConstant = GenerateRoundConstant(lastWord, 0);
            //PrintWord(roundConstant, "4. GenerateRoundConstant");

            byte[] xorWord = XorSubWordAndRoundConstant(lastWord, roundConstant);
            //PrintWord(xorWord, "5. XorSubWordAndRoundConstant");

            byte[] firstWord = GetColumnFromMatrix(previousRoundKey, 0);
            byte[] newWord = XorFirstWordWithXorWord(firstWord, xorWord);
            //PrintWord(newWord, "6. XorFirstWordWithXorWord");

            return newWord;
        }

        // first step
        public byte[] CopyLastWord(byte[,] matrix)
        {
            return GetColumnFromMatrix(matrix, matrix.GetLength(1) - 1);
        }

        // second step
        private void RotWord(byte[] word)
        {
            byte first = word[0];
            byte last = word[word.Length - 1];

            word[0] = last;
            word[word.Length - 1] = first;
        }

        // third step
        private void SubWord(byte[] word)
        {
            const byte mask = 0x0F;

            for(int i = 0; i < word.Length; i++)
            {
                int MSB = (word[i] >> 4) & mask;
                int LSB = word[i] & mask;

                word[i] = Sbox[MSB, LSB];
            }
        }

        // fourth step
        private byte[] GenerateRoundConstant(byte[] word, int roundKeyNumber)
        {
            byte[] roundConstant = { 0, 0, 0, 0 };
            roundConstant[0] = roundConstantList[roundKeyNumber];

            return roundConstant;
        }

        // fifth step
        private byte[] XorSubWordAndRoundConstant(byte[] word, byte[] roundConstant)
        {
            return XorOperation(word, roundConstant);
        }

        // sixth step
        private byte[] XorFirstWordWithXorWord(byte[] firstWord, byte[] xorWord)
        {
            return XorOperation(firstWord, xorWord);
        }

        private byte[] XorOperation(byte[] first, byte[] second)
        {
            byte[] xorArray = new byte[first.Length];

            for (int i = 0; i < first.Length; i++)
                xorArray[i] = (byte)(first[i] ^ second[i]);

            return xorArray;
        }

        private byte[] GetColumnFromMatrix(byte[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        private void PrintKeySchedule(List<byte[,]> roundKeyList)
        {
            Console.WriteLine("+--- KeySchedule ---+");
            for(int i = 0; i < roundKeyList.Count; i++)
                PrintMatrix(roundKeyList[i], i);
        }

        private void PrintMatrix(byte[,] matrix, int RoundKeyNumber = -1)
        {
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            Console.WriteLine();
            Console.WriteLine("+--- RoundKey {0} ---+", RoundKeyNumber);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", matrix[row, col]));
                Console.WriteLine();
            }
        }

        private void PrintWord(byte[] word, string title)
        {
            Console.WriteLine();
            Console.WriteLine("+--- {0} ---+", title);
            for (int col = 0; col < word.Length; col++)
                Console.WriteLine(word[col]);
        }
    }
}
