namespace GameOfLife
{
    partial class GlobalForm
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
            this.WidthLabel = new System.Windows.Forms.Label();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.WidthInput = new System.Windows.Forms.TextBox();
            this.HeightInput = new System.Windows.Forms.TextBox();
            this.NumberOfRandomElementsLabel = new System.Windows.Forms.Label();
            this.RandomElementsNumberInput = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.boardPictureBox = new System.Windows.Forms.PictureBox();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.IntervalInput = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TimesList = new System.Windows.Forms.ListView();
            this.Action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(24, 52);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(35, 13);
            this.WidthLabel.TabIndex = 0;
            this.WidthLabel.Text = "Width";
            this.WidthLabel.UseMnemonic = false;
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(24, 93);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(38, 13);
            this.HeightLabel.TabIndex = 1;
            this.HeightLabel.Text = "Height";
            // 
            // WidthInput
            // 
            this.WidthInput.Location = new System.Drawing.Point(27, 68);
            this.WidthInput.Name = "WidthInput";
            this.WidthInput.Size = new System.Drawing.Size(121, 20);
            this.WidthInput.TabIndex = 2;
            // 
            // HeightInput
            // 
            this.HeightInput.Location = new System.Drawing.Point(27, 109);
            this.HeightInput.Name = "HeightInput";
            this.HeightInput.Size = new System.Drawing.Size(121, 20);
            this.HeightInput.TabIndex = 3;
            // 
            // NumberOfRandomElementsLabel
            // 
            this.NumberOfRandomElementsLabel.AutoSize = true;
            this.NumberOfRandomElementsLabel.Location = new System.Drawing.Point(24, 132);
            this.NumberOfRandomElementsLabel.Name = "NumberOfRandomElementsLabel";
            this.NumberOfRandomElementsLabel.Size = new System.Drawing.Size(139, 13);
            this.NumberOfRandomElementsLabel.TabIndex = 4;
            this.NumberOfRandomElementsLabel.Text = "Number of random elements";
            // 
            // RandomElementsNumberInput
            // 
            this.RandomElementsNumberInput.Location = new System.Drawing.Point(27, 148);
            this.RandomElementsNumberInput.Name = "RandomElementsNumberInput";
            this.RandomElementsNumberInput.Size = new System.Drawing.Size(121, 20);
            this.RandomElementsNumberInput.TabIndex = 5;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(39, 284);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(100, 23);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "Start new";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Enabled = false;
            this.PauseButton.Location = new System.Drawing.Point(39, 328);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(100, 23);
            this.PauseButton.TabIndex = 7;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // boardPictureBox
            // 
            this.boardPictureBox.Location = new System.Drawing.Point(225, 3);
            this.boardPictureBox.Name = "boardPictureBox";
            this.boardPictureBox.Size = new System.Drawing.Size(656, 637);
            this.boardPictureBox.TabIndex = 8;
            this.boardPictureBox.TabStop = false;
            this.boardPictureBox.Click += new System.EventHandler(this.boardPictureBox_Click);
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.Location = new System.Drawing.Point(24, 174);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(64, 13);
            this.IntervalLabel.TabIndex = 10;
            this.IntervalLabel.Text = "Interval (ms)";
            // 
            // IntervalInput
            // 
            this.IntervalInput.Location = new System.Drawing.Point(27, 190);
            this.IntervalInput.Name = "IntervalInput";
            this.IntervalInput.Size = new System.Drawing.Size(122, 20);
            this.IntervalInput.TabIndex = 11;
            this.IntervalInput.Text = "1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(27, 230);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Neighborhood";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Random",
            "Clicks",
            "Circle area",
            "Evenly"});
            this.comboBox2.Location = new System.Drawing.Point(27, 28);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 14;
            this.comboBox2.Text = "Random";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Initial setting";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 257);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Periodical";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TimesList
            // 
            this.TimesList.AutoArrange = false;
            this.TimesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Action,
            this.Time});
            this.TimesList.HideSelection = false;
            this.TimesList.Location = new System.Drawing.Point(12, 366);
            this.TimesList.Name = "TimesList";
            this.TimesList.Scrollable = false;
            this.TimesList.Size = new System.Drawing.Size(160, 265);
            this.TimesList.TabIndex = 18;
            this.TimesList.UseCompatibleStateImageBehavior = false;
            this.TimesList.View = System.Windows.Forms.View.Details;
            // 
            // Action
            // 
            this.Action.Text = "Action";
            this.Action.Width = 80;
            // 
            // Time
            // 
            this.Time.Text = "Time [ms]";
            this.Time.Width = 71;
            // 
            // GlobalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 643);
            this.Controls.Add(this.TimesList);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.IntervalInput);
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.boardPictureBox);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.RandomElementsNumberInput);
            this.Controls.Add(this.NumberOfRandomElementsLabel);
            this.Controls.Add(this.HeightInput);
            this.Controls.Add(this.WidthInput);
            this.Controls.Add(this.HeightLabel);
            this.Controls.Add(this.WidthLabel);
            this.Name = "GlobalForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.TextBox WidthInput;
        private System.Windows.Forms.TextBox HeightInput;
        private System.Windows.Forms.Label NumberOfRandomElementsLabel;
        private System.Windows.Forms.TextBox RandomElementsNumberInput;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.PictureBox boardPictureBox;
        private System.Windows.Forms.Label IntervalLabel;
        private System.Windows.Forms.TextBox IntervalInput;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListView TimesList;
        public System.Windows.Forms.ColumnHeader Action;
        private System.Windows.Forms.ColumnHeader Time;
    }
}

