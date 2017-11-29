namespace VolunteerSystem.UserInterfaceAdmin
{
    partial class AddWorkerManuallyButtonPopUp
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
            this.addWorkerButton = new System.Windows.Forms.Button();
            this.CancelAddWorker = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchForWorker = new System.Windows.Forms.TextBox();
            this.listOfWorkersBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // addWorkerButton
            // 
            this.addWorkerButton.Location = new System.Drawing.Point(292, 178);
            this.addWorkerButton.Name = "addWorkerButton";
            this.addWorkerButton.Size = new System.Drawing.Size(75, 23);
            this.addWorkerButton.TabIndex = 2;
            this.addWorkerButton.Text = "Add";
            this.addWorkerButton.UseVisualStyleBackColor = true;
            this.addWorkerButton.Click += new System.EventHandler(this.addWorkerButton_Click);
            // 
            // CancelAddWorker
            // 
            this.CancelAddWorker.Location = new System.Drawing.Point(40, 178);
            this.CancelAddWorker.Name = "CancelAddWorker";
            this.CancelAddWorker.Size = new System.Drawing.Size(75, 23);
            this.CancelAddWorker.TabIndex = 3;
            this.CancelAddWorker.Text = "Cancel";
            this.CancelAddWorker.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search for volunteer";
            // 
            // SearchForWorker
            // 
            this.SearchForWorker.Location = new System.Drawing.Point(121, 22);
            this.SearchForWorker.Name = "SearchForWorker";
            this.SearchForWorker.Size = new System.Drawing.Size(165, 20);
            this.SearchForWorker.TabIndex = 1;
            this.SearchForWorker.TextChanged += new System.EventHandler(this.SearchForWorker_TextChanged);
            // 
            // listOfWorkersBox
            // 
            this.listOfWorkersBox.FormattingEnabled = true;
            this.listOfWorkersBox.Location = new System.Drawing.Point(89, 48);
            this.listOfWorkersBox.Name = "listOfWorkersBox";
            this.listOfWorkersBox.Size = new System.Drawing.Size(235, 108);
            this.listOfWorkersBox.TabIndex = 0;
            // 
            // AddWorkerManuallyButtonPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 228);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelAddWorker);
            this.Controls.Add(this.addWorkerButton);
            this.Controls.Add(this.SearchForWorker);
            this.Controls.Add(this.listOfWorkersBox);
            this.Name = "AddWorkerManuallyButtonPopUp";
            this.Text = "AddWorkerManuallyButtonPopUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addWorkerButton;
        private System.Windows.Forms.Button CancelAddWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SearchForWorker;
        private System.Windows.Forms.ListBox listOfWorkersBox;
    }
}