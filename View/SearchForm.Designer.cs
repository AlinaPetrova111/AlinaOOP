namespace LibraryView
{
    partial class SearchForm
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
            this.searchCriteriaGroupBox = new System.Windows.Forms.GroupBox();
            this.closeSearchButton = new System.Windows.Forms.Button();
            this.performSearchButton = new System.Windows.Forms.Button();
            this.searchYearTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.searchTitleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.searchPatronymicTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchSurnameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchResultsGroupBox = new System.Windows.Forms.GroupBox();
            this.searchResultsDataGridView = new System.Windows.Forms.DataGridView();
            this.searchCriteriaGroupBox.SuspendLayout();
            this.searchResultsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchResultsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // searchCriteriaGroupBox
            // 
            this.searchCriteriaGroupBox.Controls.Add(this.closeSearchButton);
            this.searchCriteriaGroupBox.Controls.Add(this.performSearchButton);
            this.searchCriteriaGroupBox.Controls.Add(this.searchYearTextBox);
            this.searchCriteriaGroupBox.Controls.Add(this.label5);
            this.searchCriteriaGroupBox.Controls.Add(this.searchTitleTextBox);
            this.searchCriteriaGroupBox.Controls.Add(this.label4);
            this.searchCriteriaGroupBox.Controls.Add(this.searchPatronymicTextBox);
            this.searchCriteriaGroupBox.Controls.Add(this.label3);
            this.searchCriteriaGroupBox.Controls.Add(this.searchNameTextBox);
            this.searchCriteriaGroupBox.Controls.Add(this.label2);
            this.searchCriteriaGroupBox.Controls.Add(this.searchSurnameTextBox);
            this.searchCriteriaGroupBox.Controls.Add(this.label1);
            this.searchCriteriaGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchCriteriaGroupBox.Location = new System.Drawing.Point(0, 0);
            this.searchCriteriaGroupBox.Name = "searchCriteriaGroupBox";
            this.searchCriteriaGroupBox.Size = new System.Drawing.Size(800, 216);
            this.searchCriteriaGroupBox.TabIndex = 0;
            this.searchCriteriaGroupBox.TabStop = false;
            this.searchCriteriaGroupBox.Text = "Критерии поиска";
            // 
            // closeSearchButton
            // 
            this.closeSearchButton.Location = new System.Drawing.Point(374, 193);
            this.closeSearchButton.Name = "closeSearchButton";
            this.closeSearchButton.Size = new System.Drawing.Size(75, 23);
            this.closeSearchButton.TabIndex = 10;
            this.closeSearchButton.Text = "Закрыть";
            this.closeSearchButton.UseVisualStyleBackColor = true;
            this.closeSearchButton.Click += new System.EventHandler(this.closeSearchButton_Click);
            // 
            // performSearchButton
            // 
            this.performSearchButton.Location = new System.Drawing.Point(251, 193);
            this.performSearchButton.Name = "performSearchButton";
            this.performSearchButton.Size = new System.Drawing.Size(75, 23);
            this.performSearchButton.TabIndex = 1;
            this.performSearchButton.Text = "Найти";
            this.performSearchButton.UseVisualStyleBackColor = true;
            this.performSearchButton.Click += new System.EventHandler(this.performSearchButton_Click);
            // 
            // searchYearTextBox
            // 
            this.searchYearTextBox.Location = new System.Drawing.Point(115, 179);
            this.searchYearTextBox.Name = "searchYearTextBox";
            this.searchYearTextBox.Size = new System.Drawing.Size(100, 20);
            this.searchYearTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Год издания";
            // 
            // searchTitleTextBox
            // 
            this.searchTitleTextBox.Location = new System.Drawing.Point(115, 144);
            this.searchTitleTextBox.Name = "searchTitleTextBox";
            this.searchTitleTextBox.Size = new System.Drawing.Size(100, 20);
            this.searchTitleTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Название работы";
            // 
            // searchPatronymicTextBox
            // 
            this.searchPatronymicTextBox.Location = new System.Drawing.Point(98, 103);
            this.searchPatronymicTextBox.Name = "searchPatronymicTextBox";
            this.searchPatronymicTextBox.Size = new System.Drawing.Size(100, 20);
            this.searchPatronymicTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отчестсво";
            // 
            // searchNameTextBox
            // 
            this.searchNameTextBox.Location = new System.Drawing.Point(98, 66);
            this.searchNameTextBox.Name = "searchNameTextBox";
            this.searchNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.searchNameTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя";
            // 
            // searchSurnameTextBox
            // 
            this.searchSurnameTextBox.Location = new System.Drawing.Point(98, 29);
            this.searchSurnameTextBox.Name = "searchSurnameTextBox";
            this.searchSurnameTextBox.Size = new System.Drawing.Size(100, 20);
            this.searchSurnameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // searchResultsGroupBox
            // 
            this.searchResultsGroupBox.Controls.Add(this.searchResultsDataGridView);
            this.searchResultsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchResultsGroupBox.Location = new System.Drawing.Point(0, 238);
            this.searchResultsGroupBox.Name = "searchResultsGroupBox";
            this.searchResultsGroupBox.Size = new System.Drawing.Size(800, 212);
            this.searchResultsGroupBox.TabIndex = 1;
            this.searchResultsGroupBox.TabStop = false;
            this.searchResultsGroupBox.Text = "Результаты поиска";
            // 
            // searchResultsDataGridView
            // 
            this.searchResultsDataGridView.AllowUserToAddRows = false;
            this.searchResultsDataGridView.AllowUserToDeleteRows = false;
            this.searchResultsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.searchResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchResultsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.searchResultsDataGridView.MultiSelect = false;
            this.searchResultsDataGridView.Name = "searchResultsDataGridView";
            this.searchResultsDataGridView.ReadOnly = true;
            this.searchResultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.searchResultsDataGridView.Size = new System.Drawing.Size(794, 193);
            this.searchResultsDataGridView.TabIndex = 0;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchResultsGroupBox);
            this.Controls.Add(this.searchCriteriaGroupBox);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.searchCriteriaGroupBox.ResumeLayout(false);
            this.searchCriteriaGroupBox.PerformLayout();
            this.searchResultsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchResultsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox searchCriteriaGroupBox;
        private System.Windows.Forms.TextBox searchSurnameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchYearTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox searchTitleTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox searchPatronymicTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searchNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button performSearchButton;
        private System.Windows.Forms.GroupBox searchResultsGroupBox;
        private System.Windows.Forms.DataGridView searchResultsDataGridView;
        private System.Windows.Forms.Button closeSearchButton;
    }
}