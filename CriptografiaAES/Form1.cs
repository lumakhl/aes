using System;
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
            caminhoOrigem = "C:\\temp\\message_default.txt";
            caminhoDestino = "C:\\temp\\message_encrypted.txt";

            KeyMatrix keyMatrix = new KeyMatrix();
            Console.WriteLine(" ## Construindo matriz de chave");

            byte[,] stateMatrix = keyMatrix.GenerateStateMatrix(txtChave.Text);
            keyMatrix.CreateKeySchedule(stateMatrix);
            
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
            if (txtChave.Text.Equals("chave de criptografia") || txtChave.Text.Equals(""))
            {
                MessageBox.Show("Favor inserir uma chave válida!");
            }
            else
            {
                KeyMatrix t = new KeyMatrix();
                byte[,] key2 = t.GenerateStateMatrix(txtChave.Text);
            }
        }
    }
}
