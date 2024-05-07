namespace Ychebka
{
    partial class TasksPanel
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
            this.buttonBack = new System.Windows.Forms.Button();
            this.dataGridViewTasks = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ButtonShowCheck = new System.Windows.Forms.Button();
            this.buttonSaveStatus = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(18, 708);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(191, 45);
            this.buttonBack.TabIndex = 20;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // dataGridViewTasks
            // 
            this.dataGridViewTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTasks.Location = new System.Drawing.Point(18, 54);
            this.dataGridViewTasks.Name = "dataGridViewTasks";
            this.dataGridViewTasks.Size = new System.Drawing.Size(1156, 543);
            this.dataGridViewTasks.TabIndex = 19;
            this.dataGridViewTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTasks_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(598, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "Редактирование/просмотр задач сотрудников";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(18, 603);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(409, 45);
            this.button1.TabIndex = 24;
            this.button1.Text = "Показать все задачи";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ButtonShowCheck
            // 
            this.ButtonShowCheck.BackColor = System.Drawing.Color.DodgerBlue;
            this.ButtonShowCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonShowCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.ButtonShowCheck.ForeColor = System.Drawing.Color.White;
            this.ButtonShowCheck.Location = new System.Drawing.Point(18, 654);
            this.ButtonShowCheck.Name = "ButtonShowCheck";
            this.ButtonShowCheck.Size = new System.Drawing.Size(409, 45);
            this.ButtonShowCheck.TabIndex = 23;
            this.ButtonShowCheck.Text = "Показать задачи на проверке";
            this.ButtonShowCheck.UseVisualStyleBackColor = false;
            this.ButtonShowCheck.Click += new System.EventHandler(this.ButtonShowCheck_Click);
            // 
            // buttonSaveStatus
            // 
            this.buttonSaveStatus.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonSaveStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.buttonSaveStatus.ForeColor = System.Drawing.Color.White;
            this.buttonSaveStatus.Location = new System.Drawing.Point(1180, 476);
            this.buttonSaveStatus.Name = "buttonSaveStatus";
            this.buttonSaveStatus.Size = new System.Drawing.Size(416, 34);
            this.buttonSaveStatus.TabIndex = 26;
            this.buttonSaveStatus.Text = "Изменить статус";
            this.buttonSaveStatus.UseVisualStyleBackColor = false;
            this.buttonSaveStatus.Click += new System.EventHandler(this.buttonSaveStatus_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(1180, 436);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(416, 34);
            this.comboBoxStatus.TabIndex = 25;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1180, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(416, 356);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // TasksPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1608, 765);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonSaveStatus);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ButtonShowCheck);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.dataGridViewTasks);
            this.Controls.Add(this.label1);
            this.Name = "TasksPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование/просмотр задач";
            this.Load += new System.EventHandler(this.TasksPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.DataGridView dataGridViewTasks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ButtonShowCheck;
        private System.Windows.Forms.Button buttonSaveStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}