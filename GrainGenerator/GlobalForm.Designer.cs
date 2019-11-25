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
            this.NeighborhoodType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InitialSetting = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxPeriodical = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.newRandomInput = new System.Windows.Forms.TextBox();
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
            this.NumberOfRandomElementsLabel.Size = new System.Drawing.Size(171, 13);
            this.NumberOfRandomElementsLabel.TabIndex = 4;
            this.NumberOfRandomElementsLabel.Text = "Random elements/Evenly values/r";
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
            // NeighborhoodType
            // 
            this.NeighborhoodType.FormattingEnabled = true;
            this.NeighborhoodType.Items.AddRange(new object[] {
            "Moore",
            "Von Neumann",
            "Hexagonal left",
            "Hexagonal right",
            "Hexagonal random",
            "Pentagonal random",
            "Tetragonal random",
            "Triagonal random",
            "Diagonal random",
            "Monogonal random"});
            this.NeighborhoodType.Location = new System.Drawing.Point(27, 230);
            this.NeighborhoodType.Name = "NeighborhoodType";
            this.NeighborhoodType.Size = new System.Drawing.Size(121, 21);
            this.NeighborhoodType.TabIndex = 12;
            this.NeighborhoodType.Text = "Moore";
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
            // InitialSetting
            // 
            this.InitialSetting.FormattingEnabled = true;
            this.InitialSetting.Items.AddRange(new object[] {
            "Random",
            "Clicks",
            "Circle area",
            "Evenly"});
            this.InitialSetting.Location = new System.Drawing.Point(27, 28);
            this.InitialSetting.Name = "InitialSetting";
            this.InitialSetting.Size = new System.Drawing.Size(121, 21);
            this.InitialSetting.TabIndex = 14;
            this.InitialSetting.Text = "Random";
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
            // checkBoxPeriodical
            // 
            this.checkBoxPeriodical.AutoSize = true;
            this.checkBoxPeriodical.Location = new System.Drawing.Point(26, 257);
            this.checkBoxPeriodical.Name = "checkBoxPeriodical";
            this.checkBoxPeriodical.Size = new System.Drawing.Size(72, 17);
            this.checkBoxPeriodical.TabIndex = 17;
            this.checkBoxPeriodical.Text = "Periodical";
            this.checkBoxPeriodical.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxPeriodical.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 37);
            this.button1.TabIndex = 19;
            this.button1.Text = "Draw board";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(154, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 41);
            this.button2.TabIndex = 20;
            this.button2.Text = "Reset clicks";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 374);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 50);
            this.button3.TabIndex = 21;
            this.button3.Text = "Random values";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // newRandomInput
            // 
            this.newRandomInput.Location = new System.Drawing.Point(39, 430);
            this.newRandomInput.Name = "newRandomInput";
            this.newRandomInput.Size = new System.Drawing.Size(100, 20);
            this.newRandomInput.TabIndex = 22;
            // 
            // GlobalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 643);
            this.Controls.Add(this.newRandomInput);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxPeriodical);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InitialSetting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NeighborhoodType);
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
        private System.Windows.Forms.ComboBox NeighborhoodType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox InitialSetting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxPeriodical;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox newRandomInput;
    }
}

