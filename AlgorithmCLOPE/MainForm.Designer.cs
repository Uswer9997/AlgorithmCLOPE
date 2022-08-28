namespace AlgorithmCLOPE
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnClusterize = new System.Windows.Forms.Button();
            this.RepulsionTextBox = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.ClustersDataGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTransactions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClustersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(12, 12);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(97, 23);
            this.btnLoadFromFile.TabIndex = 0;
            this.btnLoadFromFile.Text = "Load from file";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnClusterize
            // 
            this.btnClusterize.Location = new System.Drawing.Point(12, 104);
            this.btnClusterize.Name = "btnClusterize";
            this.btnClusterize.Size = new System.Drawing.Size(96, 23);
            this.btnClusterize.TabIndex = 1;
            this.btnClusterize.Text = "Clusterize";
            this.btnClusterize.UseVisualStyleBackColor = true;
            this.btnClusterize.Click += new System.EventHandler(this.btnClusterize_Click);
            // 
            // RepulsionTextBox
            // 
            this.RepulsionTextBox.Location = new System.Drawing.Point(12, 78);
            this.RepulsionTextBox.Name = "RepulsionTextBox";
            this.RepulsionTextBox.Size = new System.Drawing.Size(97, 20);
            this.RepulsionTextBox.TabIndex = 2;
            this.RepulsionTextBox.Validated += new System.EventHandler(this.RepulsionTextBox_Validated);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Коэффициент \r\nотталкивания";
            // 
            // ClustersDataGridView
            // 
            this.ClustersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClustersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClustersDataGridView.Location = new System.Drawing.Point(126, 12);
            this.ClustersDataGridView.Name = "ClustersDataGridView";
            this.ClustersDataGridView.Size = new System.Drawing.Size(307, 272);
            this.ClustersDataGridView.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Обработано\r\nтранзакций:";
            // 
            // lblTransactions
            // 
            this.lblTransactions.AutoSize = true;
            this.lblTransactions.Location = new System.Drawing.Point(12, 164);
            this.lblTransactions.Name = "lblTransactions";
            this.lblTransactions.Size = new System.Drawing.Size(78, 13);
            this.lblTransactions.TabIndex = 6;
            this.lblTransactions.Text = "lblTransactions";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 296);
            this.Controls.Add(this.lblTransactions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClustersDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RepulsionTextBox);
            this.Controls.Add(this.btnClusterize);
            this.Controls.Add(this.btnLoadFromFile);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClustersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnClusterize;
        private System.Windows.Forms.TextBox RepulsionTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ClustersDataGridView;
        private System.Windows.Forms.Label lblTransactions;
        private System.Windows.Forms.Label label2;
    }
}

