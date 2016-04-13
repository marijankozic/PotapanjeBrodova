namespace PotapanjeGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numRedaka = new System.Windows.Forms.NumericUpDown();
            this.numStupaca = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFight = new System.Windows.Forms.Button();
            this.groupPostavke = new System.Windows.Forms.GroupBox();
            this.radioPCPC = new System.Windows.Forms.RadioButton();
            this.radioCovjekPC = new System.Windows.Forms.RadioButton();
            this.radioCovjekCovjek = new System.Windows.Forms.RadioButton();
            this.tbDnevnik = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRedaka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStupaca)).BeginInit();
            this.groupPostavke.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Veličina mreže";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "redaka";
            // 
            // numRedaka
            // 
            this.numRedaka.Location = new System.Drawing.Point(18, 43);
            this.numRedaka.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRedaka.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRedaka.Name = "numRedaka";
            this.numRedaka.Size = new System.Drawing.Size(37, 20);
            this.numRedaka.TabIndex = 3;
            this.numRedaka.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numStupaca
            // 
            this.numStupaca.Location = new System.Drawing.Point(18, 66);
            this.numStupaca.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numStupaca.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStupaca.Name = "numStupaca";
            this.numStupaca.Size = new System.Drawing.Size(37, 20);
            this.numStupaca.TabIndex = 5;
            this.numStupaca.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "stupaca";
            // 
            // btnFight
            // 
            this.btnFight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFight.Location = new System.Drawing.Point(267, 101);
            this.btnFight.Name = "btnFight";
            this.btnFight.Size = new System.Drawing.Size(75, 23);
            this.btnFight.TabIndex = 6;
            this.btnFight.Text = "FIGHT";
            this.btnFight.UseVisualStyleBackColor = true;
            // 
            // groupPostavke
            // 
            this.groupPostavke.Controls.Add(this.numericUpDown4);
            this.groupPostavke.Controls.Add(this.label9);
            this.groupPostavke.Controls.Add(this.numericUpDown3);
            this.groupPostavke.Controls.Add(this.label8);
            this.groupPostavke.Controls.Add(this.numericUpDown2);
            this.groupPostavke.Controls.Add(this.label7);
            this.groupPostavke.Controls.Add(this.label6);
            this.groupPostavke.Controls.Add(this.numericUpDown1);
            this.groupPostavke.Controls.Add(this.label5);
            this.groupPostavke.Controls.Add(this.radioCovjekCovjek);
            this.groupPostavke.Controls.Add(this.btnFight);
            this.groupPostavke.Controls.Add(this.radioCovjekPC);
            this.groupPostavke.Controls.Add(this.radioPCPC);
            this.groupPostavke.Controls.Add(this.label1);
            this.groupPostavke.Controls.Add(this.label2);
            this.groupPostavke.Controls.Add(this.numStupaca);
            this.groupPostavke.Controls.Add(this.numRedaka);
            this.groupPostavke.Controls.Add(this.label3);
            this.groupPostavke.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPostavke.Location = new System.Drawing.Point(12, 12);
            this.groupPostavke.Name = "groupPostavke";
            this.groupPostavke.Size = new System.Drawing.Size(386, 139);
            this.groupPostavke.TabIndex = 7;
            this.groupPostavke.TabStop = false;
            this.groupPostavke.Text = "Postavke";
            // 
            // radioPCPC
            // 
            this.radioPCPC.AutoSize = true;
            this.radioPCPC.Location = new System.Drawing.Point(267, 27);
            this.radioPCPC.Name = "radioPCPC";
            this.radioPCPC.Size = new System.Drawing.Size(70, 17);
            this.radioPCPC.TabIndex = 6;
            this.radioPCPC.TabStop = true;
            this.radioPCPC.Text = "PC vs PC";
            this.radioPCPC.UseVisualStyleBackColor = true;
            // 
            // radioCovjekPC
            // 
            this.radioCovjekPC.AutoSize = true;
            this.radioCovjekPC.Location = new System.Drawing.Point(267, 50);
            this.radioCovjekPC.Name = "radioCovjekPC";
            this.radioCovjekPC.Size = new System.Drawing.Size(89, 17);
            this.radioCovjekPC.TabIndex = 7;
            this.radioCovjekPC.TabStop = true;
            this.radioCovjekPC.Text = "Čovjek vs PC";
            this.radioCovjekPC.UseVisualStyleBackColor = true;
            // 
            // radioCovjekCovjek
            // 
            this.radioCovjekCovjek.AutoSize = true;
            this.radioCovjekCovjek.Location = new System.Drawing.Point(267, 73);
            this.radioCovjekCovjek.Name = "radioCovjekCovjek";
            this.radioCovjekCovjek.Size = new System.Drawing.Size(108, 17);
            this.radioCovjekCovjek.TabIndex = 8;
            this.radioCovjekCovjek.TabStop = true;
            this.radioCovjekCovjek.Text = "Čovjek vs Čovjek";
            this.radioCovjekCovjek.UseVisualStyleBackColor = true;
            // 
            // tbDnevnik
            // 
            this.tbDnevnik.Location = new System.Drawing.Point(416, 28);
            this.tbDnevnik.Multiline = true;
            this.tbDnevnik.Name = "tbDnevnik";
            this.tbDnevnik.Size = new System.Drawing.Size(200, 123);
            this.tbDnevnik.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Kapetanov Dnevnik";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 157);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(300, 300);
            this.dataGridView1.TabIndex = 10;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(330, 157);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(300, 300);
            this.dataGridView2.TabIndex = 11;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(140, 36);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(30, 20);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "nosača (4)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Flota";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(140, 56);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(30, 20);
            this.numericUpDown2.TabIndex = 13;
            this.numericUpDown2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(170, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "krstarica (3)";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(140, 76);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(30, 20);
            this.numericUpDown3.TabIndex = 15;
            this.numericUpDown3.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(170, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "razarača (2)";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(140, 97);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(30, 20);
            this.numericUpDown4.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(170, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "splavi (1)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 479);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDnevnik);
            this.Controls.Add(this.groupPostavke);
            this.Name = "MainForm";
            this.Text = "Potapanje Brodova";
            ((System.ComponentModel.ISupportInitialize)(this.numRedaka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStupaca)).EndInit();
            this.groupPostavke.ResumeLayout(false);
            this.groupPostavke.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numRedaka;
        private System.Windows.Forms.NumericUpDown numStupaca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFight;
        private System.Windows.Forms.GroupBox groupPostavke;
        private System.Windows.Forms.RadioButton radioCovjekCovjek;
        private System.Windows.Forms.RadioButton radioCovjekPC;
        private System.Windows.Forms.RadioButton radioPCPC;
        private System.Windows.Forms.TextBox tbDnevnik;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label7;
    }
}

