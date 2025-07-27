using System.Text.Json;

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

            string settingsFilePath = Category.GetSaveDataPath();

            string jsonData = String.Empty;
            if (File.Exists(settingsFilePath))
            {
                jsonData = File.ReadAllText(settingsFilePath);
                JsonDocument jsonDoc = JsonDocument.Parse(jsonData);
                Category.Load(tabControl1, jsonDoc.RootElement);
            }

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 250;
            timer.Tick += OnTick;
            timer.Start();

            tabControl1.Controls.Remove(tabPage1);
            //tabPage1.Hide();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            Category.OnTick();
        }

        private void button_add_category_Click(object sender, EventArgs e)
        {
            Category _ = Category.Create(tabControl1);
        }
    }
}
