﻿namespace ITClassHelper
{
    partial class MainWindow
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
        private void InitializeComponent()
        {
            this.AttackTab = new System.Windows.Forms.TabPage();
            this.ProgramUsingGroup = new System.Windows.Forms.GroupBox();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.PCNameTextBox = new System.Windows.Forms.TextBox();
            this.PCNameLabel = new System.Windows.Forms.Label();
            this.ConvertNameIPButton = new System.Windows.Forms.Button();
            this.InstallPythonButton = new System.Windows.Forms.Button();
            this.ChooseScriptButton = new System.Windows.Forms.Button();
            this.UseScriptRadio = new System.Windows.Forms.RadioButton();
            this.IPButton = new System.Windows.Forms.Button();
            this.AttackButton = new System.Windows.Forms.Button();
            this.UseMsgRadio = new System.Windows.Forms.RadioButton();
            this.MsgTextBox = new System.Windows.Forms.TextBox();
            this.UseCmdRadio = new System.Windows.Forms.RadioButton();
            this.CmdTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.IPRangeTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel3 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.UpdateAppButton = new System.Windows.Forms.Button();
            this.DisableAttackButton = new System.Windows.Forms.Button();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ProgramVerLabel = new System.Windows.Forms.Label();
            this.AppPathTextBox = new System.Windows.Forms.TextBox();
            this.AppPathLabel = new System.Windows.Forms.Label();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.AppStatusGroup = new System.Windows.Forms.GroupBox();
            this.AppStatusLabel = new System.Windows.Forms.Label();
            this.AppStatusButton = new System.Windows.Forms.Button();
            this.HideProgramButton = new System.Windows.Forms.Button();
            this.AppControlGroup = new System.Windows.Forms.GroupBox();
            this.GetPswdButton = new System.Windows.Forms.Button();
            this.RecoverAppButton = new System.Windows.Forms.Button();
            this.CloseAppButton = new System.Windows.Forms.Button();
            this.PauseAppButton = new System.Windows.Forms.Button();
            this.ExitProgramButton = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.AttackTab.SuspendLayout();
            this.ProgramUsingGroup.SuspendLayout();
            this.SettingsTab.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.AppStatusGroup.SuspendLayout();
            this.AppControlGroup.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // AttackTab
            // 
            this.AttackTab.Controls.Add(this.ProgramUsingGroup);
            this.AttackTab.Location = new System.Drawing.Point(4, 29);
            this.AttackTab.Name = "AttackTab";
            this.AttackTab.Padding = new System.Windows.Forms.Padding(3);
            this.AttackTab.Size = new System.Drawing.Size(389, 480);
            this.AttackTab.TabIndex = 3;
            this.AttackTab.Text = "网络攻击";
            this.AttackTab.UseVisualStyleBackColor = true;
            // 
            // ProgramUsingGroup
            // 
            this.ProgramUsingGroup.Controls.Add(this.ConvertButton);
            this.ProgramUsingGroup.Controls.Add(this.PCNameTextBox);
            this.ProgramUsingGroup.Controls.Add(this.PCNameLabel);
            this.ProgramUsingGroup.Controls.Add(this.ConvertNameIPButton);
            this.ProgramUsingGroup.Controls.Add(this.InstallPythonButton);
            this.ProgramUsingGroup.Controls.Add(this.ChooseScriptButton);
            this.ProgramUsingGroup.Controls.Add(this.UseScriptRadio);
            this.ProgramUsingGroup.Controls.Add(this.IPButton);
            this.ProgramUsingGroup.Controls.Add(this.AttackButton);
            this.ProgramUsingGroup.Controls.Add(this.UseMsgRadio);
            this.ProgramUsingGroup.Controls.Add(this.MsgTextBox);
            this.ProgramUsingGroup.Controls.Add(this.UseCmdRadio);
            this.ProgramUsingGroup.Controls.Add(this.CmdTextBox);
            this.ProgramUsingGroup.Controls.Add(this.PortTextBox);
            this.ProgramUsingGroup.Controls.Add(this.PortLabel);
            this.ProgramUsingGroup.Controls.Add(this.IPRangeTextBox);
            this.ProgramUsingGroup.Controls.Add(this.IPLabel3);
            this.ProgramUsingGroup.Controls.Add(this.IPTextBox);
            this.ProgramUsingGroup.Location = new System.Drawing.Point(4, 4);
            this.ProgramUsingGroup.Name = "ProgramUsingGroup";
            this.ProgramUsingGroup.Size = new System.Drawing.Size(381, 470);
            this.ProgramUsingGroup.TabIndex = 0;
            this.ProgramUsingGroup.TabStop = false;
            this.ProgramUsingGroup.Text = "极域利用";
            // 
            // ConvertButton
            // 
            this.ConvertButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConvertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ConvertButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertButton.Location = new System.Drawing.Point(11, 372);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(160, 31);
            this.ConvertButton.TabIndex = 25;
            this.ConvertButton.Text = "立即转换";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Visible = false;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // PCNameTextBox
            // 
            this.PCNameTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PCNameTextBox.Location = new System.Drawing.Point(101, 294);
            this.PCNameTextBox.Name = "PCNameTextBox";
            this.PCNameTextBox.Size = new System.Drawing.Size(270, 29);
            this.PCNameTextBox.TabIndex = 24;
            this.PCNameTextBox.Text = "1-1";
            this.PCNameTextBox.Visible = false;
            // 
            // PCNameLabel
            // 
            this.PCNameLabel.AutoSize = true;
            this.PCNameLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PCNameLabel.Location = new System.Drawing.Point(7, 297);
            this.PCNameLabel.Name = "PCNameLabel";
            this.PCNameLabel.Size = new System.Drawing.Size(78, 23);
            this.PCNameLabel.TabIndex = 23;
            this.PCNameLabel.Text = "计算机名";
            this.PCNameLabel.Visible = false;
            // 
            // ConvertNameIPButton
            // 
            this.ConvertNameIPButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConvertNameIPButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ConvertNameIPButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertNameIPButton.Location = new System.Drawing.Point(177, 372);
            this.ConvertNameIPButton.Name = "ConvertNameIPButton";
            this.ConvertNameIPButton.Size = new System.Drawing.Size(194, 31);
            this.ConvertNameIPButton.TabIndex = 22;
            this.ConvertNameIPButton.Text = "计算机名 -> IP 地址";
            this.ConvertNameIPButton.UseVisualStyleBackColor = true;
            this.ConvertNameIPButton.Click += new System.EventHandler(this.ConvertNameIPButton_Click);
            // 
            // InstallPythonButton
            // 
            this.InstallPythonButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InstallPythonButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.InstallPythonButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InstallPythonButton.Location = new System.Drawing.Point(11, 372);
            this.InstallPythonButton.Name = "InstallPythonButton";
            this.InstallPythonButton.Size = new System.Drawing.Size(160, 31);
            this.InstallPythonButton.TabIndex = 21;
            this.InstallPythonButton.Text = "安装运行环境";
            this.InstallPythonButton.UseVisualStyleBackColor = true;
            this.InstallPythonButton.Click += new System.EventHandler(this.InstallPythonButton_Click);
            // 
            // ChooseScriptButton
            // 
            this.ChooseScriptButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseScriptButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChooseScriptButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChooseScriptButton.Location = new System.Drawing.Point(252, 257);
            this.ChooseScriptButton.Name = "ChooseScriptButton";
            this.ChooseScriptButton.Size = new System.Drawing.Size(119, 31);
            this.ChooseScriptButton.TabIndex = 20;
            this.ChooseScriptButton.Text = "选择脚本";
            this.ChooseScriptButton.UseVisualStyleBackColor = true;
            this.ChooseScriptButton.Click += new System.EventHandler(this.ChooseScriptButton_Click);
            // 
            // UseScriptRadio
            // 
            this.UseScriptRadio.AutoSize = true;
            this.UseScriptRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UseScriptRadio.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UseScriptRadio.Location = new System.Drawing.Point(11, 257);
            this.UseScriptRadio.Name = "UseScriptRadio";
            this.UseScriptRadio.Size = new System.Drawing.Size(74, 28);
            this.UseScriptRadio.TabIndex = 19;
            this.UseScriptRadio.Text = "脚本";
            this.UseScriptRadio.UseVisualStyleBackColor = true;
            this.UseScriptRadio.CheckedChanged += new System.EventHandler(this.AttackTypeRadio_CheckedChanged);
            // 
            // IPButton
            // 
            this.IPButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IPButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.IPButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPButton.Location = new System.Drawing.Point(6, 26);
            this.IPButton.Name = "IPButton";
            this.IPButton.Size = new System.Drawing.Size(79, 31);
            this.IPButton.TabIndex = 15;
            this.IPButton.Text = "IP 地址";
            this.IPButton.UseVisualStyleBackColor = true;
            this.IPButton.Click += new System.EventHandler(this.IPButton_Click);
            // 
            // AttackButton
            // 
            this.AttackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AttackButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AttackButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AttackButton.Location = new System.Drawing.Point(11, 409);
            this.AttackButton.Name = "AttackButton";
            this.AttackButton.Size = new System.Drawing.Size(360, 55);
            this.AttackButton.TabIndex = 14;
            this.AttackButton.Text = "立即攻击";
            this.AttackButton.UseVisualStyleBackColor = true;
            this.AttackButton.Click += new System.EventHandler(this.AttackButton_Click);
            // 
            // UseMsgRadio
            // 
            this.UseMsgRadio.AutoSize = true;
            this.UseMsgRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UseMsgRadio.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UseMsgRadio.Location = new System.Drawing.Point(11, 134);
            this.UseMsgRadio.Name = "UseMsgRadio";
            this.UseMsgRadio.Size = new System.Drawing.Size(74, 28);
            this.UseMsgRadio.TabIndex = 13;
            this.UseMsgRadio.Text = "消息";
            this.UseMsgRadio.UseVisualStyleBackColor = true;
            this.UseMsgRadio.CheckedChanged += new System.EventHandler(this.AttackTypeRadio_CheckedChanged);
            // 
            // MsgTextBox
            // 
            this.MsgTextBox.Enabled = false;
            this.MsgTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MsgTextBox.Location = new System.Drawing.Point(101, 133);
            this.MsgTextBox.Multiline = true;
            this.MsgTextBox.Name = "MsgTextBox";
            this.MsgTextBox.Size = new System.Drawing.Size(270, 117);
            this.MsgTextBox.TabIndex = 12;
            this.MsgTextBox.Text = "老师正在监视您的屏幕！";
            // 
            // UseCmdRadio
            // 
            this.UseCmdRadio.AutoSize = true;
            this.UseCmdRadio.Checked = true;
            this.UseCmdRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UseCmdRadio.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UseCmdRadio.Location = new System.Drawing.Point(11, 99);
            this.UseCmdRadio.Name = "UseCmdRadio";
            this.UseCmdRadio.Size = new System.Drawing.Size(74, 28);
            this.UseCmdRadio.TabIndex = 11;
            this.UseCmdRadio.TabStop = true;
            this.UseCmdRadio.Text = "命令";
            this.UseCmdRadio.UseVisualStyleBackColor = true;
            this.UseCmdRadio.CheckedChanged += new System.EventHandler(this.AttackTypeRadio_CheckedChanged);
            // 
            // CmdTextBox
            // 
            this.CmdTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CmdTextBox.Location = new System.Drawing.Point(101, 98);
            this.CmdTextBox.Name = "CmdTextBox";
            this.CmdTextBox.Size = new System.Drawing.Size(270, 29);
            this.CmdTextBox.TabIndex = 10;
            this.CmdTextBox.Text = "shutdown -s -t 0";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PortTextBox.Location = new System.Drawing.Point(284, 63);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(87, 29);
            this.PortTextBox.TabIndex = 8;
            this.PortTextBox.Text = "4605";
            this.PortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PortLabel.Location = new System.Drawing.Point(7, 66);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(44, 23);
            this.PortLabel.TabIndex = 7;
            this.PortLabel.Text = "端口";
            // 
            // IPRangeTextBox
            // 
            this.IPRangeTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPRangeTextBox.Location = new System.Drawing.Point(326, 28);
            this.IPRangeTextBox.Name = "IPRangeTextBox";
            this.IPRangeTextBox.Size = new System.Drawing.Size(45, 29);
            this.IPRangeTextBox.TabIndex = 6;
            this.IPRangeTextBox.Text = "1";
            this.IPRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IPLabel3
            // 
            this.IPLabel3.AutoSize = true;
            this.IPLabel3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPLabel3.Location = new System.Drawing.Point(303, 34);
            this.IPLabel3.Name = "IPLabel3";
            this.IPLabel3.Size = new System.Drawing.Size(17, 23);
            this.IPLabel3.TabIndex = 5;
            this.IPLabel3.Text = "-";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPTextBox.Location = new System.Drawing.Point(101, 28);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(196, 29);
            this.IPTextBox.TabIndex = 2;
            this.IPTextBox.Text = "192.168.0.1";
            this.IPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SettingsTab
            // 
            this.SettingsTab.BackColor = System.Drawing.Color.Transparent;
            this.SettingsTab.Controls.Add(this.UpdateAppButton);
            this.SettingsTab.Controls.Add(this.DisableAttackButton);
            this.SettingsTab.Controls.Add(this.AuthorLabel);
            this.SettingsTab.Controls.Add(this.ProgramVerLabel);
            this.SettingsTab.Controls.Add(this.AppPathTextBox);
            this.SettingsTab.Controls.Add(this.AppPathLabel);
            this.SettingsTab.Location = new System.Drawing.Point(4, 29);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(389, 480);
            this.SettingsTab.TabIndex = 1;
            this.SettingsTab.Text = "程序设置";
            // 
            // UpdateAppButton
            // 
            this.UpdateAppButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateAppButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UpdateAppButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpdateAppButton.Location = new System.Drawing.Point(8, 151);
            this.UpdateAppButton.Name = "UpdateAppButton";
            this.UpdateAppButton.Size = new System.Drawing.Size(374, 35);
            this.UpdateAppButton.TabIndex = 19;
            this.UpdateAppButton.Text = "更新软件";
            this.UpdateAppButton.UseVisualStyleBackColor = true;
            this.UpdateAppButton.Click += new System.EventHandler(this.UpdateAppButton_Click);
            // 
            // DisableAttackButton
            // 
            this.DisableAttackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisableAttackButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DisableAttackButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisableAttackButton.Location = new System.Drawing.Point(7, 110);
            this.DisableAttackButton.Name = "DisableAttackButton";
            this.DisableAttackButton.Size = new System.Drawing.Size(374, 35);
            this.DisableAttackButton.TabIndex = 15;
            this.DisableAttackButton.Text = "禁用网络攻击";
            this.DisableAttackButton.UseVisualStyleBackColor = true;
            this.DisableAttackButton.Click += new System.EventHandler(this.DisableAttackButton_Click);
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(129, 430);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(113, 20);
            this.AuthorLabel.TabIndex = 5;
            this.AuthorLabel.Text = "作者：Ujhhgtg";
            // 
            // ProgramVerLabel
            // 
            this.ProgramVerLabel.AutoSize = true;
            this.ProgramVerLabel.Location = new System.Drawing.Point(131, 450);
            this.ProgramVerLabel.Name = "ProgramVerLabel";
            this.ProgramVerLabel.Size = new System.Drawing.Size(69, 20);
            this.ProgramVerLabel.TabIndex = 4;
            this.ProgramVerLabel.Text = "版本号：";
            // 
            // AppPathTextBox
            // 
            this.AppPathTextBox.Location = new System.Drawing.Point(135, 6);
            this.AppPathTextBox.Multiline = true;
            this.AppPathTextBox.Name = "AppPathTextBox";
            this.AppPathTextBox.Size = new System.Drawing.Size(246, 98);
            this.AppPathTextBox.TabIndex = 3;
            // 
            // AppPathLabel
            // 
            this.AppPathLabel.AutoSize = true;
            this.AppPathLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AppPathLabel.Location = new System.Drawing.Point(3, 6);
            this.AppPathLabel.Name = "AppPathLabel";
            this.AppPathLabel.Size = new System.Drawing.Size(124, 46);
            this.AppPathLabel.TabIndex = 2;
            this.AppPathLabel.Text = "极域程序路径\r\n(默认无需指定)";
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.AppStatusGroup);
            this.MainTab.Controls.Add(this.HideProgramButton);
            this.MainTab.Controls.Add(this.AppControlGroup);
            this.MainTab.Controls.Add(this.ExitProgramButton);
            this.MainTab.Location = new System.Drawing.Point(4, 29);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(389, 480);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "主界面";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // AppStatusGroup
            // 
            this.AppStatusGroup.Controls.Add(this.AppStatusLabel);
            this.AppStatusGroup.Controls.Add(this.AppStatusButton);
            this.AppStatusGroup.Location = new System.Drawing.Point(6, 7);
            this.AppStatusGroup.Name = "AppStatusGroup";
            this.AppStatusGroup.Size = new System.Drawing.Size(379, 86);
            this.AppStatusGroup.TabIndex = 3;
            this.AppStatusGroup.TabStop = false;
            this.AppStatusGroup.Text = "程序状态";
            // 
            // AppStatusLabel
            // 
            this.AppStatusLabel.AutoSize = true;
            this.AppStatusLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AppStatusLabel.Location = new System.Drawing.Point(150, 41);
            this.AppStatusLabel.Name = "AppStatusLabel";
            this.AppStatusLabel.Size = new System.Drawing.Size(78, 23);
            this.AppStatusLabel.TabIndex = 7;
            this.AppStatusLabel.Text = "状态未知";
            // 
            // AppStatusButton
            // 
            this.AppStatusButton.Enabled = false;
            this.AppStatusButton.Location = new System.Drawing.Point(7, 27);
            this.AppStatusButton.Name = "AppStatusButton";
            this.AppStatusButton.Size = new System.Drawing.Size(366, 53);
            this.AppStatusButton.TabIndex = 0;
            this.AppStatusButton.UseVisualStyleBackColor = true;
            // 
            // HideProgramButton
            // 
            this.HideProgramButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideProgramButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.HideProgramButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HideProgramButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideProgramButton.Location = new System.Drawing.Point(8, 423);
            this.HideProgramButton.Name = "HideProgramButton";
            this.HideProgramButton.Size = new System.Drawing.Size(183, 49);
            this.HideProgramButton.TabIndex = 2;
            this.HideProgramButton.Text = "隐藏";
            this.HideProgramButton.UseVisualStyleBackColor = true;
            this.HideProgramButton.Click += new System.EventHandler(this.HideProgramButton_Click);
            // 
            // AppControlGroup
            // 
            this.AppControlGroup.Controls.Add(this.GetPswdButton);
            this.AppControlGroup.Controls.Add(this.RecoverAppButton);
            this.AppControlGroup.Controls.Add(this.CloseAppButton);
            this.AppControlGroup.Controls.Add(this.PauseAppButton);
            this.AppControlGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AppControlGroup.Location = new System.Drawing.Point(6, 99);
            this.AppControlGroup.Name = "AppControlGroup";
            this.AppControlGroup.Size = new System.Drawing.Size(379, 318);
            this.AppControlGroup.TabIndex = 0;
            this.AppControlGroup.TabStop = false;
            this.AppControlGroup.Text = "程序控制";
            // 
            // GetPswdButton
            // 
            this.GetPswdButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetPswdButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GetPswdButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GetPswdButton.Location = new System.Drawing.Point(8, 245);
            this.GetPswdButton.Name = "GetPswdButton";
            this.GetPswdButton.Size = new System.Drawing.Size(367, 67);
            this.GetPswdButton.TabIndex = 6;
            this.GetPswdButton.Text = "读取密码";
            this.GetPswdButton.UseVisualStyleBackColor = true;
            this.GetPswdButton.Click += new System.EventHandler(this.GetPswd_Click);
            // 
            // RecoverAppButton
            // 
            this.RecoverAppButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecoverAppButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RecoverAppButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RecoverAppButton.Location = new System.Drawing.Point(7, 172);
            this.RecoverAppButton.Name = "RecoverAppButton";
            this.RecoverAppButton.Size = new System.Drawing.Size(367, 67);
            this.RecoverAppButton.TabIndex = 5;
            this.RecoverAppButton.Text = "恢复极域";
            this.RecoverAppButton.UseVisualStyleBackColor = true;
            this.RecoverAppButton.Click += new System.EventHandler(this.RecoverApp_Click);
            // 
            // CloseAppButton
            // 
            this.CloseAppButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseAppButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CloseAppButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseAppButton.Location = new System.Drawing.Point(8, 26);
            this.CloseAppButton.Name = "CloseAppButton";
            this.CloseAppButton.Size = new System.Drawing.Size(367, 67);
            this.CloseAppButton.TabIndex = 4;
            this.CloseAppButton.Text = "关闭极域";
            this.CloseAppButton.UseVisualStyleBackColor = true;
            this.CloseAppButton.Click += new System.EventHandler(this.CloseApp_Click);
            // 
            // PauseAppButton
            // 
            this.PauseAppButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PauseAppButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PauseAppButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PauseAppButton.Location = new System.Drawing.Point(8, 99);
            this.PauseAppButton.Name = "PauseAppButton";
            this.PauseAppButton.Size = new System.Drawing.Size(367, 67);
            this.PauseAppButton.TabIndex = 3;
            this.PauseAppButton.Text = "挂起极域";
            this.PauseAppButton.UseVisualStyleBackColor = true;
            this.PauseAppButton.Click += new System.EventHandler(this.PauseApp_Click);
            // 
            // ExitProgramButton
            // 
            this.ExitProgramButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitProgramButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitProgramButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExitProgramButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExitProgramButton.Location = new System.Drawing.Point(193, 423);
            this.ExitProgramButton.Name = "ExitProgramButton";
            this.ExitProgramButton.Size = new System.Drawing.Size(192, 49);
            this.ExitProgramButton.TabIndex = 0;
            this.ExitProgramButton.Text = "退出";
            this.ExitProgramButton.UseVisualStyleBackColor = true;
            this.ExitProgramButton.Click += new System.EventHandler(this.ExitProgram_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.MainTab);
            this.TabControl.Controls.Add(this.SettingsTab);
            this.TabControl.Controls.Add(this.AttackTab);
            this.TabControl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(397, 513);
            this.TabControl.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AcceptButton = this.CloseAppButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.HideProgramButton;
            this.ClientSize = new System.Drawing.Size(397, 513);
            this.Controls.Add(this.TabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机房助手";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.AttackTab.ResumeLayout(false);
            this.ProgramUsingGroup.ResumeLayout(false);
            this.ProgramUsingGroup.PerformLayout();
            this.SettingsTab.ResumeLayout(false);
            this.SettingsTab.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.AppStatusGroup.ResumeLayout(false);
            this.AppStatusGroup.PerformLayout();
            this.AppControlGroup.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage AttackTab;
        private System.Windows.Forms.GroupBox ProgramUsingGroup;
        private System.Windows.Forms.Button IPButton;
        private System.Windows.Forms.Button AttackButton;
        private System.Windows.Forms.RadioButton UseMsgRadio;
        private System.Windows.Forms.TextBox MsgTextBox;
        private System.Windows.Forms.RadioButton UseCmdRadio;
        private System.Windows.Forms.TextBox CmdTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox IPRangeTextBox;
        private System.Windows.Forms.Label IPLabel3;
        private System.Windows.Forms.TabPage SettingsTab;
        private System.Windows.Forms.Button UpdateAppButton;
        private System.Windows.Forms.Button DisableAttackButton;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label ProgramVerLabel;
        private System.Windows.Forms.TextBox AppPathTextBox;
        private System.Windows.Forms.Label AppPathLabel;
        private System.Windows.Forms.TabPage MainTab;
        private System.Windows.Forms.GroupBox AppStatusGroup;
        private System.Windows.Forms.Label AppStatusLabel;
        private System.Windows.Forms.Button AppStatusButton;
        private System.Windows.Forms.Button HideProgramButton;
        private System.Windows.Forms.GroupBox AppControlGroup;
        private System.Windows.Forms.Button GetPswdButton;
        private System.Windows.Forms.Button RecoverAppButton;
        private System.Windows.Forms.Button CloseAppButton;
        private System.Windows.Forms.Button PauseAppButton;
        private System.Windows.Forms.Button ExitProgramButton;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.RadioButton UseScriptRadio;
        private System.Windows.Forms.Button ChooseScriptButton;
        private System.Windows.Forms.Button InstallPythonButton;
        private System.Windows.Forms.Button ConvertNameIPButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.TextBox PCNameTextBox;
        private System.Windows.Forms.Label PCNameLabel;
        private System.Windows.Forms.TextBox IPTextBox;
    }
}