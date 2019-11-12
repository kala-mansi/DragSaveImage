using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DragSaveImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.AllowDrop = true;
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string url = e.Data.GetData("System.String") as string;
            Console.WriteLine(url);

            String _dir = @"c:\_images\";
            String prefix = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            String fn = _dir + prefix + ".tmp";

            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, fn);

            if (File.Exists(fn))
            {
                string ext = ValidFileType(fn);
                if (ext != null)
                {
                    FileInfo fi = new FileInfo(fn);
                    string new_fn = _dir + prefix + ext;
                    fi.MoveTo(new_fn);
                    textBox1.Text = "文件已经保存：" + new_fn;
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string url = e.Data.GetData("System.String") as string;
            Console.WriteLine(url);
            e.Effect = DragDropEffects.Copy;
        }

        private string ValidFileType(string fileName)
        {
            string ext = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (System.IO.BinaryReader br = new System.IO.BinaryReader(fs))
                {
                    string fileType = string.Empty; ;
                    byte data = br.ReadByte();
                    fileType += data.ToString();
                    data = br.ReadByte();
                    fileType += data.ToString();
                    FileExtension extension;
                    extension = (FileExtension)Enum.Parse(typeof(FileExtension), fileType);
                    if (extension == FileExtension.JPG)
                    {
                        ext = ".jpg";
                    }

                    if (extension == FileExtension.PNG)
                    {
                        ext = ".png";
                    }

                    return ext;
                }
            }
        }

        public enum FileExtension
        {
            JPG = 255216,
            PNG = 13780,
            BMP = 6677,
            GIF = 7173,
            COM = 7790,
            EXE = 7790,
            DLL = 7790,
            RAR = 8297,
            ZIP = 8075,
            XML = 6063,
            HTML = 6033,
            ASPX = 239187,
            CS = 117115,
            JS = 119105,
            TXT = 210187,
            SQL = 255254,
            BAT = 64101,
            BTSEED = 10056,
            RDP = 255254,
            PSD = 5666,
            PDF = 3780,
            CHM = 7384,
            LOG = 70105,
            REG = 8269,
            HLP = 6395,
            DOC = 208207,
            XLS = 208207,
            DOCX = 208207,
            XLSX = 208207,
            VALIDFILE = 9999999
        }
    }
}
