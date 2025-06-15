namespace LibraryView
{
    partial class SearchForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Очистка используемых ресурсов.
        /// </summary>
        /// <param name="disposing">значение true, если управляемые 
        /// ресурсы должны быть утилизированы; в противном случае 
        /// значение false.</param>
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
            this.searchCriteriaGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchCriteriaGroupBox.Name = "searchCriteriaGroupBox";
            this.searchCriteriaGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchCriteriaGroupBox.Size = new System.Drawing.Size(557, 255);
            this.searchCriteriaGroupBox.TabIndex = 0;
            this.searchCriteriaGroupBox.TabStop = false;
            this.searchCriteriaGroupBox.Text = "Критерии поиска";
            // 
            // closeSearchButton
            // 
            this.closeSearchButton.Location = new System.Drawing.Point(440, 220);
            this.closeSearchButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.closeSearchButton.Name = "closeSearchButton";
            this.closeSearchButton.Size = new System.Drawing.Size(100, 28);
            this.closeSearchButton.TabIndex = 10;
            this.closeSearchButton.Text = "Закрыть";
            this.closeSearchButton.UseVisualStyleBackColor = true;
            this.closeSearchButton.Click += new System.EventHandler(this.closeSearchButton_Click);
            // 
            // performSearchButton
            // 
            this.performSearchButton.Location = new System.Drawing.Point(315, 220);
            this.performSearchButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.performSearchButton.Name = "performSearchButton";
            this.performSearchButton.Size = new System.Drawing.Size(100, 28);
            this.performSearchButton.TabIndex = 1;
            this.performSearchButton.Text = "Найти";
            this.performSearchButton.UseVisualStyleBackColor = true;
            this.performSearchButton.Click += new System.EventHandler(this.performSearchButton_Click);
            // 
            // searchYearTextBox
            // 
            this.searchYearTextBox.Location = new System.Drawing.Point(153, 220);
            this.searchYearTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchYearTextBox.Name = "searchYearTextBox";
            this.searchYearTextBox.Size = new System.Drawing.Size(132, 22);
            this.searchYearTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 224);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Год издания";
            // 
            // searchTitleTextBox
            // 
            this.searchTitleTextBox.Location = new System.Drawing.Point(153, 177);
            this.searchTitleTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchTitleTextBox.Name = "searchTitleTextBox";
            this.searchTitleTextBox.Size = new System.Drawing.Size(132, 22);
            this.searchTitleTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 181);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Название работы";
            // 
            // searchPatronymicTextBox
            // 
            this.searchPatronymicTextBox.Location = new System.Drawing.Point(152, 127);
            this.searchPatronymicTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchPatronymicTextBox.Name = "searchPatronymicTextBox";
            this.searchPatronymicTextBox.Size = new System.Drawing.Size(132, 22);
            this.searchPatronymicTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отчестсво";
            // 
            // searchNameTextBox
            // 
            this.searchNameTextBox.Location = new System.Drawing.Point(153, 81);
            this.searchNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchNameTextBox.Name = "searchNameTextBox";
            this.searchNameTextBox.Size = new System.Drawing.Size(132, 22);
            this.searchNameTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя";
            // 
            // searchSurnameTextBox
            // 
            this.searchSurnameTextBox.Location = new System.Drawing.Point(153, 36);
            this.searchSurnameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchSurnameTextBox.Name = "searchSurnameTextBox";
            this.searchSurnameTextBox.Size = new System.Drawing.Size(132, 22);
            this.searchSurnameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // searchResultsGroupBox
            // 
            this.searchResultsGroupBox.Controls.Add(this.searchResultsDataGridView);
            this.searchResultsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchResultsGroupBox.Location = new System.Drawing.Point(0, 264);
            this.searchResultsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchResultsGroupBox.Name = "searchResultsGroupBox";
            this.searchResultsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchResultsGroupBox.Size = new System.Drawing.Size(557, 160);
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
            this.searchResultsDataGridView.Location = new System.Drawing.Point(4, 19);
            this.searchResultsDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchResultsDataGridView.MultiSelect = false;
            this.searchResultsDataGridView.Name = "searchResultsDataGridView";
            this.searchResultsDataGridView.ReadOnly = true;
            this.searchResultsDataGridView.RowHeadersWidth = 51;
            this.searchResultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.searchResultsDataGridView.Size = new System.Drawing.Size(549, 137);
            this.searchResultsDataGridView.TabIndex = 0;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 424);
            this.Controls.Add(this.searchResultsGroupBox);
            this.Controls.Add(this.searchCriteriaGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SearchForm";
            this.Text = "Поиск работы";
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