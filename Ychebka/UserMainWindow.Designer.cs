namespace Ychebka
{
    partial class UserMainWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelFIO = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewAddress = new System.Windows.Forms.DataGridView();
            this.dataGridViewTasks = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewTools = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTools)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Профиль сотрудника:";
            // 
            // labelFIO
            // 
            this.labelFIO.AutoSize = true;
            this.labelFIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.labelFIO.Location = new System.Drawing.Point(309, 13);
            this.labelFIO.Name = "labelFIO";
            this.labelFIO.Size = new System.Drawing.Size(78, 31);
            this.labelFIO.TabIndex = 1;
            this.labelFIO.Text = "ФИО";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "Адреса:";
            // 
            // dataGridViewAddress
            // 
            this.dataGridViewAddress.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAddress.Location = new System.Drawing.Point(13, 88);
            this.dataGridViewAddress.Name = "dataGridViewAddress";
            this.dataGridViewAddress.Size = new System.Drawing.Size(1449, 143);
            this.dataGridViewAddress.TabIndex = 3;
            this.dataGridViewAddress.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAddress_CellContentClick);
            // 
            // dataGridViewTasks
            // 
            this.dataGridViewTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTasks.Location = new System.Drawing.Point(14, 269);
            this.dataGridViewTasks.Name = "dataGridViewTasks";
            this.dataGridViewTasks.Size = new System.Drawing.Size(1449, 190);
            this.dataGridViewTasks.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label4.Location = new System.Drawing.Point(13, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 31);
            this.label4.TabIndex = 4;
            this.label4.Text = "Задачи:";
            // 
            // dataGridViewTools
            // 
            this.dataGridViewTools.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTools.Location = new System.Drawing.Point(12, 506);
            this.dataGridViewTools.Name = "dataGridViewTools";
            this.dataGridViewTools.Size = new System.Drawing.Size(1449, 143);
            this.dataGridViewTools.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label5.Location = new System.Drawing.Point(13, 462);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 31);
            this.label5.TabIndex = 6;
            this.label5.Text = "Инструменты:";
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.buttonLogout.ForeColor = System.Drawing.Color.White;
            this.buttonLogout.Location = new System.Drawing.Point(14, 711);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(191, 45);
            this.buttonLogout.TabIndex = 8;
            this.buttonLogout.Text = "Выход";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // UserMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1487, 768);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.dataGridViewTools);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewTasks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridViewAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelFIO);
            this.Controls.Add(this.label1);
            this.Name = "UserMainWindow";
            this.Text = "UserMainWindow";
            this.Load += new System.EventHandler(this.UserMainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTools)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFIO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewAddress;
        private System.Windows.Forms.DataGridView dataGridViewTasks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewTools;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLogout;
    }
}