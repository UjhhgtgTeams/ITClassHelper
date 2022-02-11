namespace ITClassHelper
{
    partial class FormMain
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
            this.DeviceManageButton = new System.Windows.Forms.Button();
            this.UpdateProgramButton = new System.Windows.Forms.Button();
            this.DisableAttackButton = new System.Windows.Forms.Button();
            this.RoomStatusGroup = new System.Windows.Forms.GroupBox();
            this.RoomStatusLabel = new System.Windows.Forms.Label();
            this.RoomStatusButton = new System.Windows.Forms.Button();
            this.ProcessControlGroup = new System.Windows.Forms.GroupBox();
            this.RecoverRoomButton = new System.Windows.Forms.Button();
            this.CloseRoomButton = new System.Windows.Forms.Button();
            this.PauseRoomButton = new System.Windows.Forms.Button();
            this.ProgramToolsGroup = new System.Windows.Forms.GroupBox();
            this.FakeScreenButton = new System.Windows.Forms.Button();
            this.PreventKeyboardHookButton = new System.Windows.Forms.Button();
            this.ChatButton = new System.Windows.Forms.Button();
            this.ExitProgramButton = new System.Windows.Forms.Button();
            this.ProgramAboutLabel = new System.Windows.Forms.Label();
            this.GetRoomPathButton = new System.Windows.Forms.Button();
            this.PswdSettingsGroup = new System.Windows.Forms.GroupBox();
            this.GetPswdButton = new System.Windows.Forms.Button();
            this.SetPswdButton = new System.Windows.Forms.Button();
            this.RoomStatusGroup.SuspendLayout();
            this.ProcessControlGroup.SuspendLayout();
            this.ProgramToolsGroup.SuspendLayout();
            this.PswdSettingsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeviceManageButton
            // 
            this.DeviceManageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeviceManageButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DeviceManageButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeviceManageButton.Location = new System.Drawing.Point(236, 67);
            this.DeviceManageButton.Name = "DeviceManageButton";
            this.DeviceManageButton.Size = new System.Drawing.Size(225, 35);
            this.DeviceManageButton.TabIndex = 35;
            this.DeviceManageButton.Text = "打开设备管理";
            this.DeviceManageButton.UseVisualStyleBackColor = true;
            this.DeviceManageButton.Click += new System.EventHandler(this.DeviceManageButton_Click);
            // 
            // UpdateProgramButton
            // 
            this.UpdateProgramButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateProgramButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UpdateProgramButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpdateProgramButton.Location = new System.Drawing.Point(8, 26);
            this.UpdateProgramButton.Name = "UpdateProgramButton";
            this.UpdateProgramButton.Size = new System.Drawing.Size(225, 35);
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
            this.DisableAttackButton.Location = new System.Drawing.Point(236, 27);
            this.DisableAttackButton.Name = "DisableAttackButton";
            this.DisableAttackButton.Size = new System.Drawing.Size(225, 35);
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
            // ProcessControlGroup
            // 
            this.ProcessControlGroup.Controls.Add(this.RecoverRoomButton);
            this.ProcessControlGroup.Controls.Add(this.CloseRoomButton);
            this.ProcessControlGroup.Controls.Add(this.PauseRoomButton);
            this.ProcessControlGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProcessControlGroup.Location = new System.Drawing.Point(12, 99);
            this.ProcessControlGroup.Name = "ProcessControlGroup";
            this.ProcessControlGroup.Size = new System.Drawing.Size(281, 99);
            this.ProcessControlGroup.TabIndex = 3;
            this.ProcessControlGroup.TabStop = false;
            this.ProcessControlGroup.Text = "进程控制";
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
            // ProgramToolsGroup
            // 
            this.ProgramToolsGroup.Controls.Add(this.FakeScreenButton);
            this.ProgramToolsGroup.Controls.Add(this.PreventKeyboardHookButton);
            this.ProgramToolsGroup.Controls.Add(this.ChatButton);
            this.ProgramToolsGroup.Controls.Add(this.DeviceManageButton);
            this.ProgramToolsGroup.Controls.Add(this.ExitProgramButton);
            this.ProgramToolsGroup.Controls.Add(this.ProgramAboutLabel);
            this.ProgramToolsGroup.Controls.Add(this.GetRoomPathButton);
            this.ProgramToolsGroup.Controls.Add(this.UpdateProgramButton);
            this.ProgramToolsGroup.Controls.Add(this.DisableAttackButton);
            this.ProgramToolsGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgramToolsGroup.Location = new System.Drawing.Point(299, 7);
            this.ProgramToolsGroup.Name = "ProgramToolsGroup";
            this.ProgramToolsGroup.Size = new System.Drawing.Size(472, 281);
            this.ProgramToolsGroup.TabIndex = 10;
            this.ProgramToolsGroup.TabStop = false;
            this.ProgramToolsGroup.Text = "实用工具";
            // 
            // FakeScreenButton
            // 
            this.FakeScreenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FakeScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FakeScreenButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FakeScreenButton.Location = new System.Drawing.Point(8, 149);
            this.FakeScreenButton.Name = "FakeScreenButton";
            this.FakeScreenButton.Size = new System.Drawing.Size(225, 35);
            this.FakeScreenButton.TabIndex = 39;
            this.FakeScreenButton.Text = "截图伪装屏幕";
            this.FakeScreenButton.UseVisualStyleBackColor = true;
            this.FakeScreenButton.Click += new System.EventHandler(this.FakeScreenButton_Click);
            // 
            // PreventKeyboardHookButton
            // 
            this.PreventKeyboardHookButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PreventKeyboardHookButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PreventKeyboardHookButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PreventKeyboardHookButton.Location = new System.Drawing.Point(236, 108);
            this.PreventKeyboardHookButton.Name = "PreventKeyboardHookButton";
            this.PreventKeyboardHookButton.Size = new System.Drawing.Size(225, 35);
            this.PreventKeyboardHookButton.TabIndex = 38;
            this.PreventKeyboardHookButton.Text = "防止键盘挂钩";
            this.PreventKeyboardHookButton.UseVisualStyleBackColor = true;
            this.PreventKeyboardHookButton.Click += new System.EventHandler(this.PreventKeyboardHookButton_Click);
            // 
            // ChatButton
            // 
            this.ChatButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChatButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChatButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChatButton.Location = new System.Drawing.Point(8, 108);
            this.ChatButton.Name = "ChatButton";
            this.ChatButton.Size = new System.Drawing.Size(225, 35);
            this.ChatButton.TabIndex = 37;
            this.ChatButton.Text = "简易内网聊天";
            this.ChatButton.UseVisualStyleBackColor = true;
            this.ChatButton.Click += new System.EventHandler(this.ChatButton_Click);
            // 
            // ExitProgramButton
            // 
            this.ExitProgramButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitProgramButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExitProgramButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExitProgramButton.Location = new System.Drawing.Point(249, 223);
            this.ExitProgramButton.Name = "ExitProgramButton";
            this.ExitProgramButton.Size = new System.Drawing.Size(97, 46);
            this.ExitProgramButton.TabIndex = 33;
            this.ExitProgramButton.Text = "退出软件";
            this.ExitProgramButton.UseVisualStyleBackColor = true;
            this.ExitProgramButton.Click += new System.EventHandler(this.ExitProgramButton_Click);
            // 
            // ProgramAboutLabel
            // 
            this.ProgramAboutLabel.AutoSize = true;
            this.ProgramAboutLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgramAboutLabel.Location = new System.Drawing.Point(100, 223);
            this.ProgramAboutLabel.Name = "ProgramAboutLabel";
            this.ProgramAboutLabel.Size = new System.Drawing.Size(127, 46);
            this.ProgramAboutLabel.TabIndex = 14;
            this.ProgramAboutLabel.Text = "版本号：X.Y.Z\r\n作者：Ujhhgtg";
            // 
            // GetRoomPathButton
            // 
            this.GetRoomPathButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetRoomPathButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GetRoomPathButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GetRoomPathButton.Location = new System.Drawing.Point(8, 67);
            this.GetRoomPathButton.Name = "GetRoomPathButton";
            this.GetRoomPathButton.Size = new System.Drawing.Size(225, 35);
            this.GetRoomPathButton.TabIndex = 13;
            this.GetRoomPathButton.Text = "选择教室程序";
            this.GetRoomPathButton.UseVisualStyleBackColor = true;
            this.GetRoomPathButton.Click += new System.EventHandler(this.GetRoomPathButton_Click);
            // 
            // PswdSettingsGroup
            // 
            this.PswdSettingsGroup.Controls.Add(this.GetPswdButton);
            this.PswdSettingsGroup.Controls.Add(this.SetPswdButton);
            this.PswdSettingsGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PswdSettingsGroup.Location = new System.Drawing.Point(12, 204);
            this.PswdSettingsGroup.Name = "PswdSettingsGroup";
            this.PswdSettingsGroup.Size = new System.Drawing.Size(281, 84);
            this.PswdSettingsGroup.TabIndex = 7;
            this.PswdSettingsGroup.TabStop = false;
            this.PswdSettingsGroup.Text = "密码设置";
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
            // FormMain
            // 
            this.AcceptButton = this.CloseRoomButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 300);
            this.Controls.Add(this.PswdSettingsGroup);
            this.Controls.Add(this.ProgramToolsGroup);
            this.Controls.Add(this.RoomStatusGroup);
            this.Controls.Add(this.ProcessControlGroup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机房助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.RoomStatusGroup.ResumeLayout(false);
            this.RoomStatusGroup.PerformLayout();
            this.ProcessControlGroup.ResumeLayout(false);
            this.ProgramToolsGroup.ResumeLayout(false);
            this.ProgramToolsGroup.PerformLayout();
            this.PswdSettingsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button UpdateProgramButton;
        private System.Windows.Forms.Button DisableAttackButton;
        private System.Windows.Forms.GroupBox RoomStatusGroup;
        private System.Windows.Forms.Label RoomStatusLabel;
        private System.Windows.Forms.GroupBox ProcessControlGroup;
        private System.Windows.Forms.Button RecoverRoomButton;
        private System.Windows.Forms.Button CloseRoomButton;
        private System.Windows.Forms.Button PauseRoomButton;
        private System.Windows.Forms.GroupBox ProgramToolsGroup;
        private System.Windows.Forms.Label ProgramAboutLabel;
        private System.Windows.Forms.Button GetRoomPathButton;
        private System.Windows.Forms.Button RoomStatusButton;
        private System.Windows.Forms.GroupBox PswdSettingsGroup;
        private System.Windows.Forms.Button GetPswdButton;
        private System.Windows.Forms.Button SetPswdButton;
        private System.Windows.Forms.Button ExitProgramButton;
        private System.Windows.Forms.Button DeviceManageButton;
        private System.Windows.Forms.Button ChatButton;
        private System.Windows.Forms.Button PreventKeyboardHookButton;
        private System.Windows.Forms.Button FakeScreenButton;
    }
}