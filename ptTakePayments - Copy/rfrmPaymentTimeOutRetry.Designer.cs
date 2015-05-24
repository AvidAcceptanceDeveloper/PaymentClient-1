namespace RDDSMakePayments
{
    partial class rfrmPaymentTimeOutRetry
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
            this.radTitleBar1 = new Telerik.WinControls.UI.RadTitleBar();
            this.roundRectShapeTitle = new Telerik.WinControls.RoundRectShape(this.components);
            this.roundRectShapeForm = new Telerik.WinControls.RoundRectShape(this.components);
            this.rtxtRetryText = new Telerik.WinControls.UI.RadTextBoxControl();
            this.rbtnOkRetry = new Telerik.WinControls.UI.RadButton();
            this.rbtnCancelRetry = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radTitleBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRetryText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnOkRetry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCancelRetry)).BeginInit();
            this.SuspendLayout();
            // 
            // radTitleBar1
            // 
            this.radTitleBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radTitleBar1.Location = new System.Drawing.Point(1, 1);
            this.radTitleBar1.Name = "radTitleBar1";
            // 
            // 
            // 
            this.radTitleBar1.RootElement.ApplyShapeToControl = true;
            this.radTitleBar1.RootElement.Shape = this.roundRectShapeTitle;
            this.radTitleBar1.Size = new System.Drawing.Size(471, 23);
            this.radTitleBar1.TabIndex = 0;
            this.radTitleBar1.TabStop = false;
            this.radTitleBar1.Text = "rfrmPaymentTimeOutRetry";
            // 
            // roundRectShapeTitle
            // 
            this.roundRectShapeTitle.BottomLeftRounded = false;
            this.roundRectShapeTitle.BottomRightRounded = false;
            // 
            // rtxtRetryText
            // 
            this.rtxtRetryText.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.rtxtRetryText.Enabled = false;
            this.rtxtRetryText.Location = new System.Drawing.Point(12, 40);
            this.rtxtRetryText.Multiline = true;
            this.rtxtRetryText.Name = "rtxtRetryText";
            this.rtxtRetryText.Size = new System.Drawing.Size(449, 62);
            this.rtxtRetryText.TabIndex = 1;
            this.rtxtRetryText.Text = "It appears that the loan is locked in Nortridge.  Make sure you have saved your c" +
    "hanges and retry the transaction again.  Cancel to send an email to payment proc" +
    "essing.";
            // 
            // rbtnOkRetry
            // 
            this.rbtnOkRetry.Location = new System.Drawing.Point(374, 108);
            this.rbtnOkRetry.Name = "rbtnOkRetry";
            this.rbtnOkRetry.Size = new System.Drawing.Size(37, 24);
            this.rbtnOkRetry.TabIndex = 3;
            this.rbtnOkRetry.Text = "Ok";
            this.rbtnOkRetry.Click += new System.EventHandler(this.rbtnOkRetry_Click);
            // 
            // rbtnCancelRetry
            // 
            this.rbtnCancelRetry.Location = new System.Drawing.Point(413, 108);
            this.rbtnCancelRetry.Name = "rbtnCancelRetry";
            this.rbtnCancelRetry.Size = new System.Drawing.Size(48, 24);
            this.rbtnCancelRetry.TabIndex = 4;
            this.rbtnCancelRetry.Text = "Cancel";
            // 
            // rfrmPaymentTimeOutRetry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 144);
            this.Controls.Add(this.rbtnCancelRetry);
            this.Controls.Add(this.rbtnOkRetry);
            this.Controls.Add(this.rtxtRetryText);
            this.Controls.Add(this.radTitleBar1);
            this.Name = "rfrmPaymentTimeOutRetry";
            this.Shape = this.roundRectShapeForm;
            this.Text = "rfrmPaymentTimeOutRetry";
            ((System.ComponentModel.ISupportInitialize)(this.radTitleBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRetryText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnOkRetry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCancelRetry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadTitleBar radTitleBar1;
        private Telerik.WinControls.RoundRectShape roundRectShapeForm;
        private Telerik.WinControls.RoundRectShape roundRectShapeTitle;
        private Telerik.WinControls.UI.RadTextBoxControl rtxtRetryText;
        private Telerik.WinControls.UI.RadButton rbtnOkRetry;
        private Telerik.WinControls.UI.RadButton rbtnCancelRetry;
    }
}
