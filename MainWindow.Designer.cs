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
            this.RoomFakingGroup = new System.Windows.Forms.GroupBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PCNameTextBox = new System.Windows.Forms.TextBox();
            this.ConvertButton = new System.Windows.Forms.Button();
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
            this.IPRangeTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel3 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.UpdateProgramButton = new System.Windows.Forms.Button();
            this.DisableAttackButton = new System.Windows.Forms.Button();
            this.RoomStatusGroup = new System.Windows.Forms.GroupBox();
            this.RoomStatusLabel = new System.Windows.Forms.Label();
            this.RoomStatusButton = new System.Windows.Forms.Button();
            this.RoomControlGroup = new System.Windows.Forms.GroupBox();
            this.RecoverRoomButton = new System.Windows.Forms.Button();
            this.CloseRoomButton = new System.Windows.Forms.Button();
            this.PauseRoomButton = new System.Windows.Forms.Button();
            this.ProgramSettingsGroup = new System.Windows.Forms.GroupBox();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ProgramVerLabel = new System.Windows.Forms.Label();
            this.ChooseRoomButton = new System.Windows.Forms.Button();
            this.PswdGroup = new System.Windows.Forms.GroupBox();
            this.GetPswdButton = new System.Windows.Forms.Button();
            this.SetPswdButton = new System.Windows.Forms.Button();
            this.ScripterButton = new System.Windows.Forms.Button();
            this.RoomFakingGroup.SuspendLayout();
            this.RoomStatusGroup.SuspendLayout();
            this.RoomControlGroup.SuspendLayout();
            this.ProgramSettingsGroup.SuspendLayout();
            this.PswdGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomFakingGroup
            // 
            this.RoomFakingGroup.Controls.Add(this.ScripterButton);
            this.RoomFakingGroup.Controls.Add(this.PortLabel);
            this.RoomFakingGroup.Controls.Add(this.PCNameTextBox);
            this.RoomFakingGroup.Controls.Add(this.ConvertButton);
            this.RoomFakingGroup.Controls.Add(this.InstallPythonButton);
            this.RoomFakingGroup.Controls.Add(this.ChooseScriptButton);
            this.RoomFakingGroup.Controls.Add(this.UseScriptRadio);
            this.RoomFakingGroup.Controls.Add(this.IPButton);
            this.RoomFakingGroup.Controls.Add(this.AttackButton);
            this.RoomFakingGroup.Controls.Add(this.UseMsgRadio);
            this.RoomFakingGroup.Controls.Add(this.MsgTextBox);
            this.RoomFakingGroup.Controls.Add(this.UseCmdRadio);
            this.RoomFakingGroup.Controls.Add(this.CmdTextBox);
            this.RoomFakingGroup.Controls.Add(this.PortTextBox);
            this.RoomFakingGroup.Controls.Add(this.IPRangeTextBox);
            this.RoomFakingGroup.Controls.Add(this.IPLabel3);
            this.RoomFakingGroup.Controls.Add(this.IPTextBox);
            this.RoomFakingGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RoomFakingGroup.Location = new System.Drawing.Point(299, 7);
            this.RoomFakingGroup.Name = "RoomFakingGroup";
            this.RoomFakingGroup.Size = new System.Drawing.Size(493, 491);
            this.RoomFakingGroup.TabIndex = 16;
            this.RoomFakingGroup.TabStop = false;
            this.RoomFakingGroup.Text = "教室利用";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PortLabel.Location = new System.Drawing.Point(380, 33);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(14, 23);
            this.PortLabel.TabIndex = 18;
            this.PortLabel.Text = ":";
            // 
            // PCNameTextBox
            // 
            this.PCNameTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PCNameTextBox.Location = new System.Drawing.Point(206, 261);
            this.PCNameTextBox.Name = "PCNameTextBox";
            this.PCNameTextBox.Size = new System.Drawing.Size(281, 29);
            this.PCNameTextBox.TabIndex = 27;
            this.PCNameTextBox.Text = "1-1";
            // 
            // ConvertButton
            // 
            this.ConvertButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConvertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ConvertButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertButton.Location = new System.Drawing.Point(6, 260);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(194, 31);
            this.ConvertButton.TabIndex = 26;
            this.ConvertButton.Text = "计算机名 -> IP 地址";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // InstallPythonButton
            // 
            this.InstallPythonButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InstallPythonButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.InstallPythonButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InstallPythonButton.Location = new System.Drawing.Point(372, 427);
            this.InstallPythonButton.Name = "InstallPythonButton";
            this.InstallPythonButton.Size = new System.Drawing.Size(115, 55);
            this.InstallPythonButton.TabIndex = 28;
            this.InstallPythonButton.Text = "安装\r\n运行环境";
            this.InstallPythonButton.UseVisualStyleBackColor = true;
            this.InstallPythonButton.Click += new System.EventHandler(this.InstallPythonButton_Click);
            // 
            // ChooseScriptButton
            // 
            this.ChooseScriptButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseScriptButton.Enabled = false;
            this.ChooseScriptButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChooseScriptButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChooseScriptButton.Location = new System.Drawing.Point(329, 224);
            this.ChooseScriptButton.Name = "ChooseScriptButton";
            this.ChooseScriptButton.Size = new System.Drawing.Size(158, 31);
            this.ChooseScriptButton.TabIndex = 25;
            this.ChooseScriptButton.Text = "选择脚本文件";
            this.ChooseScriptButton.UseVisualStyleBackColor = true;
            this.ChooseScriptButton.Click += new System.EventHandler(this.ChooseScriptButton_Click);
            // 
            // UseScriptRadio
            // 
            this.UseScriptRadio.AutoSize = true;
            this.UseScriptRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UseScriptRadio.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UseScriptRadio.Location = new System.Drawing.Point(11, 224);
            this.UseScriptRadio.Name = "UseScriptRadio";
            this.UseScriptRadio.Size = new System.Drawing.Size(74, 28);
            this.UseScriptRadio.TabIndex = 24;
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
            this.IPButton.TabIndex = 17;
            this.IPButton.Text = "IP 地址";
            this.IPButton.UseVisualStyleBackColor = true;
            this.IPButton.Click += new System.EventHandler(this.IPButton_Click);
            // 
            // AttackButton
            // 
            this.AttackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AttackButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AttackButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AttackButton.Location = new System.Drawing.Point(6, 427);
            this.AttackButton.Name = "AttackButton";
            this.AttackButton.Size = new System.Drawing.Size(360, 55);
            this.AttackButton.TabIndex = 29;
            this.AttackButton.Text = "立即攻击";
            this.AttackButton.UseVisualStyleBackColor = true;
            this.AttackButton.Click += new System.EventHandler(this.AttackButton_Click);
            // 
            // UseMsgRadio
            // 
            this.UseMsgRadio.AutoSize = true;
            this.UseMsgRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UseMsgRadio.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UseMsgRadio.Location = new System.Drawing.Point(11, 98);
            this.UseMsgRadio.Name = "UseMsgRadio";
            this.UseMsgRadio.Size = new System.Drawing.Size(74, 28);
            this.UseMsgRadio.TabIndex = 22;
            this.UseMsgRadio.Text = "消息";
            this.UseMsgRadio.UseVisualStyleBackColor = true;
            this.UseMsgRadio.CheckedChanged += new System.EventHandler(this.AttackTypeRadio_CheckedChanged);
            // 
            // MsgTextBox
            // 
            this.MsgTextBox.Enabled = false;
            this.MsgTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MsgTextBox.Location = new System.Drawing.Point(104, 97);
            this.MsgTextBox.Multiline = true;
            this.MsgTextBox.Name = "MsgTextBox";
            this.MsgTextBox.Size = new System.Drawing.Size(383, 121);
            this.MsgTextBox.TabIndex = 23;
            this.MsgTextBox.Text = "BOO！！！";
            // 
            // UseCmdRadio
            // 
            this.UseCmdRadio.AutoSize = true;
            this.UseCmdRadio.Checked = true;
            this.UseCmdRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UseCmdRadio.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UseCmdRadio.Location = new System.Drawing.Point(11, 62);
            this.UseCmdRadio.Name = "UseCmdRadio";
            this.UseCmdRadio.Size = new System.Drawing.Size(74, 28);
            this.UseCmdRadio.TabIndex = 20;
            this.UseCmdRadio.TabStop = true;
            this.UseCmdRadio.Text = "命令";
            this.UseCmdRadio.UseVisualStyleBackColor = true;
            this.UseCmdRadio.CheckedChanged += new System.EventHandler(this.AttackTypeRadio_CheckedChanged);
            // 
            // CmdTextBox
            // 
            this.CmdTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CmdTextBox.Location = new System.Drawing.Point(104, 62);
            this.CmdTextBox.Name = "CmdTextBox";
            this.CmdTextBox.Size = new System.Drawing.Size(383, 29);
            this.CmdTextBox.TabIndex = 21;
            this.CmdTextBox.Text = "shutdown -s -t 0";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PortTextBox.Location = new System.Drawing.Point(400, 27);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(87, 29);
            this.PortTextBox.TabIndex = 19;
            this.PortTextBox.Text = "4605";
            this.PortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IPRangeTextBox
            // 
            this.IPRangeTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPRangeTextBox.Location = new System.Drawing.Point(329, 27);
            this.IPRangeTextBox.Name = "IPRangeTextBox";
            this.IPRangeTextBox.Size = new System.Drawing.Size(45, 29);
            this.IPRangeTextBox.TabIndex = 17;
            this.IPRangeTextBox.Text = "1";
            this.IPRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IPLabel3
            // 
            this.IPLabel3.AutoSize = true;
            this.IPLabel3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPLabel3.Location = new System.Drawing.Point(306, 33);
            this.IPLabel3.Name = "IPLabel3";
            this.IPLabel3.Size = new System.Drawing.Size(17, 23);
            this.IPLabel3.TabIndex = 16;
            this.IPLabel3.Text = "-";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPTextBox.Location = new System.Drawing.Point(104, 27);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(196, 29);
            this.IPTextBox.TabIndex = 15;
            this.IPTextBox.Text = "192.168.0.1";
            this.IPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UpdateProgramButton
            // 
            this.UpdateProgramButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateProgramButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UpdateProgramButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpdateProgramButton.Location = new System.Drawing.Point(8, 26);
            this.UpdateProgramButton.Name = "UpdateProgramButton";
            this.UpdateProgramButton.Size = new System.Drawing.Size(267, 35);
            this.UpdateProgramButton.TabIndex = 11;
            this.UpdateProgramButton.Text = "更新软件";
            this.UpdateProgramButton.UseVisualStyleBackColor = true;
            this.UpdateProgramButton.Click += new System.EventHandler(this.UpdateProgramButton_Click);
            // 
            // DisableAttackButton
            // 
            this.DisableAttackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisableAttackButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DisableAttackButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisableAttackButton.Location = new System.Drawing.Point(8, 67);
            this.DisableAttackButton.Name = "DisableAttackButton";
            this.DisableAttackButton.Size = new System.Drawing.Size(267, 35);
            this.DisableAttackButton.TabIndex = 12;
            this.DisableAttackButton.Text = "禁用网络攻击";
            this.DisableAttackButton.UseVisualStyleBackColor = true;
            this.DisableAttackButton.Click += new System.EventHandler(this.DisableAttackButton_Click);
            // 
            // RoomStatusGroup
            // 
            this.RoomStatusGroup.Controls.Add(this.RoomStatusLabel);
            this.RoomStatusGroup.Controls.Add(this.RoomStatusButton);
            this.RoomStatusGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RoomStatusGroup.Location = new System.Drawing.Point(12, 7);
            this.RoomStatusGroup.Name = "RoomStatusGroup";
            this.RoomStatusGroup.Size = new System.Drawing.Size(281, 86);
            this.RoomStatusGroup.TabIndex = 0;
            this.RoomStatusGroup.TabStop = false;
            this.RoomStatusGroup.Text = "教室状态";
            // 
            // RoomStatusLabel
            // 
            this.RoomStatusLabel.AutoSize = true;
            this.RoomStatusLabel.BackColor = System.Drawing.Color.White;
            this.RoomStatusLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RoomStatusLabel.Location = new System.Drawing.Point(100, 41);
            this.RoomStatusLabel.Name = "RoomStatusLabel";
            this.RoomStatusLabel.Size = new System.Drawing.Size(78, 23);
            this.RoomStatusLabel.TabIndex = 2;
            this.RoomStatusLabel.Text = "状态未知";
            // 
            // RoomStatusButton
            // 
            this.RoomStatusButton.BackColor = System.Drawing.Color.White;
            this.RoomStatusButton.Enabled = false;
            this.RoomStatusButton.Location = new System.Drawing.Point(7, 27);
            this.RoomStatusButton.Name = "RoomStatusButton";
            this.RoomStatusButton.Size = new System.Drawing.Size(268, 53);
            this.RoomStatusButton.TabIndex = 1;
            this.RoomStatusButton.UseVisualStyleBackColor = false;
            // 
            // RoomControlGroup
            // 
            this.RoomControlGroup.Controls.Add(this.RecoverRoomButton);
            this.RoomControlGroup.Controls.Add(this.CloseRoomButton);
            this.RoomControlGroup.Controls.Add(this.PauseRoomButton);
            this.RoomControlGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RoomControlGroup.Location = new System.Drawing.Point(12, 99);
            this.RoomControlGroup.Name = "RoomControlGroup";
            this.RoomControlGroup.Size = new System.Drawing.Size(281, 99);
            this.RoomControlGroup.TabIndex = 3;
            this.RoomControlGroup.TabStop = false;
            this.RoomControlGroup.Text = "进程控制";
            // 
            // RecoverRoomButton
            // 
            this.RecoverRoomButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecoverRoomButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RecoverRoomButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RecoverRoomButton.Location = new System.Drawing.Point(190, 26);
            this.RecoverRoomButton.Name = "RecoverRoomButton";
            this.RecoverRoomButton.Size = new System.Drawing.Size(85, 67);
            this.RecoverRoomButton.TabIndex = 6;
            this.RecoverRoomButton.Text = "恢复";
            this.RecoverRoomButton.UseVisualStyleBackColor = true;
            this.RecoverRoomButton.Click += new System.EventHandler(this.RecoverRoomButton_Click);
            // 
            // CloseRoomButton
            // 
            this.CloseRoomButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseRoomButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CloseRoomButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseRoomButton.Location = new System.Drawing.Point(8, 26);
            this.CloseRoomButton.Name = "CloseRoomButton";
            this.CloseRoomButton.Size = new System.Drawing.Size(85, 67);
            this.CloseRoomButton.TabIndex = 4;
            this.CloseRoomButton.Text = "关闭";
            this.CloseRoomButton.UseVisualStyleBackColor = true;
            this.CloseRoomButton.Click += new System.EventHandler(this.CloseRoomButton_Click);
            // 
            // PauseRoomButton
            // 
            this.PauseRoomButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PauseRoomButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PauseRoomButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PauseRoomButton.Location = new System.Drawing.Point(99, 26);
            this.PauseRoomButton.Name = "PauseRoomButton";
            this.PauseRoomButton.Size = new System.Drawing.Size(85, 67);
            this.PauseRoomButton.TabIndex = 5;
            this.PauseRoomButton.Text = "挂起";
            this.PauseRoomButton.UseVisualStyleBackColor = true;
            this.PauseRoomButton.Click += new System.EventHandler(this.PauseRoomButton_Click);
            // 
            // ProgramSettingsGroup
            // 
            this.ProgramSettingsGroup.Controls.Add(this.AuthorLabel);
            this.ProgramSettingsGroup.Controls.Add(this.ProgramVerLabel);
            this.ProgramSettingsGroup.Controls.Add(this.ChooseRoomButton);
            this.ProgramSettingsGroup.Controls.Add(this.UpdateProgramButton);
            this.ProgramSettingsGroup.Controls.Add(this.DisableAttackButton);
            this.ProgramSettingsGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgramSettingsGroup.Location = new System.Drawing.Point(12, 294);
            this.ProgramSettingsGroup.Name = "ProgramSettingsGroup";
            this.ProgramSettingsGroup.Size = new System.Drawing.Size(281, 204);
            this.ProgramSettingsGroup.TabIndex = 10;
            this.ProgramSettingsGroup.TabStop = false;
            this.ProgramSettingsGroup.Text = "程序设置";
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AuthorLabel.Location = new System.Drawing.Point(77, 172);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(127, 23);
            this.AuthorLabel.TabIndex = 15;
            this.AuthorLabel.Text = "作者：Ujhhgtg";
            // 
            // ProgramVerLabel
            // 
            this.ProgramVerLabel.AutoSize = true;
            this.ProgramVerLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgramVerLabel.Location = new System.Drawing.Point(77, 149);
            this.ProgramVerLabel.Name = "ProgramVerLabel";
            this.ProgramVerLabel.Size = new System.Drawing.Size(118, 23);
            this.ProgramVerLabel.TabIndex = 14;
            this.ProgramVerLabel.Text = "版本号：X.Y.Z";
            // 
            // ChooseRoomButton
            // 
            this.ChooseRoomButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseRoomButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChooseRoomButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChooseRoomButton.Location = new System.Drawing.Point(8, 108);
            this.ChooseRoomButton.Name = "ChooseRoomButton";
            this.ChooseRoomButton.Size = new System.Drawing.Size(267, 35);
            this.ChooseRoomButton.TabIndex = 13;
            this.ChooseRoomButton.Text = "选择教室程序";
            this.ChooseRoomButton.UseVisualStyleBackColor = true;
            this.ChooseRoomButton.Click += new System.EventHandler(this.ChooseRoomButton_Click);
            // 
            // PswdGroup
            // 
            this.PswdGroup.Controls.Add(this.GetPswdButton);
            this.PswdGroup.Controls.Add(this.SetPswdButton);
            this.PswdGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PswdGroup.Location = new System.Drawing.Point(12, 204);
            this.PswdGroup.Name = "PswdGroup";
            this.PswdGroup.Size = new System.Drawing.Size(281, 84);
            this.PswdGroup.TabIndex = 7;
            this.PswdGroup.TabStop = false;
            this.PswdGroup.Text = "密码设置";
            // 
            // GetPswdButton
            // 
            this.GetPswdButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetPswdButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GetPswdButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GetPswdButton.Location = new System.Drawing.Point(8, 26);
            this.GetPswdButton.Name = "GetPswdButton";
            this.GetPswdButton.Size = new System.Drawing.Size(131, 50);
            this.GetPswdButton.TabIndex = 8;
            this.GetPswdButton.Text = "读取";
            this.GetPswdButton.UseVisualStyleBackColor = true;
            this.GetPswdButton.Click += new System.EventHandler(this.GetPswdButton_Click);
            // 
            // SetPswdButton
            // 
            this.SetPswdButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SetPswdButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetPswdButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SetPswdButton.Location = new System.Drawing.Point(145, 26);
            this.SetPswdButton.Name = "SetPswdButton";
            this.SetPswdButton.Size = new System.Drawing.Size(130, 50);
            this.SetPswdButton.TabIndex = 9;
            this.SetPswdButton.Text = "设置";
            this.SetPswdButton.UseVisualStyleBackColor = true;
            this.SetPswdButton.Click += new System.EventHandler(this.SetPswdButton_Click);
            // 
            // ScripterButton
            // 
            this.ScripterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScripterButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ScripterButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScripterButton.Location = new System.Drawing.Point(165, 224);
            this.ScripterButton.Name = "ScripterButton";
            this.ScripterButton.Size = new System.Drawing.Size(158, 31);
            this.ScripterButton.TabIndex = 30;
            this.ScripterButton.Text = "脚本制作器";
            this.ScripterButton.UseVisualStyleBackColor = true;
            this.ScripterButton.Click += new System.EventHandler(this.ScripterButton_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.CloseRoomButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 510);
            this.Controls.Add(this.PswdGroup);
            this.Controls.Add(this.ProgramSettingsGroup);
            this.Controls.Add(this.RoomFakingGroup);
            this.Controls.Add(this.RoomStatusGroup);
            this.Controls.Add(this.RoomControlGroup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机房助手";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.RoomFakingGroup.ResumeLayout(false);
            this.RoomFakingGroup.PerformLayout();
            this.RoomStatusGroup.ResumeLayout(false);
            this.RoomStatusGroup.PerformLayout();
            this.RoomControlGroup.ResumeLayout(false);
            this.ProgramSettingsGroup.ResumeLayout(false);
            this.ProgramSettingsGroup.PerformLayout();
            this.PswdGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox RoomFakingGroup;
        private System.Windows.Forms.Button IPButton;
        private System.Windows.Forms.Button AttackButton;
        private System.Windows.Forms.RadioButton UseMsgRadio;
        private System.Windows.Forms.TextBox MsgTextBox;
        private System.Windows.Forms.RadioButton UseCmdRadio;
        private System.Windows.Forms.TextBox CmdTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.TextBox IPRangeTextBox;
        private System.Windows.Forms.Label IPLabel3;
        private System.Windows.Forms.Button UpdateProgramButton;
        private System.Windows.Forms.Button DisableAttackButton;
        private System.Windows.Forms.GroupBox RoomStatusGroup;
        private System.Windows.Forms.Label RoomStatusLabel;
        private System.Windows.Forms.GroupBox RoomControlGroup;
        private System.Windows.Forms.Button RecoverRoomButton;
        private System.Windows.Forms.Button CloseRoomButton;
        private System.Windows.Forms.Button PauseRoomButton;
        private System.Windows.Forms.RadioButton UseScriptRadio;
        private System.Windows.Forms.Button ChooseScriptButton;
        private System.Windows.Forms.Button InstallPythonButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.TextBox PCNameTextBox;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.GroupBox ProgramSettingsGroup;
        private System.Windows.Forms.Label ProgramVerLabel;
        private System.Windows.Forms.Button ChooseRoomButton;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button RoomStatusButton;
        private System.Windows.Forms.GroupBox PswdGroup;
        private System.Windows.Forms.Button GetPswdButton;
        private System.Windows.Forms.Button SetPswdButton;
        private System.Windows.Forms.Button ScripterButton;
    }
}