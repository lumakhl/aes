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
            this.btnOrigem.Location = new System.Drawing.Point(53, 29);
            this.btnOrigem.Name = "btnOrigem";
            this.btnOrigem.Size = new System.Drawing.Size(143, 70);
            this.btnOrigem.TabIndex = 0;
            this.btnOrigem.Text = "Selecionar arquivo origem";
            this.btnOrigem.UseVisualStyleBackColor = true;
            this.btnOrigem.Click += new System.EventHandler(this.btnOrigem_Click);
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(202, 29);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(132, 70);
            this.btnDestino.TabIndex = 1;
            this.btnDestino.Text = "Selecionar arquivo destino";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // txtChave
            // 
            this.txtChave.Location = new System.Drawing.Point(44, 125);
            this.txtChave.Name = "txtChave";
            this.txtChave.Size = new System.Drawing.Size(317, 26);
            this.txtChave.TabIndex = 2;
            this.txtChave.Text = "chave de criptografia";
            // 
            // btnCriptografar
            // 
            this.btnCriptografar.Location = new System.Drawing.Point(126, 177);
            this.btnCriptografar.Name = "btnCriptografar";
            this.btnCriptografar.Size = new System.Drawing.Size(129, 36);
            this.btnCriptografar.TabIndex = 3;
            this.btnCriptografar.Text = "Criptografar";
            this.btnCriptografar.UseVisualStyleBackColor = true;
            this.btnCriptografar.Click += new System.EventHandler(this.btnCriptografar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 237);
            this.Controls.Add(this.btnCriptografar);
            this.Controls.Add(this.txtChave);
            this.Controls.Add(this.btnDestino);
            this.Controls.Add(this.btnOrigem);
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

