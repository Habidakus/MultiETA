namespace MultiETA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button_sort = new Button();
            button_add_eta = new Button();
            button_remove_category = new Button();
            groupBox1 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button4 = new Button();
            textBox1 = new TextBox();
            button_add_category = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 23);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(966, 607);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(button_sort);
            tabPage1.Controls.Add(button_add_eta);
            tabPage1.Controls.Add(button_remove_category);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(958, 579);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_sort
            // 
            button_sort.Location = new Point(80, 11);
            button_sort.Name = "button_sort";
            button_sort.Size = new Size(62, 23);
            button_sort.TabIndex = 3;
            button_sort.Text = "Sort";
            button_sort.UseVisualStyleBackColor = true;
            // 
            // button_add_eta
            // 
            button_add_eta.Location = new Point(12, 11);
            button_add_eta.Name = "button_add_eta";
            button_add_eta.Size = new Size(62, 23);
            button_add_eta.TabIndex = 2;
            button_add_eta.Text = "Add ETA";
            button_add_eta.UseVisualStyleBackColor = true;
            // 
            // button_remove_category
            // 
            button_remove_category.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_remove_category.Location = new Point(837, 11);
            button_remove_category.Name = "button_remove_category";
            button_remove_category.Size = new Size(110, 23);
            button_remove_category.TabIndex = 1;
            button_remove_category.Text = "Remove Category";
            button_remove_category.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(6, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(939, 57);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(751, 24);
            label6.Name = "label6";
            label6.Size = new Size(92, 15);
            label6.TabIndex = 9;
            label6.Text = "Est Value: 1056.4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(577, 25);
            label5.Name = "label5";
            label5.Size = new Size(153, 15);
            label5.TabIndex = 8;
            label5.Text = "ETA: 53m 53s,  Thur 5:21 pm";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(439, 25);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 7;
            label4.Text = "Rate: 106.3 / hour";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(345, 25);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 6;
            label3.Text = "Goal: 100";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(204, 25);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 5;
            label2.Text = "Last Update: 100.5";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 25);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 4;
            label1.Text = "Initialize:";
            // 
            // button4
            // 
            button4.Location = new Point(862, 20);
            button4.Name = "button4";
            button4.Size = new Size(56, 23);
            button4.TabIndex = 3;
            button4.Text = "Reset";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(65, 21);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            // 
            // button_add_category
            // 
            button_add_category.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_add_category.Location = new Point(853, 12);
            button_add_category.Name = "button_add_category";
            button_add_category.Size = new Size(110, 29);
            button_add_category.TabIndex = 0;
            button_add_category.Text = "Add Category";
            button_add_category.UseVisualStyleBackColor = true;
            button_add_category.Click += button_add_category_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(981, 642);
            Controls.Add(button_add_category);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "MultiETA";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button_add_category;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Button button_add_eta;
        private Button button_remove_category;
        private Button button4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button button_sort;
    }
}
