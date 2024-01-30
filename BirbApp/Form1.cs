using System.Diagnostics;

namespace BirbApp
{
    public partial class Form1 : Form
    {
        public string label;
        public string photoPath;

        string pythonPath = @"C:\Users\Max\Desktop\files\env\python.exe";
        string pythonScript = @"C:\Users\Max\source\repos\BirbApp\BirbApp\classifier.py";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(pythonPath);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"{pythonScript} {photoPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process pythonProcess = new Process { StartInfo = psi };
            pythonProcess.Start();

            string output = pythonProcess.StandardOutput.ReadToEnd();
            Console.WriteLine("Output from Python:");
            Console.WriteLine(output);

            string labelPrefix = "Predicted Class Label:";
            int index = output.IndexOf(labelPrefix);
            if (index != -1)
            {
                string birdName = output.Substring(index + labelPrefix.Length).Trim();

                label1.Text = $"Identified bird: {birdName}";
            }

            pythonProcess.WaitForExit();
            pythonProcess.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                photoPath = opnfd.FileName;
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }
        }


    }
}
