using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriptografiaAES
{
    class KeyMatrix
    {
        public KeyMatrix()
        {

        }

        public byte[,] TranformaChave(String chave)
        {
            string[] chaves = chave.Split(',');

            if(chaves.Length != 16)
            {
                throw new Exception("Tamanho de chave inválido");
            }

            byte[,] key_matriz = new byte[4, 4];

            int i = 0;
            int y = 0;
            while (i <= 3)
            {
                int j = 0;
                for (int cont = y; cont < (y+4); cont++)
                {
                    //key_matriz[i, j] = ASCIIEncoding.ASCII.GetBytes(chaves[cont])[0];

                    key_matriz[i, j] = Byte.Parse(chaves[cont]);
                    Console.WriteLine($"byte da chave: {key_matriz[i, j]}");
                    //Console.WriteLine()

                    j++;
                }

                i++;
                y += 4;
            }
            return key_matriz;
        }
    }
}
