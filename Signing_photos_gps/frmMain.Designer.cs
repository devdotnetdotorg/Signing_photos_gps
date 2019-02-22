namespace Signing_photos_gps
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtPathFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPathGpsFile = new System.Windows.Forms.TextBox();
            this.btnSelectFileGps = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericMin = new System.Windows.Forms.NumericUpDown();
            this.numericHour = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radioBtnAddRemove = new System.Windows.Forms.RadioButton();
            this.radioBtnAddTime = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.richtbLog = new System.Windows.Forms.RichTextBox();
            this.checkReplaceGPS = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHour)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPathFolder
            // 
            this.txtPathFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathFolder.Location = new System.Drawing.Point(174, 7);
            this.txtPathFolder.Name = "txtPathFolder";
            this.txtPathFolder.Size = new System.Drawing.Size(293, 20);
            this.txtPathFolder.TabIndex = 0;
            this.txtPathFolder.TextChanged += new System.EventHandler(this.txtPathFolder_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь к папке с фотографиями:";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(473, 5);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(70, 23);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Выбрать";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Файл с координатами kml:";
            // 
            // txtPathGpsFile
            // 
            this.txtPathGpsFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathGpsFile.Location = new System.Drawing.Point(174, 40);
            this.txtPathGpsFile.Name = "txtPathGpsFile";
            this.txtPathGpsFile.Size = new System.Drawing.Size(293, 20);
            this.txtPathGpsFile.TabIndex = 4;
            this.txtPathGpsFile.TextChanged += new System.EventHandler(this.txtPathGpsFile_TextChanged);
            // 
            // btnSelectFileGps
            // 
            this.btnSelectFileGps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFileGps.Location = new System.Drawing.Point(473, 40);
            this.btnSelectFileGps.Name = "btnSelectFileGps";
            this.btnSelectFileGps.Size = new System.Drawing.Size(70, 23);
            this.btnSelectFileGps.TabIndex = 5;
            this.btnSelectFileGps.Text = "Выбрать";
            this.btnSelectFileGps.UseVisualStyleBackColor = true;
            this.btnSelectFileGps.Click += new System.EventHandler(this.btnSelectFileGps_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(464, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericMin);
            this.groupBox1.Controls.Add(this.numericHour);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.radioBtnAddRemove);
            this.groupBox1.Controls.Add(this.radioBtnAddTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(7, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 129);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Коррекция времени";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(426, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "[1 ... 9000]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(426, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "[1 ... 59]";
            // 
            // numericMin
            // 
            this.numericMin.Location = new System.Drawing.Point(300, 100);
            this.numericMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericMin.Name = "numericMin";
            this.numericMin.Size = new System.Drawing.Size(120, 20);
            this.numericMin.TabIndex = 15;
            // 
            // numericHour
            // 
            this.numericHour.Location = new System.Drawing.Point(300, 78);
            this.numericHour.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.numericHour.Name = "numericHour";
            this.numericHour.Size = new System.Drawing.Size(120, 20);
            this.numericHour.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Минуты";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Часы";
            // 
            // radioBtnAddRemove
            // 
            this.radioBtnAddRemove.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioBtnAddRemove.AutoSize = true;
            this.radioBtnAddRemove.Location = new System.Drawing.Point(92, 78);
            this.radioBtnAddRemove.Name = "radioBtnAddRemove";
            this.radioBtnAddRemove.Size = new System.Drawing.Size(82, 23);
            this.radioBtnAddRemove.TabIndex = 9;
            this.radioBtnAddRemove.Text = "- Уменьшить";
            this.radioBtnAddRemove.UseVisualStyleBackColor = true;
            // 
            // radioBtnAddTime
            // 
            this.radioBtnAddTime.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioBtnAddTime.AutoSize = true;
            this.radioBtnAddTime.Checked = true;
            this.radioBtnAddTime.Location = new System.Drawing.Point(10, 78);
            this.radioBtnAddTime.Name = "radioBtnAddTime";
            this.radioBtnAddTime.Size = new System.Drawing.Size(76, 23);
            this.radioBtnAddTime.TabIndex = 8;
            this.radioBtnAddTime.TabStop = true;
            this.radioBtnAddTime.Text = "+ Добавить";
            this.radioBtnAddTime.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "К времени создания фотографии";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(7, 239);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(536, 31);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Запустить";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // richtbLog
            // 
            this.richtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richtbLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richtbLog.Location = new System.Drawing.Point(7, 276);
            this.richtbLog.Name = "richtbLog";
            this.richtbLog.ReadOnly = true;
            this.richtbLog.Size = new System.Drawing.Size(536, 188);
            this.richtbLog.TabIndex = 10;
            this.richtbLog.Text = "Поддерживается файл kml полученный только с помощью приложения  \"Мои треки\" на An" +
    "droid.\nhttps://play.google.com/store/apps/details?id=com.google.android.maps.myt" +
    "racks&hl=ru";
            this.richtbLog.TextChanged += new System.EventHandler(this.richtbLog_TextChanged);
            // 
            // checkReplaceGPS
            // 
            this.checkReplaceGPS.AutoSize = true;
            this.checkReplaceGPS.Location = new System.Drawing.Point(16, 78);
            this.checkReplaceGPS.Name = "checkReplaceGPS";
            this.checkReplaceGPS.Size = new System.Drawing.Size(403, 17);
            this.checkReplaceGPS.TabIndex = 11;
            this.checkReplaceGPS.Text = "Перезаписывать координаты, даже если они присутствуют в фотографии";
            this.checkReplaceGPS.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 468);
            this.Controls.Add(this.checkReplaceGPS);
            this.Controls.Add(this.richtbLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectFileGps);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtPathGpsFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPathFolder);
            this.Name = "frmMain";
            this.Text = "Подписывание фотографий GPS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPathGpsFile;
        private System.Windows.Forms.Button btnSelectFileGps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioBtnAddRemove;
        private System.Windows.Forms.RadioButton radioBtnAddTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RichTextBox richtbLog;
        private System.Windows.Forms.CheckBox checkReplaceGPS;
        private System.Windows.Forms.NumericUpDown numericMin;
        private System.Windows.Forms.NumericUpDown numericHour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}

