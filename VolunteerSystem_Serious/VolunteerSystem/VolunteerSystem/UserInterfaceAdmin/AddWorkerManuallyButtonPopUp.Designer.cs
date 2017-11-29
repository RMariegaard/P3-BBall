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
            this.listOfWorkersBox = new System.Windows.Forms.ListBox();
            this.SearchForWorker = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listOfWorkersBox
            // 
            this.listOfWorkersBox.FormattingEnabled = true;
            this.listOfWorkersBox.Location = new System.Drawing.Point(12, 48);
            this.listOfWorkersBox.Name = "listOfWorkersBox";
            this.listOfWorkersBox.Size = new System.Drawing.Size(166, 212);
            this.listOfWorkersBox.TabIndex = 0;
            // 
            // SearchForWorker
            // 
            this.SearchForWorker.Location = new System.Drawing.Point(13, 22);
            this.SearchForWorker.Name = "SearchForWorker";
            this.SearchForWorker.Size = new System.Drawing.Size(165, 20);
            this.SearchForWorker.TabIndex = 1;
            this.SearchForWorker.Text = "Search For Volunteer";
            // 
            // AddWorkerManuallyButtonPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 326);
            this.Controls.Add(this.SearchForWorker);
            this.Controls.Add(this.listOfWorkersBox);
            this.Name = "AddWorkerManuallyButtonPopUp";
            this.Text = "AddWorkerManuallyButtonPopUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listOfWorkersBox;
        private System.Windows.Forms.TextBox SearchForWorker;
    }
}