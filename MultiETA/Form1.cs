using System.Diagnostics;
using System.Text.Json;


namespace MultiETA
{
    public partial class Form1 : Form
    {
        private string MutexName => "MultiETASingleInstance";
        private Mutex? _mutex = null;
        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private void OnlyHaveOneInstanceOpen()
        {
            _mutex = new Mutex(true, MutexName, out bool createdNew);
            if (createdNew)
            {
                return;
            }

            // There is already an instance of this app running, we should bring it to the foreground and exit ourself.
            Process currentProcess = Process.GetCurrentProcess();
            Process? earlierProcess = Process.GetProcessesByName(currentProcess.ProcessName).Where(a => a.Id != currentProcess.Id).FirstOrDefault();
            if (earlierProcess != null)
            {
                SetForegroundWindow(earlierProcess.MainWindowHandle);
            }

            currentProcess.Kill();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string settingsFilePath = Category.GetSaveDataPath();

            OnlyHaveOneInstanceOpen();

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
