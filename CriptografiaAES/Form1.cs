using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CriptografiaAES
{
    public partial class Form1 : Form
    {
        private string caminhoOrigem;
        private string caminhoDestino;

        public Form1()
        {
            InitializeComponent();
            /*
            caminhoOrigem = "C:\\temp\\message_default.txt";
            caminhoDestino = "C:\\temp\\message_encrypted.txt";

            KeyMatrix keyMatrix = new KeyMatrix();

            byte[,] stateMatrix = keyMatrix.GenerateStateMatrix("65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80");


            List<byte[,]> keySchedule = keyMatrix.CreateKeySchedule(stateMatrix);

            AESCypher cypher = new AESCypher(keySchedule);
            byte[,] messageMatrix = cypher.GenerateStateMatrix("DESENVOLVIMENTO!");

            byte[,] encryptedMessage = cypher.Encrypt(messageMatrix);
            Console.WriteLine("\n+--- Mensagem cifrada ---+\n");
            cypher.PrintMatrix(encryptedMessage);
            */
        }

        private void btnOrigem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivo de Texto|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                caminhoOrigem = openFileDialog1.FileName;
            }

        }

        private void btnDestino_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivo de Texto|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                caminhoDestino = openFileDialog1.FileName;
            }

        }

        private void btnCriptografar_Click(object sender, EventArgs e)
        {
            if (txtChave.Text.Equals(""))
            {
                MessageBox.Show("Favor inserir uma chave válida!");
            }
            else
            {
                byte[] message = File.ReadAllBytes(caminhoOrigem);

                KeyMatrix keyMatrix = new KeyMatrix();
                byte[,] stateMatrix = keyMatrix.GenerateStateMatrix(txtChave.Text);

                List<byte[,]> keySchedule = keyMatrix.CreateKeySchedule(stateMatrix);

                AESCypher cypher = new AESCypher(keySchedule);
                byte[,] messageMatrix = cypher.GenerateStateMatrix(message);

                byte[,] encryptedMessage = cypher.Encrypt(messageMatrix);
                Console.WriteLine("\n+--- Mensagem cifrada ---+\n");
                cypher.PrintMatrix(encryptedMessage);

                cypher.SaveEncryptedMessageInFile(caminhoDestino, encryptedMessage);
                Console.WriteLine("\nA mensagem cifrada foi salva no arquivo de destino com sucesso.\n");
            }
        }
    }
}
