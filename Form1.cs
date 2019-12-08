using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace VXPMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        struct PathMono
        {
            public string path;
            public bool isMono;
        }

        List<Button> tabs = new List<Button>();
        List<Panel> tabPanels = new List<Panel>();
        //       game name,           path,  isMono
        Dictionary<string, PathMono> gameList = new Dictionary<string, PathMono>();
        Dictionary<string, PathMono> emulatorList = new Dictionary<string, PathMono>();
        Dictionary<string, Bitmap> iconList = new Dictionary<string, Bitmap>();
        Dictionary<string, string> powerList = new Dictionary<string, string>();
        Color mainColor = Color.White;
        Color secondaryColor = Color.LightGray;
        Color thirdColor = Color.LightSlateGray;
        Color fourthColor = Color.DarkGray;
        Color textColor = Color.Blue;
        Color tabColor = Color.Black;

        int tab = 0;
        int[] tabIndx;

        int itemsPerRow = 7;

        private void Form1_Load(object sender, EventArgs e)
        {
            _OnLoad(sender, e);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            int w = (int)(this.Width / 1.04f);
            int h = this.Height;

            tabPanels.Add(gameView);
            tabPanels.Add(emulatorView);
            tabPanels.Add(settingView);
            tabPanels.Add(consoleView);
            tabPanels.Add(powerView);

            tabs.Add(games);
            tabs.Add(emulators);
            tabs.Add(settings);
            tabs.Add(console);
            tabs.Add(power);

            tabIndx = new int[tabs.Count - 1];
            for (int i = 0; i < tabIndx.Length; i++)
                tabIndx[i] = 0;

            iconList.Add("Quake 3", global::VXPMenu.Properties.Resources.quake3);
            iconList.Add("MCPi", global::VXPMenu.Properties.Resources.minecraft);

            StreamReader settingReader = new StreamReader("Resources/settings.txt");
            string content;
            while((content = settingReader.ReadLine()) != null) {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"((""((?<token>.*?)(?<!\\)"")|(?<token>[\w]+))(\s)*)", options);
                var result = (from Match m in regex.Matches(content)
                              where m.Groups["token"].Success
                              select m.Groups["token"].Value).ToList();

                switch(result[0])
                {
                    case "game":
                        PathMono pm = new PathMono();
                        pm.path = result[2];
                        switch (result[3])
                        {
                            case "false":
                                pm.isMono = false;
                                break;

                            case "true":
                                pm.isMono = true;
                                break;
                        }
                        gameList.Add(result[1], pm);
                        break;

                    case "addgame":
                        PathMono _pm = new PathMono();
                        _pm.path = result[2];
                        switch (result[3])
                        {
                            case "false":
                                _pm.isMono = false;
                                break;

                            case "true":
                                _pm.isMono = true;
                                break;
                        }
                        gameList.Add(result[1], _pm); break;

                    case "emulator":
                        PathMono __pm = new PathMono();
                        __pm.path = result[2];
                        switch (result[3])
                        {
                            case "false":
                                __pm.isMono = false;
                                break;

                            case "true":
                                __pm.isMono = true;
                                break;
                        }
                        emulatorList.Add(result[1], __pm);
                        break;

                    case "setting":
                        switch(result[1])
                        {
                            case "primarycolor":
                                mainColor = Color.FromArgb(Convert.ToInt16(result[2]), Convert.ToInt16(result[3]), Convert.ToInt16(result[4]));
                                break;

                            case "secondarycolor":
                                secondaryColor = Color.FromArgb(Convert.ToInt16(result[2]), Convert.ToInt16(result[3]), Convert.ToInt16(result[4]));
                                break;

                            case "thirdcolor":
                                thirdColor = Color.FromArgb(Convert.ToInt16(result[2]), Convert.ToInt16(result[3]), Convert.ToInt16(result[4]));
                                break;
                            case "fourthcolor":
                                fourthColor = Color.FromArgb(Convert.ToInt16(result[2]), Convert.ToInt16(result[3]), Convert.ToInt16(result[4]));
                                break;
                            case "tabcolor":
                                tabColor = Color.FromArgb(Convert.ToInt16(result[2]), Convert.ToInt16(result[3]), Convert.ToInt16(result[4]));
                                break;
                            case "textcolor":
                                textColor = Color.FromArgb(Convert.ToInt16(result[2]), Convert.ToInt16(result[3]), Convert.ToInt16(result[4]));
                                break;
                        }
                        break;

                    case "power":
                        switch(result[1])
                        {
                            case "poweroff":
                                    powerList.Add("Shut Down", "sudo poweroff");
                                break;

                            case "reboot":
                                powerList.Add("Reboot", "sudo reboot");
                                break;
                        }
                        break;
                }
            }

            int gameID = 0;
            int gameIDRow = 0;

            foreach (KeyValuePair<string, PathMono> gameKV in gameList)
            {
                string name = gameKV.Key;
                string path = gameKV.Value.path;
                bool isMono = gameKV.Value.isMono;

                Button _l = new Button();
                _l.FlatAppearance.BorderColor = Color.FromArgb(255, tabColor.R + 10, tabColor.G + 10, tabColor.B + 10);
                _l.FlatAppearance.BorderSize = 1;
                _l.FlatStyle = FlatStyle.Flat;
                _l.BackColor = tabColor;
                if (iconList.ContainsKey(name))
                {
                    _l.BackgroundImage = SetImageColor(iconList[name], mainColor);
                }
                else
                {
                    _l.BackgroundImage = SetImageColor(global::VXPMenu.Properties.Resources.unknown, mainColor);
                }
                _l.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                _l.Font = new Font("Segoe UI Light", 7);
                _l.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                _l.Location = new System.Drawing.Point(0, 0);
                //_l.Text = name;
                _l.ForeColor = textColor;
                _l.Click += new EventHandler(delegate (object _sender, EventArgs _e) { OpenApplication(path, isMono); });
                _l.TextAlign = ContentAlignment.BottomCenter;
                _l.Size = new System.Drawing.Size((int)(w / 8), (int)(w / 8));
                _l.Location = new Point(2 + (int)(w / 72 + gameID * w / 8 + (gameID * (w / 72))), w / 72 + ((gameIDRow) * _l.Height) + (gameIDRow * (w / 72)));
                _l.TabIndex = 9;
                _l.UseVisualStyleBackColor = false;

                gameView.Controls.Add(_l);

                gameID++;
                if (gameID == itemsPerRow)
                {
                    gameIDRow++;
                    gameID = 0;
                }
            }

            int emulatorID = 0;
            int emulatorIDRow = 0;
            int emulatorsPerRow = 0;

            foreach (KeyValuePair<string, PathMono> emulatorKV in emulatorList)
            {
                string name = emulatorKV.Key;
                string path = emulatorKV.Value.path;
                bool isMono = emulatorKV.Value.isMono;

                Button _l = new Button();
                _l.FlatAppearance.BorderColor = Color.FromArgb(255, tabColor.R + 10, tabColor.G + 10, tabColor.B + 10);
                _l.FlatAppearance.BorderSize = 1;
                _l.FlatStyle = FlatStyle.Flat;
                _l.BackColor = tabColor;
                if (iconList.ContainsKey(name))
                {
                    _l.BackgroundImage = SetImageColor(iconList[name], mainColor);
                }
                else
                {
                    _l.BackgroundImage = SetImageColor(global::VXPMenu.Properties.Resources.unknown, mainColor);
                }
                _l.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                _l.Font = new Font("Segoe UI Light", 7);
                _l.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                _l.Location = new System.Drawing.Point(0, 0);
                //_l.Text = name;
                _l.ForeColor = textColor;
                _l.Click += new EventHandler(delegate (object _sender, EventArgs _e) { OpenApplication(path, isMono); });
                _l.TextAlign = ContentAlignment.BottomCenter;
                _l.Size = new System.Drawing.Size((int)(w / 8), (int)(w / 8));
                _l.Location = new Point(2 + (int)(w / 72 + emulatorID * w / 8 + (emulatorID * (w / 72))), w / 72 + ((emulatorIDRow) * _l.Height) + (emulatorIDRow * (w / 72)));
                _l.TabIndex = 9;
                _l.UseVisualStyleBackColor = false;

                emulatorView.Controls.Add(_l);

                emulatorID++;
                if (emulatorID == emulatorsPerRow)
                {
                    emulatorIDRow++;
                    emulatorID = 0;
                }
            }

            int powerIDRow = 0;

            foreach (KeyValuePair<string, string> powerKV in powerList)
            {
                string name = powerKV.Key;
                string[] args = powerKV.Value.Split(' ');

                Button _l = new Button();
                _l.FlatStyle = FlatStyle.Flat;
                _l.BackColor = tabColor;
                _l.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                _l.FlatAppearance.BorderColor = Color.FromArgb(255, tabColor.R + 10, tabColor.G + 10, tabColor.B + 10);
                _l.FlatAppearance.BorderSize = 1;
                _l.Font = new Font("Segoe UI Light", 9);
                _l.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                _l.Location = new System.Drawing.Point(0, 0);
                _l.Text = name;
                _l.ForeColor = textColor;
                _l.Click += new EventHandler(delegate (object _sender, EventArgs _e) {
                    var info = new ProcessStartInfo();
                    info.FileName = args[0];
                    info.Arguments = args[1];

                    info.UseShellExecute = false;
                    info.CreateNoWindow = true;

                    info.RedirectStandardOutput = true;
                    info.RedirectStandardError = true;

                    var p = Process.Start(info);
                    p.WaitForExit();
                });
                _l.TextAlign = ContentAlignment.MiddleCenter;
                _l.Size = new System.Drawing.Size(w - 20, h / 10);
                _l.Location = new Point(10, h / 18 + ((powerIDRow) * _l.Height) + (powerIDRow * (h / 24)));
                _l.TabIndex = 9;
                _l.UseVisualStyleBackColor = false;

                powerView.Controls.Add(_l);
                powerIDRow++;
            }

            //this.BackgroundImage = global::VXPMenu.Properties.Resources.bg;
            //this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = secondaryColor;

            games.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gamecontroller1), tabColor);
            games.Location = new Point(0, h / 16);
            games.Size = new Size(w / tabs.Count, h / 6);
            games.BackColor = mainColor;

            Panel gameBorder = new Panel();
            gameBorder.Size = new Size(1, games.Height);
            gameBorder.Location = new Point(games.Width - 1, 0);
            gameBorder.BackColor = secondaryColor;

            games.Controls.Add(gameBorder);

            emulators.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.videogame), tabColor);
            emulators.Location = new Point(w / tabs.Count, h / 16);
            emulators.Size = new Size(w / tabs.Count, h / 6);
            emulators.BackColor = mainColor;

            Panel emulatorBorder = new Panel();
            emulatorBorder.Size = new Size(1, games.Height);
            emulatorBorder.Location = new Point(games.Width - 1, 0);
            emulatorBorder.BackColor = secondaryColor;

            emulators.Controls.Add(emulatorBorder);

            settings.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gear65), tabColor);
            settings.Location = new Point((w / tabs.Count) * 2, h / 16);
            settings.Size = new Size(w / tabs.Count, h / 6);
            settings.BackColor = mainColor;

            Panel settingsBorder = new Panel();
            settingsBorder.Size = new Size(1, games.Height);
            settingsBorder.Location = new Point(games.Width - 1, 0);
            settingsBorder.BackColor = secondaryColor;

            settings.Controls.Add(settingsBorder);

            console.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.console), tabColor);
            console.Location = new Point((w / tabs.Count) * 3, h / 16);
            console.Size = new Size(w / tabs.Count, h / 6);
            console.BackColor = mainColor;

            Panel consoleBorder = new Panel();
            consoleBorder.Size = new Size(1, games.Height);
            consoleBorder.Location = new Point(games.Width - 1, 0);
            consoleBorder.BackColor = secondaryColor;

            console.Controls.Add(consoleBorder);

            power.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.power), tabColor);
            power.Location = new Point((w / tabs.Count) * 4, h / 16);
            power.Size = new Size(w / tabs.Count, h / 6);
            power.BackColor = mainColor;

            gameView.Location = new Point(0, h / 16 + h / 6);
            gameView.Size = new Size(w, h - h / 4 + 7);
            gameView.Visible = false;
            gameView.BackColor = secondaryColor;
            gameView.AutoScroll = true;

            emulatorView.Location = new Point(0, h / 16 + h / 6);
            emulatorView.Size = new Size(w, h - h / 6);
            emulatorView.Visible = false;
            emulatorView.AutoScroll = true;
            emulatorView.BackColor = Color.Transparent;

            settingView.Location = new Point(0, h / 16 + h / 6);
            settingView.Size = new Size(w, h - h / 6);
            settingView.Visible = false;
            settingView.AutoScroll = true;
            settingView.BackColor = Color.Transparent;

            //NumericUpDown pColorR = new NumericUpDown();
            //settingView.Controls.Add(pColorR);

            consoleView.Location = new Point(0, h / 16 + h / 6);
            consoleView.Size = new Size(w, h - h / 6);
            consoleView.Visible = false;
            consoleView.AutoScroll = true;
            consoleView.BackColor = Color.Transparent;

            powerView.Location = new Point(0, h / 16 + h / 6);
            powerView.Size = new Size(w, h - h / 6);
            powerView.Visible = false;
            powerView.AutoScroll = true;
            powerView.BackColor = Color.Transparent;

            titleBar.Location = new Point(0, 0);
            titleBar.Size = new Size(w, h / 16);
            titleBar.BackColor = thirdColor;

            Label titleBarLabel = new Label();
            titleBarLabel.Text = "Project VXP";
            titleBarLabel.ForeColor = textColor;
            titleBarLabel.Font = new Font("Segoe UI Light", 9);
            titleBarLabel.ForeColor = Color.Black;
            titleBarLabel.Location = new Point(titleBarLabel.Location.X, titleBarLabel.Location.Y - 1);

            Label dateTimeLabel = new Label();
            dateTimeLabel.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
            dateTimeLabel.ForeColor = textColor;
            dateTimeLabel.Font = new Font("Segoe UI Light", 9);
            dateTimeLabel.Name = "dateTime";
            dateTimeLabel.ForeColor = Color.Black;
            dateTimeLabel.Location = new Point((int)(Width / 2 - dateTimeLabel.Size.Width / 2), dateTimeLabel.Location.Y);
            dateTimeLabel.TextAlign = ContentAlignment.TopCenter;

            Label percentageLabel = new Label();
            percentageLabel.Text = "100%";
            percentageLabel.ForeColor = textColor;
            percentageLabel.Font = new Font("Segoe UI Light", 9);
            percentageLabel.Name = "percentage";
            percentageLabel.ForeColor = Color.Black;
            percentageLabel.Location = new Point((int)(Width / 1.55f), dateTimeLabel.Location.Y);
            percentageLabel.TextAlign = ContentAlignment.TopRight;


            Thread updateThread = new Thread(() => UpdateTime());
            updateThread.Start();

            titleBar.Controls.Add(dateTimeLabel);
            titleBar.Controls.Add(titleBarLabel);
            titleBar.Controls.Add(percentageLabel);

            games_Click(sender, e);

            Color c = GetView().Controls[tabIndx[tab]].BackColor;
            GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, c.R - 30, c.G - 30, c.B - 30);
        }

        Panel GetView()
        {
            switch(tab)
            {
                case 0:
                    return gameView;
                case 1:
                    return emulatorView;
                case 2:
                    return settingView;
                case 3:
                    return powerView;
            }
            return null;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int count = GetView().Controls.Count - 1;
            Color c;
            switch (keyData)
            {
                case Keys.Up:
                        GetView().Controls[tabIndx[tab]].BackColor = tabColor;
                        tabIndx[tab] = Clamp(tabIndx[tab] - itemsPerRow, 0, count);
                        c = GetView().Controls[tabIndx[tab]].BackColor;
                        GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, c.R - 30, c.G - 30, c.B - 30);
                    break;
                case Keys.Right:
                        GetView().Controls[tabIndx[tab]].BackColor = tabColor;
                        tabIndx[tab] = Clamp(tabIndx[tab] + 1, 0, count);
                        c = GetView().Controls[tabIndx[tab]].BackColor;
                        GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, c.R - 30, c.G - 30, c.B - 30);
                    break;
                case Keys.Down:
                    GetView().Controls[tabIndx[tab]].BackColor = tabColor;
                    tabIndx[tab] = Clamp(tabIndx[tab] + itemsPerRow, 0, count);
                    c = GetView().Controls[tabIndx[tab]].BackColor;
                    GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, c.R - 30, c.G - 30, c.B - 30);
                    break;
                case Keys.Left:
                        GetView().Controls[tabIndx[tab]].BackColor = tabColor;
                        tabIndx[tab] = Clamp(tabIndx[tab] - 1, 0, count);
                        c = GetView().Controls[tabIndx[tab]].BackColor;
                        GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, c.R - 30, c.G - 30, c.B - 30);
                    break;
                case Keys.PageDown:
                        tabs[tab].BackColor = mainColor;
                        tabs[tab].BackgroundImage = SetImageColor(tabs[tab].BackgroundImage, tabColor);
                        tab = Clamp(tab + 1, 0, tabs.Count - 1);
                        tabs[tab].PerformClick();
                        try {
                            c = GetView().Controls[tabIndx[tab]].BackColor;
                            GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, Clamp(c.R - 30, 0, 255), Clamp(c.G - 30, 0, 255), Clamp(c.B - 30, 0, 255));
                        } catch { }
                    break;
                case Keys.PageUp:
                        tabs[tab].BackColor = mainColor;
                        tabs[tab].BackgroundImage = SetImageColor(tabs[tab].BackgroundImage, tabColor);
                        tab = Clamp(tab - 1, 0, tabs.Count - 1);
                        tabs[tab].PerformClick();
                        try
                        {
                            c = GetView().Controls[tabIndx[tab]].BackColor;
                            GetView().Controls[tabIndx[tab]].BackColor = Color.FromArgb(255, Clamp(c.R - 30, 0, 255), Clamp(c.G - 30, 0, 255), Clamp(c.B - 30, 0, 255));
                        } catch { }
                    break;
                case Keys.Enter:
                    Button b = (Button)(GetView().Controls[tabIndx[tab]]);
                    b.PerformClick();
                    break;
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public struct RGBSlider
        {
            public TrackBar RSlider;
            public TrackBar GSlider;
            public TrackBar BSlider;
            public Panel colorDisplay;
            public Label titleLabel;
        }

        RGBSlider SetUpRGBSlider(RGBSlider _slider, Panel page, int x, int y, int h)
        {
            int xpadding = (int)this.Width / 128;
            int ypadding = (int)this.Height / 64;

            _slider.RSlider = new TrackBar();
                _slider.RSlider.BackColor = titleBar.BackColor;
                _slider.RSlider.Location = new Point(x + xpadding * 8, y + ypadding * 2);
                _slider.RSlider.Height = h;
            _slider.RSlider.Maximum = 255;
            _slider.GSlider = new TrackBar();
                _slider.GSlider.BackColor = SystemColors.Control;
                _slider.GSlider.Maximum = 255;
                _slider.GSlider.Location = new Point(x + xpadding * 8, _slider.RSlider.Height + y + ypadding * 2);
                _slider.GSlider.Height = h;

            _slider.BSlider = new TrackBar();
                _slider.BSlider.BackColor = SystemColors.Control;
                _slider.BSlider.Maximum = 255;
                _slider.BSlider.Location = new Point(x + xpadding * 8, _slider.RSlider.Height + _slider.GSlider.Height + y + ypadding * 2);
                _slider.BSlider.Height = h;


            _slider.colorDisplay = new Panel();
            _slider.colorDisplay.Location = new Point(_slider.RSlider.Width + x + xpadding + xpadding * 2, y + ypadding * 2);
            _slider.colorDisplay.Size = new Size(xpadding, _slider.RSlider.Height + _slider.GSlider.Height + _slider.BSlider.Height);

            page.Controls.Add(_slider.RSlider);
            page.Controls.Add(_slider.GSlider);
            page.Controls.Add(_slider.BSlider);

            Label RLabel = new Label();
            RLabel.Location = new Point(x, y + _slider.RSlider.Height / 4 + ypadding * 2);
            RLabel.Text = "R:";

            Label GLabel = new Label();
            GLabel.Location = new Point(x, y + _slider.RSlider.Height + _slider.GSlider.Height / 4 + ypadding * 2);
            GLabel.Text = "G:";

            Label BLabel = new Label();
            BLabel.Location = new Point(x, y + _slider.RSlider.Height + _slider.GSlider.Height + _slider.BSlider.Height / 4 + ypadding * 2);
            BLabel.Text = "B:";

            _slider.titleLabel = new Label();
            _slider.titleLabel.Location = new Point(x + (_slider.RSlider.Width) / 3, y);
            _slider.titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            page.Controls.Add(RLabel);
            page.Controls.Add(GLabel);
            page.Controls.Add(BLabel);
            page.Controls.Add(_slider.titleLabel);

            //page.Controls.Add(_slider.colorDisplay);


            return _slider;
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        private void _OnLoad(object sender, EventArgs e)
        {
            RGBSlider rgbSlider = new RGBSlider();
            rgbSlider = SetUpRGBSlider(rgbSlider, settingView, 0, 0, 0);

            rgbSlider.RSlider.Value = thirdColor.R;
            rgbSlider.RSlider.ValueChanged += new EventHandler((object _sender, EventArgs _e) => {
                foreach (Button _tab in tabs)
                {
                    tabColor = Color.FromArgb(rgbSlider.RSlider.Value, tabColor.G, tabColor.B);
                    _tab.BackgroundImage = SetImageColor(new Bitmap(_tab.BackgroundImage), tabColor);
                }
                foreach (Button _b in gameView.Controls.OfType<Control>())
                {
                    _b.BackColor = tabColor;
                    _b.FlatAppearance.BorderColor = Color.FromArgb(255, Clamp(tabColor.R + 10, 0, 255), Clamp(tabColor.G + 10, 0, 255), Clamp(tabColor.B + 10, 0, 255));
                }
                foreach (Button _b in emulatorView.Controls.OfType<Control>())
                {
                    _b.BackColor = tabColor;
                    _b.FlatAppearance.BorderColor = Color.FromArgb(255, Clamp(tabColor.R + 10, 0, 255), Clamp(tabColor.G + 10, 0, 255), Clamp(tabColor.B + 10, 0, 255));
                }
                settings.BackColor = tabColor;
                settings.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gear65), mainColor);
                rgbSlider.colorDisplay.BackColor = tabColor;
                titleBar.BackColor = tabColor;
            });

            rgbSlider.GSlider.Value = thirdColor.G;
            rgbSlider.GSlider.ValueChanged += new EventHandler((object _sender, EventArgs _e) => {
                foreach (Button _tab in tabs)
                {
                    tabColor = Color.FromArgb(tabColor.R, rgbSlider.GSlider.Value, tabColor.B);
                    _tab.BackgroundImage = SetImageColor(new Bitmap(_tab.BackgroundImage), tabColor);
                }
                foreach (Button _b in gameView.Controls.OfType<Control>())
                {
                    _b.BackColor = tabColor;
                    _b.FlatAppearance.BorderColor = Color.FromArgb(255, Clamp(tabColor.R + 10, 0, 255), Clamp(tabColor.G + 10, 0, 255), Clamp(tabColor.B + 10, 0, 255));
                }
                foreach (Button _b in emulatorView.Controls.OfType<Control>())
                {
                    _b.BackColor = tabColor;
                    _b.FlatAppearance.BorderColor = Color.FromArgb(255, Clamp(tabColor.R + 10, 0, 255), Clamp(tabColor.G + 10, 0, 255), Clamp(tabColor.B + 10, 0, 255));
                }
                settings.BackColor = tabColor;
                settings.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gear65), mainColor);
                rgbSlider.colorDisplay.BackColor = tabColor;
                titleBar.BackColor = tabColor;
            });

            rgbSlider.BSlider.Value = thirdColor.B;
            rgbSlider.BSlider.ValueChanged += new EventHandler((object _sender, EventArgs _e) => {
                foreach (Button _tab in tabs)
                {
                    tabColor = Color.FromArgb(tabColor.B, tabColor.G, rgbSlider.BSlider.Value);
                    _tab.BackgroundImage = SetImageColor(new Bitmap(_tab.BackgroundImage), tabColor);
                }
                foreach (Button _b in gameView.Controls.OfType<Control>())
                {
                    _b.BackColor = tabColor;
                    _b.FlatAppearance.BorderColor = Color.FromArgb(255, Clamp(tabColor.R + 10, 0, 255), Clamp(tabColor.G + 10, 0, 255), Clamp(tabColor.B + 10, 0, 255));
                }
                foreach (Button _b in emulatorView.Controls.OfType<Control>())
                {
                    _b.BackColor = tabColor;
                    _b.FlatAppearance.BorderColor = Color.FromArgb(255, Clamp(tabColor.R + 10, 0, 255), Clamp(tabColor.G + 10, 0, 255), Clamp(tabColor.B + 10, 0, 255));
                }
                settings.BackColor = tabColor;
                settings.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gear65), mainColor);
                rgbSlider.colorDisplay.BackColor = tabColor;
                titleBar.BackColor = tabColor;
            });

            rgbSlider.colorDisplay.BackColor = tabColor;
            rgbSlider.titleLabel.Text = "Tab Color";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void games_Click(object sender, EventArgs e)
        {
            foreach(Button _b in tabs)
            {
                _b.BackColor = mainColor;
                _b.BackgroundImage = SetImageColor(_b.BackgroundImage, tabColor);
            }

            games.BackColor = tabColor;
            games.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gamecontroller1), mainColor);

            foreach (Panel _p in tabPanels)
            {
                _p.Visible = false;
            }
            gameView.Visible = true;
        }

        private void emulators_Click(object sender, EventArgs e)
        {
            foreach (Button _b in tabs)
            {
                _b.BackColor = mainColor;
                _b.BackgroundImage = SetImageColor(_b.BackgroundImage, tabColor);
            }

            emulators.BackColor = tabColor;
            emulators.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.videogame), mainColor);

            foreach (Panel _p in tabPanels)
            {
                _p.Visible = false;
            }
            emulatorView.Visible = true;
        }

        private void settings_Click(object sender, EventArgs e)
        {
            foreach (Button _b in tabs)
            {
                _b.BackColor = mainColor;
                _b.BackgroundImage = SetImageColor(_b.BackgroundImage, tabColor);
            }

            settings.BackColor = tabColor;
            settings.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.gear65), mainColor);

            foreach (Panel _p in tabPanels)
            {
                _p.Visible = false;
            }
            settingView.Visible = true;
        }

        private void console_Click(object sender, EventArgs e)
        {
            foreach (Button _b in tabs)
            {
                _b.BackColor = mainColor;
                _b.BackgroundImage = SetImageColor(_b.BackgroundImage, tabColor);
            }

            console.BackColor = tabColor;
            console.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.console), mainColor);

            foreach (Panel _p in tabPanels)
            {
                _p.Visible = false;
            }
            consoleView.Visible = true;
        }

        private void power_Click(object sender, EventArgs e)
        {
            foreach (Button _b in tabs)
            {
                _b.BackColor = mainColor;
                _b.BackgroundImage = SetImageColor(_b.BackgroundImage, tabColor);
            }
            
            power.BackColor = tabColor;
            power.BackgroundImage = SetImageColor(new Bitmap(global::VXPMenu.Properties.Resources.power), mainColor);

            foreach (Panel _p in tabPanels)
            {
                _p.Visible = false;
            }
            powerView.Visible = true;
        }

        public void OpenApplication(string path, bool isMono)
        {
            MessageBox.Show("Opening program @ " + path + "!");
        }

        public void UpdateTime()
        {
            foreach(Label _l in titleBar.Controls.OfType<Label>())
            {
                _l.ForeColor = textColor;
                System.Timers.Timer _timer = new System.Timers.Timer(500);
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object __s, ElapsedEventArgs __e)
                {
                    if (_l.Name == "dateTime")
                    {
                        _l.Invoke((MethodInvoker)(() =>
                        {
                            _l.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
                            _l.Update();
                        }));
                    }
                });
                _timer.Enabled = true;
            }
        }

        public Image SetImageColor(Image image, Color _color)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    float[][] colorMatrixElements = {
                    new float[] {0, 0, 0, 0, 0},        // red scaling factor of 2 
                    new float[] {0, 0, 0, 0, 0},        // green scaling factor of 1 
                    new float[] {0, 0, 0, 0, 0},        // blue scaling factor of 1 
                    new float[] {0, 0, 0, 1, 0},        // alpha scaling factor of 1 
                    new float[] {_color.R / 127.5f / 2, _color.G / 127.5f / 2, _color.B / 127.5f / 2, 0, 1}};    // three translations of 0.2

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix(colorMatrixElements);

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void WriteToFileOnLine(string path, int line, string content)
        {
            int lineRead = 1;
            StreamReader pathSR = new StreamReader(path);

            List<string[]> readerLines = new List<string[]>();

            string _content;
            while ((_content = pathSR.ReadLine()) != null)
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"((""((?<token>.*?)(?<!\\)"")|(?<token>[\w]+))(\s)*)", options);
                string[] result = (from Match m in regex.Matches(_content)
                              where m.Groups["token"].Success
                              select m.Groups["token"].Value).ToArray<string>();
                readerLines.Add(result);
            }

            foreach(string[] __content in readerLines)
            {
                if(__content[1] == readerLines[lineRead][1])
                {
                    Debug.WriteLine("yay! line " + lineRead + " had it!");
                }
                lineRead++;
            }

            StreamWriter pathSW = new StreamWriter(path);
            pathSW.WriteLine();
        }
    }
}
