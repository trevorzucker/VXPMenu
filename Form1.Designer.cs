namespace VXPMenu
{
    partial class Form1
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
        /// 
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gameView = new System.Windows.Forms.Panel();
            this.emulatorView = new System.Windows.Forms.Panel();
            this.settingView = new System.Windows.Forms.Panel();
            this.consoleView = new System.Windows.Forms.Panel();
            this.powerView = new System.Windows.Forms.Panel();
            this.titleBar = new System.Windows.Forms.Panel();
            this.power = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Button();
            this.emulators = new System.Windows.Forms.Button();
            this.games = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameView
            // 
            this.gameView.AutoScroll = true;
            this.gameView.BackColor = System.Drawing.Color.LightGray;
            this.gameView.Location = new System.Drawing.Point(0, 0);
            this.gameView.Name = "gameView";
            this.gameView.Size = new System.Drawing.Size(200, 100);
            this.gameView.TabIndex = 12;
            this.gameView.Visible = false;
            // 
            // emulatorView
            // 
            this.emulatorView.BackColor = System.Drawing.Color.LightGray;
            this.emulatorView.Location = new System.Drawing.Point(0, 0);
            this.emulatorView.Name = "emulatorView";
            this.emulatorView.Size = new System.Drawing.Size(200, 100);
            this.emulatorView.TabIndex = 13;
            this.emulatorView.Visible = false;
            // 
            // settingView
            // 
            this.settingView.BackColor = System.Drawing.Color.LightGray;
            this.settingView.Location = new System.Drawing.Point(0, 0);
            this.settingView.Name = "settingView";
            this.settingView.Size = new System.Drawing.Size(200, 100);
            this.settingView.TabIndex = 13;
            this.settingView.Visible = false;
            // 
            // consoleView
            // 
            this.consoleView.BackColor = System.Drawing.Color.LightGray;
            this.consoleView.Location = new System.Drawing.Point(0, 0);
            this.consoleView.Name = "consoleView";
            this.consoleView.Size = new System.Drawing.Size(200, 100);
            this.consoleView.TabIndex = 13;
            this.consoleView.Visible = false;
            // 
            // powerView
            // 
            this.powerView.BackColor = System.Drawing.Color.LightGray;
            this.powerView.Location = new System.Drawing.Point(0, 0);
            this.powerView.Name = "powerView";
            this.powerView.Size = new System.Drawing.Size(200, 100);
            this.powerView.TabIndex = 13;
            this.powerView.Visible = false;
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.LightGray;
            this.titleBar.Location = new System.Drawing.Point(94, 12);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(325, 171);
            this.titleBar.TabIndex = 14;
            // 
            // power
            // 
            this.power.BackColor = System.Drawing.Color.White;
            this.power.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.power.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.power.FlatAppearance.BorderSize = 0;
            this.power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.power.Location = new System.Drawing.Point(165, 174);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(75, 101);
            this.power.TabIndex = 9;
            this.power.UseVisualStyleBackColor = false;
            this.power.Click += new System.EventHandler(this.power_Click);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.Color.White;
            this.console.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.console.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.console.FlatAppearance.BorderSize = 0;
            this.console.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.console.Location = new System.Drawing.Point(344, 175);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(75, 100);
            this.console.TabIndex = 8;
            this.console.UseVisualStyleBackColor = false;
            this.console.Click += new System.EventHandler(this.console_Click);
            // 
            // settings
            // 
            this.settings.BackColor = System.Drawing.Color.White;
            this.settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settings.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.settings.FlatAppearance.BorderSize = 0;
            this.settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settings.Location = new System.Drawing.Point(263, 142);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(75, 133);
            this.settings.TabIndex = 7;
            this.settings.UseVisualStyleBackColor = false;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // emulators
            // 
            this.emulators.BackColor = System.Drawing.Color.White;
            this.emulators.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.emulators.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.emulators.FlatAppearance.BorderSize = 0;
            this.emulators.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emulators.Location = new System.Drawing.Point(81, 147);
            this.emulators.Name = "emulators";
            this.emulators.Size = new System.Drawing.Size(75, 128);
            this.emulators.TabIndex = 6;
            this.emulators.UseVisualStyleBackColor = false;
            this.emulators.Click += new System.EventHandler(this.emulators_Click);
            // 
            // games
            // 
            this.games.BackColor = System.Drawing.Color.White;
            this.games.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.games.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.games.FlatAppearance.BorderSize = 0;
            this.games.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.games.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.games.Location = new System.Drawing.Point(0, 12);
            this.games.Name = "games";
            this.games.Size = new System.Drawing.Size(75, 156);
            this.games.TabIndex = 5;
            this.games.UseVisualStyleBackColor = false;
            this.games.Click += new System.EventHandler(this.games_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(304, 201);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.powerView);
            this.Controls.Add(this.consoleView);
            this.Controls.Add(this.settingView);
            this.Controls.Add(this.emulatorView);
            this.Controls.Add(this.gameView);
            this.Controls.Add(this.power);
            this.Controls.Add(this.console);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.emulators);
            this.Controls.Add(this.games);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 50);
            this.MaximumSize = new System.Drawing.Size(320, 240);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button games;
        private System.Windows.Forms.Button emulators;
        private System.Windows.Forms.Button settings;
        private System.Windows.Forms.Button console;
        private System.Windows.Forms.Button power;
        private System.Windows.Forms.Panel gameView;
        private System.Windows.Forms.Panel emulatorView;
        private System.Windows.Forms.Panel settingView;
        private System.Windows.Forms.Panel consoleView;
        private System.Windows.Forms.Panel powerView;
        private System.Windows.Forms.Panel titleBar;
    }
}

