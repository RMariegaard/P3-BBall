namespace VolunteerSystem.UserInterfaceAdmin
{
   public partial class AddWorkerManuallyButtonPopUp
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
            this.ListOfWorkersBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // addWorkerButton
            // 
            this.addWorkerButton.Location = new System.Drawing.Point(389, 219);
            this.addWorkerButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addWorkerButton.Name = "addWorkerButton";
            this.addWorkerButton.Size = new System.Drawing.Size(100, 28);
            this.addWorkerButton.TabIndex = 2;
            this.addWorkerButton.Text = "Add";
            this.addWorkerButton.UseVisualStyleBackColor = true;
            this.addWorkerButton.Click += new System.EventHandler(this.addWorkerButton_Click);
            // 
            // CancelAddWorker
            // 
            this.CancelAddWorker.Location = new System.Drawing.Point(53, 219);
            this.CancelAddWorker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CancelAddWorker.Name = "CancelAddWorker";
            this.CancelAddWorker.Size = new System.Drawing.Size(100, 28);
            this.CancelAddWorker.TabIndex = 3;
            this.CancelAddWorker.Text = "Cancel";
            this.CancelAddWorker.UseVisualStyleBackColor = true;
            this.CancelAddWorker.Click += new System.EventHandler(this.CancelAddWorker_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search for workers";
            // 
            // SearchForWorker
            // 
            this.SearchForWorker.Location = new System.Drawing.Point(161, 27);
            this.SearchForWorker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SearchForWorker.Name = "SearchForWorker";
            this.SearchForWorker.Size = new System.Drawing.Size(219, 22);
            this.SearchForWorker.TabIndex = 1;
            this.SearchForWorker.TextChanged += new System.EventHandler(this.SearchForWorker_TextChanged);
            // 
            // ListOfWorkersBox
            // 
            this.ListOfWorkersBox.FormattingEnabled = true;
            this.ListOfWorkersBox.ItemHeight = 16;
            this.ListOfWorkersBox.Location = new System.Drawing.Point(119, 59);
            this.ListOfWorkersBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ListOfWorkersBox.Name = "ListOfWorkersBox";
            this.ListOfWorkersBox.Size = new System.Drawing.Size(312, 132);
            this.ListOfWorkersBox.TabIndex = 0;
            // 
            // AddWorkerManuallyButtonPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 281);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelAddWorker);
            this.Controls.Add(this.addWorkerButton);
            this.Controls.Add(this.SearchForWorker);
            this.Controls.Add(this.ListOfWorkersBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.ListBox ListOfWorkersBox;
    }
}