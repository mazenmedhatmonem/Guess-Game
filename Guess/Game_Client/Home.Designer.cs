namespace Game_Client
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewAllRooms = new System.Windows.Forms.ListView();
            this.clmnAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnDifficulty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnPlayer1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnPlayer2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listboxCategory = new System.Windows.Forms.ComboBox();
            this.listboxDifficulty = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelGreeting = new System.Windows.Forms.Label();
            this.grpBoxRooms = new System.Windows.Forms.GroupBox();
            this.grpBoxCreateRoom = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshRooms = new System.Windows.Forms.Button();
            this.grpBoxRooms.SuspendLayout();
            this.grpBoxCreateRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.BackColor = System.Drawing.Color.Indigo;
            this.btnCreateRoom.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCreateRoom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoom.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRoom.ForeColor = System.Drawing.Color.LavenderBlush;
            this.btnCreateRoom.Location = new System.Drawing.Point(152, 337);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(277, 67);
            this.btnCreateRoom.TabIndex = 0;
            this.btnCreateRoom.Text = "Create room";
            this.btnCreateRoom.UseVisualStyleBackColor = false;
            this.btnCreateRoom.Click += new System.EventHandler(this.BtnCreateRoom_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(234, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Join or Watch a game";
            // 
            // listViewAllRooms
            // 
            this.listViewAllRooms.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewAllRooms.BackColor = System.Drawing.Color.Indigo;
            this.listViewAllRooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewAllRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnAction,
            this.clmnCategory,
            this.clmnDifficulty,
            this.clmnPlayer1,
            this.clmnPlayer2});
            this.listViewAllRooms.Font = new System.Drawing.Font("Kristen ITC", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewAllRooms.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.listViewAllRooms.FullRowSelect = true;
            this.listViewAllRooms.Location = new System.Drawing.Point(5, 46);
            this.listViewAllRooms.MultiSelect = false;
            this.listViewAllRooms.Name = "listViewAllRooms";
            this.listViewAllRooms.Size = new System.Drawing.Size(754, 469);
            this.listViewAllRooms.TabIndex = 6;
            this.listViewAllRooms.UseCompatibleStateImageBehavior = false;
            this.listViewAllRooms.View = System.Windows.Forms.View.Details;
            this.listViewAllRooms.SelectedIndexChanged += new System.EventHandler(this.listViewAllRooms_SelectedIndexChanged);
            // 
            // clmnAction
            // 
            this.clmnAction.Text = "Action";
            this.clmnAction.Width = 134;
            // 
            // clmnCategory
            // 
            this.clmnCategory.Text = "Category";
            this.clmnCategory.Width = 148;
            // 
            // clmnDifficulty
            // 
            this.clmnDifficulty.Text = "Difficulty";
            this.clmnDifficulty.Width = 141;
            // 
            // clmnPlayer1
            // 
            this.clmnPlayer1.Text = "Player1";
            this.clmnPlayer1.Width = 166;
            // 
            // clmnPlayer2
            // 
            this.clmnPlayer2.Text = "Player2";
            this.clmnPlayer2.Width = 165;
            // 
            // listboxCategory
            // 
            this.listboxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listboxCategory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.listboxCategory.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxCategory.FormattingEnabled = true;
            this.listboxCategory.Location = new System.Drawing.Point(236, 117);
            this.listboxCategory.Name = "listboxCategory";
            this.listboxCategory.Size = new System.Drawing.Size(260, 44);
            this.listboxCategory.TabIndex = 7;
            // 
            // listboxDifficulty
            // 
            this.listboxDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listboxDifficulty.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.listboxDifficulty.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxDifficulty.FormattingEnabled = true;
            this.listboxDifficulty.Location = new System.Drawing.Point(236, 206);
            this.listboxDifficulty.Name = "listboxDifficulty";
            this.listboxDifficulty.Size = new System.Drawing.Size(259, 44);
            this.listboxDifficulty.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 36);
            this.label3.TabIndex = 9;
            this.label3.Text = "Category";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 36);
            this.label4.TabIndex = 10;
            this.label4.Text = "Difficulty";
            // 
            // labelGreeting
            // 
            this.labelGreeting.AutoSize = true;
            this.labelGreeting.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGreeting.Location = new System.Drawing.Point(1035, 20);
            this.labelGreeting.Name = "labelGreeting";
            this.labelGreeting.Size = new System.Drawing.Size(0, 36);
            this.labelGreeting.TabIndex = 11;
            this.labelGreeting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBoxRooms
            // 
            this.grpBoxRooms.Controls.Add(this.listViewAllRooms);
            this.grpBoxRooms.Font = new System.Drawing.Font("Kristen ITC", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxRooms.Location = new System.Drawing.Point(3, 196);
            this.grpBoxRooms.Name = "grpBoxRooms";
            this.grpBoxRooms.Size = new System.Drawing.Size(766, 521);
            this.grpBoxRooms.TabIndex = 13;
            this.grpBoxRooms.TabStop = false;
            // 
            // grpBoxCreateRoom
            // 
            this.grpBoxCreateRoom.Controls.Add(this.label5);
            this.grpBoxCreateRoom.Controls.Add(this.btnCreateRoom);
            this.grpBoxCreateRoom.Controls.Add(this.listboxCategory);
            this.grpBoxCreateRoom.Controls.Add(this.listboxDifficulty);
            this.grpBoxCreateRoom.Controls.Add(this.label4);
            this.grpBoxCreateRoom.Controls.Add(this.label3);
            this.grpBoxCreateRoom.Font = new System.Drawing.Font("Kristen ITC", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxCreateRoom.Location = new System.Drawing.Point(775, 196);
            this.grpBoxCreateRoom.Name = "grpBoxCreateRoom";
            this.grpBoxCreateRoom.Size = new System.Drawing.Size(563, 521);
            this.grpBoxCreateRoom.TabIndex = 14;
            this.grpBoxCreateRoom.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Kristen ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(146, -5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 36);
            this.label5.TabIndex = 15;
            this.label5.Text = "Create a new room";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(412, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Kristen ITC", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(521, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(385, 66);
            this.label2.TabIndex = 17;
            this.label2.Text = "uess the Name";
            // 
            // btnRefreshRooms
            // 
            this.btnRefreshRooms.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefreshRooms.FlatAppearance.BorderSize = 0;
            this.btnRefreshRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshRooms.Font = new System.Drawing.Font("Kristen ITC", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshRooms.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshRooms.Image")));
            this.btnRefreshRooms.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshRooms.Location = new System.Drawing.Point(666, 185);
            this.btnRefreshRooms.Name = "btnRefreshRooms";
            this.btnRefreshRooms.Size = new System.Drawing.Size(97, 35);
            this.btnRefreshRooms.TabIndex = 7;
            this.btnRefreshRooms.Text = "Refresh";
            this.btnRefreshRooms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshRooms.UseVisualStyleBackColor = false;
            this.btnRefreshRooms.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkViolet;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.btnRefreshRooms);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpBoxRooms);
            this.Controls.Add(this.labelGreeting);
            this.Controls.Add(this.grpBoxCreateRoom);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.grpBoxRooms.ResumeLayout(false);
            this.grpBoxCreateRoom.ResumeLayout(false);
            this.grpBoxCreateRoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewAllRooms;
        private System.Windows.Forms.ComboBox listboxCategory;
        private System.Windows.Forms.ComboBox listboxDifficulty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelGreeting;
        private System.Windows.Forms.GroupBox grpBoxRooms;
        private System.Windows.Forms.GroupBox grpBoxCreateRoom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader clmnCategory;
        private System.Windows.Forms.ColumnHeader clmnDifficulty;
        private System.Windows.Forms.ColumnHeader clmnPlayer1;
        private System.Windows.Forms.ColumnHeader clmnPlayer2;
        private System.Windows.Forms.ColumnHeader clmnAction;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefreshRooms;
    }
}

