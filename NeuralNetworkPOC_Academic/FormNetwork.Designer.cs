namespace NeuralNetworkPOC
{
    partial class FormNetwork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNetwork));
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonTrain = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonRunCase = new System.Windows.Forms.Button();
            this.labelSP = new System.Windows.Forms.Label();
            this.textBoxSL = new System.Windows.Forms.TextBox();
            this.textBoxSW = new System.Windows.Forms.TextBox();
            this.labelSW = new System.Windows.Forms.Label();
            this.textBoxPW = new System.Windows.Forms.TextBox();
            this.labelPW = new System.Windows.Forms.Label();
            this.textBoxPL = new System.Windows.Forms.TextBox();
            this.labelPL = new System.Windows.Forms.Label();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxGeneratedSpecies = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Enabled = false;
            this.buttonRun.Location = new System.Drawing.Point(711, 385);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(135, 40);
            this.buttonRun.TabIndex = 6;
            this.buttonRun.Text = "Run TestingSet";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonTrain
            // 
            this.buttonTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTrain.Location = new System.Drawing.Point(711, 343);
            this.buttonTrain.Name = "buttonTrain";
            this.buttonTrain.Size = new System.Drawing.Size(135, 40);
            this.buttonTrain.TabIndex = 5;
            this.buttonTrain.Text = "Train Network";
            this.buttonTrain.UseVisualStyleBackColor = true;
            this.buttonTrain.Click += new System.EventHandler(this.buttonTrain_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(15, 343);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(690, 123);
            this.treeView1.TabIndex = 4;
            // 
            // buttonRunCase
            // 
            this.buttonRunCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRunCase.Enabled = false;
            this.buttonRunCase.Location = new System.Drawing.Point(717, 481);
            this.buttonRunCase.Name = "buttonRunCase";
            this.buttonRunCase.Size = new System.Drawing.Size(117, 40);
            this.buttonRunCase.TabIndex = 8;
            this.buttonRunCase.Text = "Run Case";
            this.buttonRunCase.UseVisualStyleBackColor = true;
            this.buttonRunCase.Click += new System.EventHandler(this.buttonRunCase_Click);
            // 
            // labelSP
            // 
            this.labelSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSP.AutoSize = true;
            this.labelSP.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelSP.Location = new System.Drawing.Point(511, 480);
            this.labelSP.Name = "labelSP";
            this.labelSP.Size = new System.Drawing.Size(79, 13);
            this.labelSP.TabIndex = 9;
            this.labelSP.Text = "Septal Lenght: ";
            // 
            // textBoxSL
            // 
            this.textBoxSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSL.Location = new System.Drawing.Point(596, 477);
            this.textBoxSL.Name = "textBoxSL";
            this.textBoxSL.Size = new System.Drawing.Size(100, 20);
            this.textBoxSL.TabIndex = 10;
            // 
            // textBoxSW
            // 
            this.textBoxSW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSW.Location = new System.Drawing.Point(596, 504);
            this.textBoxSW.Name = "textBoxSW";
            this.textBoxSW.Size = new System.Drawing.Size(100, 20);
            this.textBoxSW.TabIndex = 12;
            // 
            // labelSW
            // 
            this.labelSW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSW.AutoSize = true;
            this.labelSW.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelSW.Location = new System.Drawing.Point(511, 507);
            this.labelSW.Name = "labelSW";
            this.labelSW.Size = new System.Drawing.Size(74, 13);
            this.labelSW.TabIndex = 11;
            this.labelSW.Text = "Septal Width: ";
            // 
            // textBoxPW
            // 
            this.textBoxPW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPW.Location = new System.Drawing.Point(596, 560);
            this.textBoxPW.Name = "textBoxPW";
            this.textBoxPW.Size = new System.Drawing.Size(100, 20);
            this.textBoxPW.TabIndex = 16;
            // 
            // labelPW
            // 
            this.labelPW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPW.AutoSize = true;
            this.labelPW.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelPW.Location = new System.Drawing.Point(511, 563);
            this.labelPW.Name = "labelPW";
            this.labelPW.Size = new System.Drawing.Size(68, 13);
            this.labelPW.TabIndex = 15;
            this.labelPW.Text = "Petal Width: ";
            // 
            // textBoxPL
            // 
            this.textBoxPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPL.Location = new System.Drawing.Point(596, 532);
            this.textBoxPL.Name = "textBoxPL";
            this.textBoxPL.Size = new System.Drawing.Size(100, 20);
            this.textBoxPL.TabIndex = 14;
            // 
            // labelPL
            // 
            this.labelPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPL.AutoSize = true;
            this.labelPL.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelPL.Location = new System.Drawing.Point(511, 535);
            this.labelPL.Name = "labelPL";
            this.labelPL.Size = new System.Drawing.Size(73, 13);
            this.labelPL.TabIndex = 13;
            this.labelPL.Text = "Petal Lenght: ";
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(15, 472);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(486, 115);
            this.textBoxResult.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.textBoxGeneratedSpecies);
            this.panel1.Controls.Add(this.buttonGenerate);
            this.panel1.Location = new System.Drawing.Point(507, 472);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 115);
            this.panel1.TabIndex = 18;
            // 
            // textBoxGeneratedSpecies
            // 
            this.textBoxGeneratedSpecies.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxGeneratedSpecies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGeneratedSpecies.Location = new System.Drawing.Point(211, 88);
            this.textBoxGeneratedSpecies.Name = "textBoxGeneratedSpecies";
            this.textBoxGeneratedSpecies.ReadOnly = true;
            this.textBoxGeneratedSpecies.Size = new System.Drawing.Size(117, 13);
            this.textBoxGeneratedSpecies.TabIndex = 19;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.Enabled = false;
            this.buttonGenerate.Location = new System.Drawing.Point(209, 66);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(118, 20);
            this.buttonGenerate.TabIndex = 19;
            this.buttonGenerate.Text = "Generate Case";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(711, 427);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(135, 40);
            this.buttonExport.TabIndex = 19;
            this.buttonExport.Text = "Export NeuralNetwork";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(15, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(827, 330);
            this.panel2.TabIndex = 21;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(821, 324);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // FormNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.textBoxPW);
            this.Controls.Add(this.labelPW);
            this.Controls.Add(this.textBoxPL);
            this.Controls.Add(this.labelPL);
            this.Controls.Add(this.textBoxSW);
            this.Controls.Add(this.labelSW);
            this.Controls.Add(this.textBoxSL);
            this.Controls.Add(this.labelSP);
            this.Controls.Add(this.buttonRunCase);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonTrain);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormNetwork";
            this.Text = "Neural Network POC";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonTrain;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button buttonRunCase;
        private System.Windows.Forms.Label labelSP;
        private System.Windows.Forms.TextBox textBoxSL;
        private System.Windows.Forms.TextBox textBoxSW;
        private System.Windows.Forms.Label labelSW;
        private System.Windows.Forms.TextBox textBoxPW;
        private System.Windows.Forms.Label labelPW;
        private System.Windows.Forms.TextBox textBoxPL;
        private System.Windows.Forms.Label labelPL;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxGeneratedSpecies;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

