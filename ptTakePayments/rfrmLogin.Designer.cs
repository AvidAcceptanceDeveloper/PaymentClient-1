namespace RDDSMakePayments
{
    partial class rfrmLogin
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
            this.components = new System.ComponentModel.Container();
            this.rTitleBar = new Telerik.WinControls.UI.RadTitleBar();
            this.roundRectShapeTitle = new Telerik.WinControls.RoundRectShape(this.components);
            this.rtxtUserId = new Telerik.WinControls.UI.RadTextBox();
            this.rtxtPassword = new Telerik.WinControls.UI.RadTextBox();
            this.rlblUserId = new Telerik.WinControls.UI.RadLabel();
            this.rlblPassword = new Telerik.WinControls.UI.RadLabel();
            this.picbxLocked = new System.Windows.Forms.PictureBox();
            this.rbtnCancel = new Telerik.WinControls.UI.RadButton();
            this.rbtnLogin = new Telerik.WinControls.UI.RadButton();
            this.rlblErrorMsg = new Telerik.WinControls.UI.RadLabel();
            this.rlblEnvironment = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rTitleBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxLocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblErrorMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblEnvironment)).BeginInit();
            this.SuspendLayout();
            // 
            // rTitleBar
            // 
            this.rTitleBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rTitleBar.Location = new System.Drawing.Point(1, 1);
            this.rTitleBar.Name = "rTitleBar";
            // 
            // 
            // 
            this.rTitleBar.RootElement.ApplyShapeToControl = true;
            this.rTitleBar.RootElement.Shape = this.roundRectShapeTitle;
            this.rTitleBar.Size = new System.Drawing.Size(517, 23);
            this.rTitleBar.TabIndex = 0;
            this.rTitleBar.TabStop = false;
            this.rTitleBar.Text = "Login";
            // 
            // roundRectShapeTitle
            // 
            this.roundRectShapeTitle.BottomLeftRounded = false;
            this.roundRectShapeTitle.BottomRightRounded = false;
            // 
            // rtxtUserId
            // 
            this.rtxtUserId.Location = new System.Drawing.Point(134, 89);
            this.rtxtUserId.Name = "rtxtUserId";
            this.rtxtUserId.Size = new System.Drawing.Size(208, 27);
            this.rtxtUserId.TabIndex = 1;
            this.rtxtUserId.TextChanged += new System.EventHandler(this.rtxtUserId_TextChanged);
            // 
            // rtxtPassword
            // 
            this.rtxtPassword.Location = new System.Drawing.Point(134, 115);
            this.rtxtPassword.Name = "rtxtPassword";
            this.rtxtPassword.PasswordChar = '●';
            //this.rtxtPassword.Size = new System.Drawing.Size(208, 27);
            this.rtxtPassword.TabIndex = 2;
            this.rtxtPassword.UseSystemPasswordChar = true;
            this.rtxtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtxtPassword_KeyPress);
            // 
            // rlblUserId
            // 
            this.rlblUserId.Location = new System.Drawing.Point(73, 90);
            this.rlblUserId.Name = "rlblUserId";
            this.rlblUserId.Size = new System.Drawing.Size(61, 26);
            this.rlblUserId.TabIndex = 3;
            this.rlblUserId.Text = "User Id";
            // 
            // rlblPassword
            // 
            this.rlblPassword.Location = new System.Drawing.Point(73, 114);
            this.rlblPassword.Name = "rlblPassword";
            this.rlblPassword.Size = new System.Drawing.Size(79, 26);
            this.rlblPassword.TabIndex = 4;
            this.rlblPassword.Text = "Password";
            // 
            // picbxLocked
            // 
            this.picbxLocked.Image = global::RDDSMakePayments.Properties.Resources.lock_small;
            this.picbxLocked.Location = new System.Drawing.Point(369, 89);
            this.picbxLocked.Name = "picbxLocked";
            this.picbxLocked.Size = new System.Drawing.Size(83, 84);
            this.picbxLocked.TabIndex = 6;
            this.picbxLocked.TabStop = false;
            // 
            // rbtnCancel
            // 
            this.rbtnCancel.Location = new System.Drawing.Point(247, 149);
            this.rbtnCancel.Name = "rbtnCancel";
            this.rbtnCancel.Size = new System.Drawing.Size(95, 24);
            this.rbtnCancel.TabIndex = 4;
            this.rbtnCancel.Text = "Cancel";
            this.rbtnCancel.Click += new System.EventHandler(this.rbtnCancel_Click);
            // 
            // rbtnLogin
            // 
            this.rbtnLogin.Location = new System.Drawing.Point(134, 149);
            this.rbtnLogin.Name = "rbtnLogin";
            this.rbtnLogin.Size = new System.Drawing.Size(95, 24);
            this.rbtnLogin.TabIndex = 3;
            this.rbtnLogin.Text = "Login";
            this.rbtnLogin.Click += new System.EventHandler(this.rbtnLogin_Click);
            // 
            // rlblErrorMsg
            // 
            this.rlblErrorMsg.Location = new System.Drawing.Point(134, 194);
            this.rlblErrorMsg.Name = "rlblErrorMsg";
            this.rlblErrorMsg.Size = new System.Drawing.Size(2, 2);
            this.rlblErrorMsg.TabIndex = 7;
            // 
            // rlblEnvironment
            // 
            this.rlblEnvironment.Location = new System.Drawing.Point(134, 41);
            this.rlblEnvironment.Name = "rlblEnvironment";
            this.rlblEnvironment.Size = new System.Drawing.Size(2, 2);
            this.rlblEnvironment.TabIndex = 8;
            // 
            // rfrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(519, 243);
            this.Controls.Add(this.rlblEnvironment);
            this.Controls.Add(this.rlblErrorMsg);
            this.Controls.Add(this.rbtnLogin);
            this.Controls.Add(this.rbtnCancel);
            this.Controls.Add(this.picbxLocked);
            this.Controls.Add(this.rlblPassword);
            this.Controls.Add(this.rlblUserId);
            this.Controls.Add(this.rtxtPassword);
            this.Controls.Add(this.rtxtUserId);
            this.Controls.Add(this.rTitleBar);
            this.Name = "rfrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.rfrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rTitleBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxLocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblErrorMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlblEnvironment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTitleBar rTitleBar;
        private Telerik.WinControls.RoundRectShape roundRectShapeTitle;
        private Telerik.WinControls.UI.RadTextBox rtxtUserId;
        private Telerik.WinControls.UI.RadTextBox rtxtPassword;
        private Telerik.WinControls.UI.RadLabel rlblUserId;
        private Telerik.WinControls.UI.RadLabel rlblPassword;
        private System.Windows.Forms.PictureBox picbxLocked;
        private Telerik.WinControls.UI.RadButton rbtnCancel;
        private Telerik.WinControls.UI.RadButton rbtnLogin;
        private Telerik.WinControls.UI.RadLabel rlblErrorMsg;
        private Telerik.WinControls.UI.RadLabel rlblEnvironment;
    }
}
