using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriptografiaAES
{
    public partial class Form1 : Form
    {
        private String caminhoOrigem;
        private String caminhoDestino;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrigem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivo de Texto|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                caminhoOrigem = openFileDialog1.FileName;
               // MessageBox.Show(caminhoOrigem);

            }


        }

        private void btnDestino_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivo de Texto|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                caminhoDestino = openFileDialog1.FileName;
               // MessageBox.Show(caminhoDestino);

            }

        }

        private void btnCriptografar_Click(object sender, EventArgs e)
        {
            Byte ts = 4e;

            if (txtChave.Text.Equals("chave de criptografia") || txtChave.Text.Equals(""))
            {
                MessageBox.Show("Favor insira uma chave válida!");
            }
            else
            {
                KeyMatrix t = new KeyMatrix();
                Console.WriteLine(" ## Construindo matriz de chave");
                byte[,] key =  t.TranformaChave(txtChave.Text);
                Console.WriteLine("## keys" + key);

            }

        }
    }
}
