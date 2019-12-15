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
        }

        private void btnOrigem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
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
            if (txtChave.Text.Equals("") || txtChave.Text.Split(',').Length < 16)
            {
                MessageBox.Show("Favor inserir uma chave válida!");
            }
            else
            {
                byte[] message = File.ReadAllBytes(caminhoOrigem);

                KeyExpansion keyExpansion = new KeyExpansion();
                byte[,] stateMatrix = keyExpansion.GenerateStateMatrix(txtChave.Text);

                List<byte[,]> keySchedule = keyExpansion.CreateKeySchedule(stateMatrix);

                AESCypher cypher = new AESCypher(keySchedule);
                List<byte[,]> messageMatrix = cypher.GenerateStateMatrix(message);

                List<byte[,]> encryptedMessage = cypher.EncryptChunks(messageMatrix);
                cypher.PrintEncryptedChunks(encryptedMessage);

                byte[] mess = cypher.SaveEncryptedMessageInFile(caminhoDestino, encryptedMessage);
                Console.WriteLine("\nA mensagem cifrada foi salva no arquivo de destino com sucesso.\n");

                byte[] key = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                cypher.AesDecrypt(mess, key);
            }
        }
        
    }
}
