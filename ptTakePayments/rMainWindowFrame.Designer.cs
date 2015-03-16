namespace RDDSMakePayments
{
    partial class rMainWindowFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rMainWindowFrame));
            this.radRibbonBar1 = new Telerik.WinControls.UI.RadRibbonBar();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonBarGroup1 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.rbtnTakePayment = new Telerik.WinControls.UI.RadButtonElement();
            this.rbtngrpClose = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.rbtnCloseApp = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElement1 = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroup2 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radButtonElement2 = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonFormBehavior1 = new Telerik.WinControls.UI.RadRibbonFormBehavior();
            this.radRibbonBarButtonGroup1 = new Telerik.WinControls.UI.RadRibbonBarButtonGroup();
            this.radRibbonBarButtonGroup2 = new Telerik.WinControls.UI.RadRibbonBarButtonGroup();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radRibbonBar1
            // 
            this.radRibbonBar1.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab1});
            this.radRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.radRibbonBar1.Name = "radRibbonBar1";
            this.radRibbonBar1.QuickAccessToolBarItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonElement1});
            this.radRibbonBar1.Size = new System.Drawing.Size(1071, 167);
            this.radRibbonBar1.TabIndex = 1;
            this.radRibbonBar1.Text = "RDSS Take A Payment";
            this.radRibbonBar1.ThemeName = "ControlDefault";
            this.radRibbonBar1.Click += new System.EventHandler(this.radRibbonBar1_Click);
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.AccessibleDescription = "Applications";
            this.ribbonTab1.AccessibleName = "Applications";
            this.ribbonTab1.IsSelected = true;
            this.ribbonTab1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radRibbonBarGroup1,
            this.rbtngrpClose});
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "Applications";
            // 
            // radRibbonBarGroup1
            // 
            this.radRibbonBarGroup1.AccessibleDescription = "Payment Processing";
            this.radRibbonBarGroup1.AccessibleName = "Payment Processing";
            this.radRibbonBarGroup1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rbtnTakePayment});
            this.radRibbonBarGroup1.Name = "radRibbonBarGroup1";
            this.radRibbonBarGroup1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.radRibbonBarGroup1.Text = "Payments";
            // 
            // rbtnTakePayment
            // 
            this.rbtnTakePayment.AccessibleDescription = "Take Payment Wizard";
            this.rbtnTakePayment.AccessibleName = "Take Payment Wizard";
            this.rbtnTakePayment.Enabled = false;
            this.rbtnTakePayment.Image = global::RDDSMakePayments.Properties.Resources._1417689023_internt_web_technology_03_128;
            this.rbtnTakePayment.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnTakePayment.Name = "rbtnTakePayment";
            this.rbtnTakePayment.SmallImage = null;
            this.rbtnTakePayment.Text = "";
            this.rbtnTakePayment.Click += new System.EventHandler(this.rbtnTakePayment_Click);
            // 
            // rbtngrpClose
            // 
            this.rbtngrpClose.AccessibleDescription = "Close Application";
            this.rbtngrpClose.AccessibleName = "Close Application";
            this.rbtngrpClose.AutoSize = true;
            this.rbtngrpClose.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rbtnCloseApp});
            this.rbtngrpClose.Name = "rbtngrpClose";
            this.rbtngrpClose.Text = "Close";
            this.rbtngrpClose.TextOrientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rbtnCloseApp
            // 
            this.rbtnCloseApp.Image = global::RDDSMakePayments.Properties.Resources._1417688572_Shutdown_Box_Red;
            this.rbtnCloseApp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnCloseApp.Name = "rbtnCloseApp";
            this.rbtnCloseApp.Text = "";
            this.rbtnCloseApp.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.rbtnCloseApp.TextOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.rbtnCloseApp.TextWrap = true;
            this.rbtnCloseApp.Click += new System.EventHandler(this.rbtnCloseApp_Click);
            // 
            // radButtonElement1
            // 
            this.radButtonElement1.AccessibleDescription = "Main";
            this.radButtonElement1.AccessibleName = "Main";
            this.radButtonElement1.Name = "radButtonElement1";
            this.radButtonElement1.StretchHorizontally = false;
            this.radButtonElement1.StretchVertically = false;
            this.radButtonElement1.Text = "Main";
            // 
            // radRibbonBarGroup2
            // 
            this.radRibbonBarGroup2.AccessibleDescription = "Settings File";
            this.radRibbonBarGroup2.AccessibleName = "Settings File";
            this.radRibbonBarGroup2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonElement2});
            this.radRibbonBarGroup2.Name = "radRibbonBarGroup2";
            this.radRibbonBarGroup2.Text = "Settings File";
            // 
            // radButtonElement2
            // 
            this.radButtonElement2.Image = global::RDDSMakePayments.Properties.Resources.settings;
            this.radButtonElement2.Name = "radButtonElement2";
            this.radButtonElement2.Text = "";
            this.radButtonElement2.Click += new System.EventHandler(this.radButtonElement2_Click);
            // 
            // radRibbonFormBehavior1
            // 
            this.radRibbonFormBehavior1.Form = this;
            // 
            // radRibbonBarButtonGroup1
            // 
            this.radRibbonBarButtonGroup1.AccessibleDescription = "radRibbonBarButtonGroup1";
            this.radRibbonBarButtonGroup1.AccessibleName = "radRibbonBarButtonGroup1";
            this.radRibbonBarButtonGroup1.Name = "radRibbonBarButtonGroup1";
            this.radRibbonBarButtonGroup1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.radRibbonBarButtonGroup1.Text = "radRibbonBarButtonGroup1";
            // 
            // radRibbonBarButtonGroup2
            // 
            this.radRibbonBarButtonGroup2.AccessibleDescription = "radRibbonBarButtonGroup2";
            this.radRibbonBarButtonGroup2.AccessibleName = "radRibbonBarButtonGroup2";
            this.radRibbonBarButtonGroup2.Name = "radRibbonBarButtonGroup2";
            this.radRibbonBarButtonGroup2.Text = "radRibbonBarButtonGroup2";
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.AccessibleDescription = "Settings";
            this.radMenuItem1.AccessibleName = "Settings";
            this.radMenuItem1.Image = global::RDDSMakePayments.Properties.Resources.settings;
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "Settings";
            // 
            // rMainWindowFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 723);
            this.ControlBox = false;
            this.Controls.Add(this.radRibbonBar1);
            this.FormBehavior = this.radRibbonFormBehavior1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IconScaling = Telerik.WinControls.Enumerations.ImageScaling.None;
            this.IsMdiContainer = true;
            this.Name = "rMainWindowFrame";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RDSS Take A Payment";
            this.ThemeName = "ControlDefault";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rMainWindowFrame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadRibbonBar radRibbonBar1;
        private Telerik.WinControls.UI.RadRibbonFormBehavior radRibbonFormBehavior1;
        private Telerik.WinControls.UI.RibbonTab ribbonTab1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElement1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup1;
        private Telerik.WinControls.UI.RadButtonElement rbtnTakePayment;
        private Telerik.WinControls.UI.RadRibbonBarGroup rbtngrpClose;
        private Telerik.WinControls.UI.RadRibbonBarButtonGroup radRibbonBarButtonGroup1;
        private Telerik.WinControls.UI.RadButtonElement rbtnCloseApp;
        private Telerik.WinControls.UI.RadRibbonBarButtonGroup radRibbonBarButtonGroup2;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup2;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElement2;

    }
}
