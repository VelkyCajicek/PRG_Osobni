namespace PaintApp2
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
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.mousePositionLabel = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.utensilComboBox = new System.Windows.Forms.ComboBox();
            this.sizeComboBox = new System.Windows.Forms.ComboBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.Location = new System.Drawing.Point(2, 88);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(1104, 588);
            this.drawingPanel.TabIndex = 0;
            this.drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseDown);
            this.drawingPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseMove);
            this.drawingPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseUp);
            // 
            // mousePositionLabel
            // 
            this.mousePositionLabel.AutoSize = true;
            this.mousePositionLabel.Location = new System.Drawing.Point(1004, 679);
            this.mousePositionLabel.Name = "mousePositionLabel";
            this.mousePositionLabel.Size = new System.Drawing.Size(32, 16);
            this.mousePositionLabel.TabIndex = 1;
            this.mousePositionLabel.Text = "(0,0)";
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(12, 12);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(82, 70);
            this.colorButton.TabIndex = 2;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // utensilComboBox
            // 
            this.utensilComboBox.FormattingEnabled = true;
            this.utensilComboBox.Items.AddRange(new object[] {
            "Pencil",
            "Eraser",
            "Pen"});
            this.utensilComboBox.Location = new System.Drawing.Point(354, 36);
            this.utensilComboBox.Name = "utensilComboBox";
            this.utensilComboBox.Size = new System.Drawing.Size(121, 24);
            this.utensilComboBox.TabIndex = 3;
            this.utensilComboBox.Text = "Pencil";
            // 
            // sizeComboBox
            // 
            this.sizeComboBox.FormattingEnabled = true;
            this.sizeComboBox.Location = new System.Drawing.Point(481, 36);
            this.sizeComboBox.Name = "sizeComboBox";
            this.sizeComboBox.Size = new System.Drawing.Size(121, 24);
            this.sizeComboBox.TabIndex = 4;
            this.sizeComboBox.Text = "2";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(100, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(79, 70);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(185, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(80, 70);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save drawing";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(271, 12);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(77, 70);
            this.importButton.TabIndex = 7;
            this.importButton.Text = "Import image";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 705);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sizeComboBox);
            this.Controls.Add(this.utensilComboBox);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.mousePositionLabel);
            this.Controls.Add(this.drawingPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Label mousePositionLabel;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox utensilComboBox;
        private System.Windows.Forms.ComboBox sizeComboBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button importButton;
    }
}

