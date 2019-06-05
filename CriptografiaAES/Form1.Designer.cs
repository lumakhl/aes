namespace CriptografiaAES
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOrigem = new System.Windows.Forms.Button();
            this.btnDestino = new System.Windows.Forms.Button();
            this.txtChave = new System.Windows.Forms.TextBox();
            this.btnCriptografar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOrigem
            // 
            this.btnOrigem.Location = new System.Drawing.Point(47, 23);
            this.btnOrigem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrigem.Name = "btnOrigem";
            this.btnOrigem.Size = new System.Drawing.Size(127, 56);
            this.btnOrigem.TabIndex = 0;
            this.btnOrigem.Text = "Selecionar arquivo origem";
            this.btnOrigem.UseVisualStyleBackColor = true;
            this.btnOrigem.Click += new System.EventHandler(this.btnOrigem_Click);
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(180, 23);
            this.btnDestino.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(117, 56);
            this.btnDestino.TabIndex = 1;
            this.btnDestino.Text = "Selecionar arquivo destino";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // txtChave
            // 
            this.txtChave.Location = new System.Drawing.Point(39, 100);
            this.txtChave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChave.Name = "txtChave";
            this.txtChave.Size = new System.Drawing.Size(282, 22);
            this.txtChave.TabIndex = 2;
            this.txtChave.Text = "65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80";
            // 
            // btnCriptografar
            // 
            this.btnCriptografar.Location = new System.Drawing.Point(112, 142);
            this.btnCriptografar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCriptografar.Name = "btnCriptografar";
            this.btnCriptografar.Size = new System.Drawing.Size(115, 29);
            this.btnCriptografar.TabIndex = 3;
            this.btnCriptografar.Text = "Criptografar";
            this.btnCriptografar.UseVisualStyleBackColor = true;
            this.btnCriptografar.Click += new System.EventHandler(this.btnCriptografar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 190);
            this.Controls.Add(this.btnCriptografar);
            this.Controls.Add(this.txtChave);
            this.Controls.Add(this.btnDestino);
            this.Controls.Add(this.btnOrigem);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "CriptografiaAES";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrigem;
        private System.Windows.Forms.Button btnDestino;
        private System.Windows.Forms.TextBox txtChave;
        private System.Windows.Forms.Button btnCriptografar;
    }
}

