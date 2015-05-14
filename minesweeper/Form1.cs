using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.IO;
using System.Reflection;

namespace minesweeper
{
    public partial class MainForm : Form
    {
        static private PrivateFontCollection privFonts = null;
        public Font digitsFont { get; private set; }
        private Field field;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, int cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitFonts()
        {
            byte[] fontFile = Properties.Resources.digits;

            IntPtr fontBuff = Marshal.AllocCoTaskMem(fontFile.Length);
            Marshal.Copy(fontFile, 0, fontBuff, fontFile.Length);

            uint cFonts = 0;
            AddFontMemResourceEx(fontBuff, fontFile.Length, IntPtr.Zero, ref cFonts);

            privFonts = new PrivateFontCollection();
            privFonts.AddMemoryFont(fontBuff, fontFile.Length);
            Marshal.FreeCoTaskMem(fontBuff);

            if (privFonts.Families.Length > 0)
            {
                digitsFont = new Font(privFonts.Families[0], 30f, FontStyle.Regular);
            }
        }        

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitFonts();
            field = new Field(this);
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            field.NewGame();
        }
    }

    public class FieldButton : Button
    {
        public Point pos { get; private set; }
        public FieldButton(Point pos) : base()
        {
            this.pos = pos;
        }
    }

    public class Field
    {
        private MainForm targetForm;
        private Label leftMinesLabel;
        private Label stateLabel;
        private Label currentTimeLabel;
        private FieldButton[,] buttonArray;
        private int[,] origField;
        private int[,] openedField;

        private int width;
        private int height;
        private int numberOfMines;
        private int leftMines;
        private int currentTime;

        private void CreateField()
        {
            UnbindButtons();
            origField = new int[height, width];
            openedField = new int[height, width];
            buttonArray = new FieldButton[height, width];
            stateLabel.Text = ":)";
            leftMines = numberOfMines; 
            BindButtons();
        }

        private void BindButtons()
        {
            if (buttonArray != null)
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        buttonArray[i, j] = new FieldButton(new Point(i, j));
                        targetForm.Controls.Add(buttonArray[i, j]);
                    }
        }

        private void UnbindButtons()
        {
            if (buttonArray != null)
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        if(targetForm.Controls.Contains(buttonArray[i, j]))
                        {
                            targetForm.Controls.Remove(buttonArray[i, j]);
                            buttonArray[i, j].Dispose();
                        }
        }

        private void UpdateLayout()
        {
            int cellHeight = 20;
            int cellWidth = 20;
            targetForm.Width = cellWidth * width + 35;
            targetForm.Height = cellHeight * height + 140;
            
            Point buttonStart = new Point(10, 90);
            leftMinesLabel.Location = new Point(10, 30);
            stateLabel.Location = new Point((targetForm.Width - stateLabel.Width) / 2, 30);
            currentTimeLabel.Location = new Point(targetForm.Width - 10 - currentTimeLabel.Width, 50);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    buttonArray[i, j].Height = cellHeight;
                    buttonArray[i, j].Width = cellWidth;
                    buttonArray[i, j].Location = new Point(buttonStart.X + j * cellWidth, buttonStart.Y + i * cellHeight);
                }
            }
        }

        public void NewGame()
        {
            CreateField();
            UpdateLayout();
        }

        public Field(MainForm form)
        {
            targetForm = form;

            leftMines = 10;
            leftMinesLabel = new Label();
            leftMinesLabel.AutoSize = false;
            leftMinesLabel.Width = 80;
            leftMinesLabel.Height = 55;
            leftMinesLabel.Padding = new Padding(0, 0, 0, 0);
            leftMinesLabel.Text = leftMines.ToString("D3");
            leftMinesLabel.Font = targetForm.digitsFont;
            leftMinesLabel.FlatStyle = FlatStyle.Standard;
            //leftMinesLabel.TextAlign = ContentAlignment.MiddleCenter;
            leftMinesLabel.BorderStyle = BorderStyle.Fixed3D;
            leftMinesLabel.BackColor = Color.Black;
            leftMinesLabel.ForeColor = Color.Red;
            targetForm.Controls.Add(leftMinesLabel);

            stateLabel = new Label();
            stateLabel.AutoSize = true;
            stateLabel.Text = ":)";
            stateLabel.Font = new Font(FontFamily.GenericSansSerif, 30.0f, FontStyle.Bold);
            targetForm.Controls.Add(stateLabel);

            currentTime = 0;
            currentTimeLabel = new Label();
            currentTimeLabel.Text = currentTime.ToString("D3");
            currentTimeLabel.Font = targetForm.digitsFont;
            currentTimeLabel.Height = 50;
            currentTimeLabel.Width = 70;
            //targetForm.Controls.Add(currentTimeLabel);

            width = 10;
            height = 10;
            numberOfMines = 10;
            NewGame();
        }
    }
}
