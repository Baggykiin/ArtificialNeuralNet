namespace HandwritingRecognition
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.epochCount = new System.Windows.Forms.NumericUpDown();
			this.learningRate = new System.Windows.Forms.NumericUpDown();
			this.btnTrain = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.miniBatchSize = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.resultLabel = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.epochCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.learningRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.miniBatchSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(613, 811);
			this.textBox1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.epochCount);
			this.groupBox1.Controls.Add(this.learningRate);
			this.groupBox1.Controls.Add(this.btnTrain);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.miniBatchSize);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(637, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(295, 204);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Training";
			// 
			// epochCount
			// 
			this.epochCount.Location = new System.Drawing.Point(163, 23);
			this.epochCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.epochCount.Name = "epochCount";
			this.epochCount.Size = new System.Drawing.Size(120, 29);
			this.epochCount.TabIndex = 9;
			this.epochCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// learningRate
			// 
			this.learningRate.DecimalPlaces = 2;
			this.learningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.learningRate.Location = new System.Drawing.Point(163, 97);
			this.learningRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.learningRate.Name = "learningRate";
			this.learningRate.Size = new System.Drawing.Size(120, 29);
			this.learningRate.TabIndex = 8;
			this.learningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnTrain
			// 
			this.btnTrain.Enabled = false;
			this.btnTrain.Location = new System.Drawing.Point(179, 147);
			this.btnTrain.Name = "btnTrain";
			this.btnTrain.Size = new System.Drawing.Size(104, 40);
			this.btnTrain.TabIndex = 7;
			this.btnTrain.Text = "Train";
			this.btnTrain.UseVisualStyleBackColor = true;
			this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 99);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(156, 25);
			this.label4.TabIndex = 5;
			this.label4.Text = "Learning rate (η)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// miniBatchSize
			// 
			this.miniBatchSize.Location = new System.Drawing.Point(163, 62);
			this.miniBatchSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.miniBatchSize.Name = "miniBatchSize";
			this.miniBatchSize.Size = new System.Drawing.Size(120, 29);
			this.miniBatchSize.TabIndex = 4;
			this.miniBatchSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(143, 25);
			this.label3.TabIndex = 2;
			this.label3.Text = "Mini-batch size";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121, 25);
			this.label2.TabIndex = 0;
			this.label2.Text = "Epoch count";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(6, 28);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(396, 255);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 289);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(141, 40);
			this.button1.TabIndex = 10;
			this.button1.Text = "Load image";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.resultLabel);
			this.groupBox2.Controls.Add(this.pictureBox1);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Location = new System.Drawing.Point(938, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 472);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Run network";
			// 
			// resultLabel
			// 
			this.resultLabel.AutoSize = true;
			this.resultLabel.Location = new System.Drawing.Point(7, 339);
			this.resultLabel.Name = "resultLabel";
			this.resultLabel.Size = new System.Drawing.Size(0, 25);
			this.resultLabel.TabIndex = 11;
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(637, 222);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(141, 40);
			this.btnImport.TabIndex = 11;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(791, 222);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(141, 40);
			this.btnExport.TabIndex = 12;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1479, 841);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.epochCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.learningRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.miniBatchSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown miniBatchSize;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnTrain;
		private System.Windows.Forms.NumericUpDown learningRate;
		private System.Windows.Forms.NumericUpDown epochCount;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label resultLabel;
	}
}

