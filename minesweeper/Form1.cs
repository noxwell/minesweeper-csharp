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
        private readonly Point[] neighbours = { new Point(-1, -1), new Point(-1, 0), new Point(-1, 1), new Point(0, -1),
                                                new Point(0, 1),   new Point(1, -1), new Point(1, 0),  new Point(1, 1)};

        private int width;
        private int height;
        private int numberOfMines;
        private Timer timer;
        private int leftMines;
        private int currentTime;
        private int gameState;

        private void DFSClick(Point p)
        {
            if(openedField[p.X, p.Y] != 0)
                return;
            if (origField[p.X, p.Y] == 0)
            {
                openedField[p.X, p.Y] = -1;
                buttonArray[p.X, p.Y].Enabled = false;
                for (int i = 0; i < 8; i++)
                {
                    Point np = new Point(p.X + neighbours[i].X, p.Y + neighbours[i].Y);
                    if (0 <= np.X && np.X < height && 0 <= np.Y && np.Y < width)
                        DFSClick(np);
                }
            }
            else if (origField[p.X, p.Y] != -1)
            {
                openedField[p.X, p.Y] = -1;
                buttonArray[p.X, p.Y].Text = origField[p.X, p.Y].ToString();
                buttonArray[p.X, p.Y].Enabled = false;
            }
        }

        private bool CheckWin()
        {
            int res = width * height - numberOfMines;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (openedField[i, j] == -1)
                        res--;
            return (res == 0);
        }

        private void FieldButton_Click(object sender, MouseEventArgs e)
        {
            if (gameState == -1)
                return;
            Point p = ((FieldButton)(sender)).pos;

            if (e.Button == MouseButtons.Left && openedField[p.X, p.Y] == 0)
            {
                if (gameState == 0)
                    GenerateMines(p);

                if (origField[p.X, p.Y] == -1)
                {
                    buttonArray[p.X, p.Y].Text = "М";
                    LoseGame();
                }
                else
                {
                    DFSClick(p);
                    if (CheckWin())
                        WinGame();
                }
            }
            else if (e.Button == MouseButtons.Right && openedField[p.X, p.Y] != -1)
            {
                if (openedField[p.X, p.Y] == 1)
                {
                    leftMines++;
                    leftMinesLabel.Text = leftMines.ToString("D3");
                }
                openedField[p.X, p.Y] = (openedField[p.X, p.Y] + 1) % 3;
                switch(openedField[p.X, p.Y])
                {
                    case 0: buttonArray[p.X, p.Y].Text = ""; break;
                    case 1:
                        buttonArray[p.X, p.Y].Text = "F";
                        leftMines--;
                        leftMinesLabel.Text = leftMines.ToString("D3");
                        break;
                    case 2: buttonArray[p.X, p.Y].Text = "?"; break;
                }
            }
        }

        private int CountMines(Point p)
        {
            int res = 0;
            for (int i = 0; i < 8; i++)
            {
                Point np = new Point(p.X + neighbours[i].X, p.Y + neighbours[i].Y);
                if (0 <= np.X && np.X < height && 0 <= np.Y && np.Y < width && origField[np.X, np.Y] == -1)
                    res++;
            }
            return res;
        }

        private void GenerateMines(Point first)
        {
            HashSet<Point> mines = new HashSet<Point>();
            Random generator = new Random();
            while (mines.Count != numberOfMines)
            {
                Point rpoint = new Point(generator.Next(0, height), generator.Next(0, width));
                if(rpoint != first)
                    mines.Add(rpoint);
            }
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    if (mines.Contains(new Point(i, j)))
                        origField[i, j] = -1;
                    else
                        origField[i, j] = 0;
                }
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if(origField[i, j] != -1)
                        origField[i, j] = CountMines(new Point(i, j));
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    openedField[i, j] = 0;
            gameState = 1;
        }

        private void CreateField()
        {
            UnbindButtons();
            origField = new int[height, width];
            openedField = new int[height, width];
            buttonArray = new FieldButton[height, width];
            gameState = 0; //field not initiated
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
                        buttonArray[i, j].MouseUp += FieldButton_Click;
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

        private void stateLabelClick(object sender, EventArgs e)
        {
            NewGame();
        }

        private void WinGame()
        {
            gameState = -1;
            stateLabel.Text = "8)";
        }

        private void LoseGame()
        {
            gameState = -1;
            stateLabel.Text = ":(";
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
            stateLabel.Click += stateLabelClick;
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
