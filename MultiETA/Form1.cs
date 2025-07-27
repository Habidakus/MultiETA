namespace MultiETA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 250;
            timer.Tick += OnTick;
            timer.Start();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            ETAGroup.OnTick();
        }

        private void button_add_category_Click(object sender, EventArgs e)
        {
            TabPage tab_page = new TabPage();
            tab_page.SuspendLayout();

            Button button_add_eta = new Button();
            button_add_eta.Location = new Point(12, 11);
            button_add_eta.Size = new Size(62, 23);
            button_add_eta.Text = "Add ETA";
            button_add_eta.UseVisualStyleBackColor = true;
            button_add_eta.Click += (s, ev) => Button_add_eta_Click(tab_page);

            Button button_sort = new Button();
            button_sort.Location = new Point(80, 11);
            button_sort.Size = new Size(62, 23);
            button_sort.Text = "Sort";
            button_sort.UseVisualStyleBackColor = true;
            button_sort.Click += (s, ev) => Button_sort_Click(tab_page);

            Button button_remove_category1 = new Button();
            tab_page.Controls.Add(button_remove_category1);
            button_remove_category1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_remove_category1.Location = new Point(837, 11);
            button_remove_category1.Name = "button_remove_category";
            button_remove_category1.Size = new Size(110, 23);
            button_remove_category1.TabIndex = 1;
            button_remove_category1.Text = "Remove Category";
            button_remove_category1.UseVisualStyleBackColor = true;
            button_remove_category1.Click += (s, ev) => Button_remove_category_Click(tab_page);

            tab_page.AutoScroll = true;
            tab_page.Controls.Add(button_sort);
            tab_page.Controls.Add(button_add_eta);
            tab_page.Location = new Point(4, 24);
            tab_page.Padding = new Padding(3);
            tab_page.Size = new Size(980, 579);
            tab_page.TabIndex = 0;
            tab_page.Text = "???";
            tab_page.UseVisualStyleBackColor = true;

            tab_page.ResumeLayout(false );
            tab_page.PerformLayout();

            tabControl1.Controls.Add(tab_page);
        }

        private void Button_remove_category_Click(TabPage tab_page)
        {
            tabControl1.Controls.Remove(tab_page);
        }

        private void Button_add_eta_Click(TabPage tab_page)
        {
            ETAGroup _ = ETAGroup.Create(tab_page);
        }

        private void Button_sort_Click(TabPage tab_page)
        {
            throw new NotImplementedException();
        }
    }
}
