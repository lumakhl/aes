using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CriptografiaAES
{
    class AESCypher
    {

        private List<byte[,]> keySchedule;

        private readonly byte[,] Sbox = new byte[16, 16] {
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

        private readonly byte[,] tableL = new byte[16, 16] {
                   /* 0     1     2     3     4     5     6     7     8     9     10    11    12    13    14    15 */
            /*00*/  {0x00, 0x00, 0x19, 0x01, 0x32, 0x02, 0x1a, 0xc6, 0x4b, 0xc7, 0x1b, 0x68, 0x33, 0xee, 0xdf, 0x03},
            /*01*/  {0x64, 0x04, 0xe0, 0x0e, 0x34, 0x8d, 0x81, 0xef, 0x4c, 0x71, 0x08, 0xc8, 0xf8, 0x69, 0x1c, 0xc1},
            /*02*/  {0x7d, 0xc2, 0x1d, 0xb5, 0xf9, 0xb9, 0x27, 0x6a, 0x4d, 0xe4, 0xa6, 0x72, 0x9a, 0xc9, 0x09, 0x78},
            /*03*/  {0x65, 0x2f, 0x8a, 0x05, 0x21, 0x0f, 0xe1, 0x24, 0x12, 0xf0, 0x82, 0x45, 0x35, 0x93, 0xda, 0x8e},
            /*04*/  {0x96, 0x8f, 0xdb, 0xbd, 0x36, 0xd0, 0xce, 0x94, 0x13, 0x5c, 0xd2, 0xf1, 0x40, 0x46, 0x83, 0x38},
            /*05*/  {0x66, 0xdd, 0xfd, 0x30, 0xbf, 0x06, 0x8b, 0x62, 0xb3, 0x25, 0xe2, 0x98, 0x22, 0x88, 0x91, 0x10},
            /*06*/  {0x7e, 0x6e, 0x48, 0xc3, 0xa3, 0xb6, 0x1e, 0x42, 0x3a, 0x6b, 0x28, 0x54, 0xfa, 0x85, 0x3d, 0xba},
            /*07*/  {0x2b, 0x79, 0x0a, 0x15, 0x9b, 0x9f, 0x5e, 0xca, 0x4e, 0xd4, 0xac, 0xe5, 0xf3, 0x73, 0xa7, 0x57},
            /*08*/  {0xaf, 0x58, 0xa8, 0x50, 0xf4, 0xea, 0xd6, 0x74, 0x4f, 0xae, 0xe9, 0xd5, 0xe7, 0xe6, 0xad, 0xe8},
            /*09*/  {0x2c, 0xd7, 0x75, 0x7a, 0xeb, 0x16, 0x0b, 0xf5, 0x59, 0xcb, 0x5f, 0xb0, 0x9c, 0xa9, 0x51, 0xa0},
            /*10*/  {0x7f, 0x0c, 0xf6, 0x6f, 0x17, 0xc4, 0x49, 0xec, 0xd8, 0x43, 0x1f, 0x2d, 0xa4, 0x76, 0x7b, 0xb7},
            /*11*/  {0xcc, 0xbb, 0x3e, 0x5a, 0xfb, 0x60, 0xb1, 0x86, 0x3b, 0x52, 0xa1, 0x6c, 0xaa, 0x55, 0x29, 0x9d},
            /*12*/  {0x97, 0xb2, 0x87, 0x90, 0x61, 0xbe, 0xdc, 0xfc, 0xbc, 0x95, 0xcf, 0xcd, 0x37, 0x3f, 0x5b, 0xd1},
            /*13*/  {0x53, 0x39, 0x84, 0x3c, 0x41, 0xa2, 0x6d, 0x47, 0x14, 0x2a, 0x9e, 0x5d, 0x56, 0xf2, 0xd3, 0xab},
            /*14*/  {0x44, 0x11, 0x92, 0xd9, 0x23, 0x20, 0x2e, 0x89, 0xb4, 0x7c, 0xb8, 0x26, 0x77, 0x99, 0xe3, 0xa5},
            /*15*/  {0x67, 0x4a, 0xed, 0xde, 0xc5, 0x31, 0xfe, 0x18, 0x0d, 0x63, 0x8c, 0x80, 0xc0, 0xf7, 0x70, 0x07}
        };

        private readonly byte[,] tableE = new byte[16, 16] {
			       /* 0     1     2     3     4     5     6     7     8     9     10    11    12    13    14    15 */
	        /*00*/  {0x01, 0x03, 0x05, 0x0f, 0x11, 0x33, 0x55, 0xff, 0x1a, 0x2e, 0x72, 0x96, 0xa1, 0xf8, 0x13, 0x35},
	        /*01*/  {0x5f, 0xe1, 0x38, 0x48, 0xd8, 0x73, 0x95, 0xa4, 0xf7, 0x02, 0x06, 0x0a, 0x1e, 0x22, 0x66, 0xaa},
	        /*02*/  {0xe5, 0x34, 0x5c, 0xe4, 0x37, 0x59, 0xeb, 0x26, 0x6a, 0xbe, 0xd9, 0x70, 0x90, 0xab, 0xe6, 0x31},
	        /*03*/  {0x53, 0xf5, 0x04, 0x0c, 0x14, 0x3c, 0x44, 0xcc, 0x4f, 0xd1, 0x68, 0xb8, 0xd3, 0x6e, 0xb2, 0xcd},
	        /*04*/  {0x4c, 0xd4, 0x67, 0xa9, 0xe0, 0x3b, 0x4d, 0xd7, 0x62, 0xa6, 0xf1, 0x08, 0x18, 0x28, 0x78, 0x88},
	        /*05*/  {0x83, 0x9e, 0xb9, 0xd0, 0x6b, 0xbd, 0xdc, 0x7f, 0x81, 0x98, 0xb3, 0xce, 0x49, 0xdb, 0x76, 0x9a},
	        /*06*/  {0xb5, 0xc4, 0x57, 0xf9, 0x10, 0x30, 0x50, 0xf0, 0x0b, 0x1d, 0x27, 0x69, 0xbb, 0xd6, 0x61, 0xa3},
	        /*07*/  {0xfe, 0x19, 0x2b, 0x7d, 0x87, 0x92, 0xad, 0xec, 0x2f, 0x71, 0x93, 0xae, 0xe9, 0x20, 0x60, 0xa0},
	        /*08*/  {0xfb, 0x16, 0x3a, 0x4e, 0xd2, 0x6d, 0xb7, 0xc2, 0x5d, 0xe7, 0x32, 0x56, 0xfa, 0x15, 0x3f, 0x41},
	        /*09*/  {0xc3, 0x5e, 0xe2, 0x3d, 0x47, 0xc9, 0x40, 0xc0, 0x5b, 0xed, 0x2c, 0x74, 0x9c, 0xbf, 0xda, 0x75},
	        /*10*/  {0x9f, 0xba, 0xd5, 0x64, 0xac, 0xef, 0x2a, 0x7e, 0x82, 0x9d, 0xbc, 0xdf, 0x7a, 0x8e, 0x89, 0x80},
	        /*11*/  {0x9b, 0xb6, 0xc1, 0x58, 0xe8, 0x23, 0x65, 0xaf, 0xea, 0x25, 0x6f, 0xb1, 0xc8, 0x43, 0xc5, 0x54},
	        /*12*/  {0xfc, 0x1f, 0x21, 0x63, 0xa5, 0xf4, 0x07, 0x09, 0x1b, 0x2d, 0x77, 0x99, 0xb0, 0xcb, 0x46, 0xca},
	        /*13*/  {0x45, 0xcf, 0x4a, 0xde, 0x79, 0x8b, 0x86, 0x91, 0xa8, 0xe3, 0x3e, 0x42, 0xc6, 0x51, 0xf3, 0x0e},
	        /*14*/  {0x12, 0x36, 0x5a, 0xee, 0x29, 0x7b, 0x8d, 0x8c, 0x8f, 0x8a, 0x85, 0x94, 0xa7, 0xf2, 0x0d, 0x17},
	        /*15*/  {0x39, 0x4b, 0xdd, 0x7c, 0x84, 0x97, 0xa2, 0xfd, 0x1c, 0x24, 0x6c, 0xb4, 0xc7, 0x52, 0xf6, 0x01}
        };

        private readonly byte[,] mulMatrix = new byte[4, 4] {
            {2, 3, 1, 1},
            {1, 2, 3, 1},
            {1, 1, 2, 3},
            {3, 1, 1, 2}
        };

        public AESCypher(List<byte[,]> keySchedule)
        {
            this.keySchedule = keySchedule;
        }

        public List<byte[,]> GenerateStateMatrix(byte[] message)
        {
            List<byte[]> chunks = GenerateChunks(message);

            List<byte[,]> listMatrix = new List<byte[,]>();

            foreach(byte[] chunk in chunks)
            {
                byte[,] matrix = ConvertArrayToMatrix(chunk);
                listMatrix.Add(matrix);
            }
            
            return listMatrix;
        }

        private List<byte[]> GenerateChunks(byte[] message)
        {

            byte[] filledMessage = FillMessage(message);

            List<byte[]> chunks = new List<byte[]>();
            int i;
            for (i = 0; i < filledMessage.Length; i += 16)
            {
                byte[] chunk = new byte[16];
                Array.Copy(filledMessage, i, chunk, 0, 16);
                chunks.Add(chunk);
            }
            
            return chunks;
        }

        private byte[] FillMessage(byte[] message)
        {
            int diff = message.Length % 16;

            byte[] filledMessage = new byte[message.Length + 16-diff];

            int i;
            for(i = 0; i < message.Length; i++)
                filledMessage[i] = message[i];

            for (int j = i; j < filledMessage.Length; j++) {
                filledMessage[j] = (byte)(16 - diff);
            }

            return filledMessage;
        }

        private byte[,] ConvertArrayToMatrix(byte[] array)
        {
            byte[,] matrix = new byte[4, 4];
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    matrix[j, i] = array[index++];
            }
            return matrix;
        }

        public List<byte[,]> EncryptChunks(List<byte[,]> messageChunks)
        {
            List<byte[,]> encryptedChunks = new List<byte[,]>();
            foreach (byte[,] chunk in messageChunks)
            {
                byte[,] encryptedChunk = Encrypt(chunk);
                encryptedChunks.Add(encryptedChunk);
            }

            return encryptedChunks;
        }

        public byte[,] Encrypt(byte[,] messageMatrix)
        {
            // first step
            byte[,] A = XorOperation(messageMatrix, keySchedule[0]);
            Console.WriteLine("\n+--- AddRoundKey-Round {0} ---+\n", 0);
            PrintMatrix(A);

            for (int i = 1; i <= 9; i++) {
                // second step
                byte[,] B = SubByte(A);
                Console.WriteLine("\n+--- SubBytes-Round {0} ---+\n", i);
                PrintMatrix(B);

                // third step
                byte[,] C = ShiftRow(B);
                Console.WriteLine("\n+--- ShiftRows-Round {0} ---+\n", i);
                PrintMatrix(C);

                // fourth step
                byte[,] D = MixColumns(C);
                Console.WriteLine("\n+--- MixedColumns-Round {0} ---+\n", i);
                PrintMatrix(D);

                // first step
                A = XorOperation(D, keySchedule[i]);
                Console.WriteLine("\n+--- AddRoundKey-Round {0} ---+\n", i);
                PrintMatrix(A);
            }

            byte[,] B1 = SubByte(A);
            Console.WriteLine("\n+--- SubBytes-Round 10 ---+\n");
            PrintMatrix(B1);

            // third step
            byte[,] C1 = ShiftRow(B1);
            Console.WriteLine("\n+--- ShiftRows-Round 10 ---+\n");
            PrintMatrix(C1);

            byte[,] E = XorOperation(C1, keySchedule[10]);
            Console.WriteLine("\n+--- AddRoundKey-Round 10 ---+\n");
            PrintMatrix(E);

            return E;
        }

        private byte[,] XorOperation(byte[,] first, byte[,] second)
        {
            byte[,] xorArray = new byte[first.GetLength(0), first.GetLength(1)];

            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < first.GetLength(1); j++)
                    xorArray[i,j] = (byte)(first[i,j] ^ second[i,j]);
            }

            return xorArray;
        }

        private byte[,] SubByte(byte[,] matrix)
        {
            const byte mask = 0x0F;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int MSB = (matrix[i, j] >> 4) & mask;
                    int LSB = matrix[i, j] & mask;

                    matrix[i, j] = Sbox[MSB, LSB];
                }
            }

            Console.WriteLine();
            Console.WriteLine("+--- SubByte ---+");
            Console.WriteLine();
            PrintMatrix(matrix);
            return matrix;
        }

        private byte[,] ShiftRow(byte[,] matrix)
        {
            // second line
            byte first = matrix[1, 0];
            matrix[1, 0] = matrix[1, 1];
            matrix[1, 1] = matrix[1, 2];
            matrix[1, 2] = matrix[1, 3];
            matrix[1, 3] = first;

            // third line
            first = matrix[2, 0];
            byte second = matrix[2, 1];
            matrix[2, 0] = matrix[2, 2];
            matrix[2, 1] = matrix[2, 3];
            matrix[2, 2] = first;
            matrix[2, 3] = second;

            // third line
            first = matrix[3, 0];
            second = matrix[3, 1];
            byte third = matrix[3, 2];
            matrix[3, 0] = matrix[3, 3];
            matrix[3, 1] = first;
            matrix[3, 2] = second;
            matrix[3, 3] = third;

            Console.WriteLine();
            Console.WriteLine("+--- ShiftRow ---+");
            Console.WriteLine();
            PrintMatrix(matrix);
            return matrix;
        }

        private byte[,] MixColumns(byte[,] matrix)
        {
            byte[,] mixMatrix = new byte[4,4];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    mixMatrix[i, j] = CalculateBValue(matrix, i, j);
            }

            Console.WriteLine();
            Console.WriteLine("+--- MixColumns ---+");
            Console.WriteLine();
            PrintMatrix(mixMatrix);
            return mixMatrix;
        }

        private byte CalculateBValue(byte[,] matrix, int row, int column)
        {
            const byte mask = 0x0F;
            
            byte[] e = new byte[4];
            for (int k = 0; k < mulMatrix.GetLength(0); k++)
            {
                if(matrix[k, column] == 0 || mulMatrix[row, k] == 0) {
                    e[k] = 0;
                    continue;
                }

                if(matrix[k, column] == 1) {
                    e[k] = mulMatrix[row, k];
                    continue;
                } else if(mulMatrix[row, k] == 1) {
                    e[k] = matrix[k, column];
                    continue;
                }

                int MSB1 = (matrix[k, column] >> 4) & mask;
                int LSB1 = matrix[k, column] & mask;

                int MSB2 = (mulMatrix[row, k] >> 4) & mask;
                int LSB2 = mulMatrix[row, k] & mask;

                int l = tableL[MSB1, LSB1] + tableL[MSB2, LSB2];
                l = (l > 0xFF) ? (l - 0xFF) : l;

                int MSB3 = (l >> 4) & mask;
                int LSB3 = l & mask;

                e[k] = tableE[MSB3, LSB3];
            }
            byte b = (byte)(e[0] ^ e[1] ^ e[2] ^ e[3]);

            return b;
        }

        public void PrintEncryptedChunks(List<byte[,]> chunks)
        {
            for (int i = 0; i < chunks.Count; i++)
            {
                Console.WriteLine("\n+--- Mensagem cifrada {0} ---+\n", i);
                PrintMatrix(chunks[i]);
            }
        }

        public void PrintMatrix(byte[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write("0x" + string.Format("{0:x} \t", matrix[row, col]));
                Console.WriteLine();
            }
        }

        public void PrintArray(byte[] message)
        {
            for (int i = 0; i < message.Length; i++)
                Console.Write("{0}, ", message[i]);
            Console.WriteLine();
        }

        public byte[] SaveEncryptedMessageInFile(string filePath, List<byte[,]> encryptedMessage)
        {
            int size = encryptedMessage.Count * 16;
            byte[] data = new byte[size];
            int index = 0;
            foreach (byte[,] chunk in encryptedMessage)
            {
                for (int i = 0; i < chunk.GetLength(0); i++)
                {
                    for (int j = 0; j < chunk.GetLength(1); j++)
                        data[index++] = chunk[j, i];
                }
            }

            PrintArray(data);
            File.WriteAllBytes(filePath, data);

            return data;
        }

        public string AesDecrypt(byte[] inputBytes, byte[] key)
        {
            byte[] outputBytes = inputBytes;

            string plaintext = string.Empty;

            RijndaelManaged AES = new RijndaelManaged();
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.ECB;
            AES.KeySize = 128;
            AES.BlockSize = 128;

            using (MemoryStream memoryStream = new MemoryStream(outputBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, AES.CreateDecryptor(key, key), CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            Console.WriteLine("Mensagem descriptografada: " + plaintext);
            return plaintext;
        }

    }
}
