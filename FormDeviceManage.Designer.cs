namespace ITClassHelper
{
    partial class FormDeviceManage
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
            this.components = new System.ComponentModel.Container();
            this.DeviceList = new System.Windows.Forms.ListView();
            this.IPColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MacColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HostNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DeviceContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendCmdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendMsgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendScriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator1MenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.ShutdownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RebootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MagicCommandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BluescreenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RevShellMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PortLabel = new System.Windows.Forms.Label();
            this.IPButton = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.IPRangeTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel3 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.ScanButton = new System.Windows.Forms.Button();
            this.DisableHostNameCheckBox = new System.Windows.Forms.CheckBox();
            this.DisableMacAddressCheckBox = new System.Windows.Forms.CheckBox();
            this.ConvertHostNameIPLabel = new System.Windows.Forms.Label();
            this.HostNameTextBox = new System.Windows.Forms.TextBox();
            this.ConvertHostNameIPButton = new System.Windows.Forms.Button();
            this.DeviceContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeviceList
            // 
            this.DeviceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPColumn,
            this.MacColumn,
            this.HostNameColumn});
            this.DeviceList.ContextMenuStrip = this.DeviceContextMenu;
            this.DeviceList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeviceList.HideSelection = false;
            this.DeviceList.Location = new System.Drawing.Point(16, 49);
            this.DeviceList.Name = "DeviceList";
            this.DeviceList.Size = new System.Drawing.Size(606, 386);
            this.DeviceList.TabIndex = 41;
            this.DeviceList.UseCompatibleStateImageBehavior = false;
            this.DeviceList.View = System.Windows.Forms.View.Details;
            // 
            // IPColumn
            // 
            this.IPColumn.Text = "IP 地址";
            this.IPColumn.Width = 147;
            // 
            // MacColumn
            // 
            this.MacColumn.Text = "MAC 地址";
            this.MacColumn.Width = 154;
            // 
            // HostNameColumn
            // 
            this.HostNameColumn.Text = "计算机名";
            this.HostNameColumn.Width = 150;
            // 
            // DeviceContextMenu
            // 
            this.DeviceContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.DeviceContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendCmdMenuItem,
            this.SendMsgMenuItem,
            this.SendScriptMenuItem,
            this.Seperator1MenuItem,
            this.ShutdownMenuItem,
            this.RebootMenuItem,
            this.MagicCommandMenuItem});
            this.DeviceContextMenu.Name = "DeviceContextMenu";
            this.DeviceContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.DeviceContextMenu.ShowImageMargin = false;
            this.DeviceContextMenu.Size = new System.Drawing.Size(186, 182);
            // 
            // SendCmdMenuItem
            // 
            this.SendCmdMenuItem.Name = "SendCmdMenuItem";
            this.SendCmdMenuItem.Size = new System.Drawing.Size(185, 24);
            this.SendCmdMenuItem.Text = "发送命令";
            this.SendCmdMenuItem.Click += new System.EventHandler(this.SendCmdMenuItem_Click);
            // 
            // SendMsgMenuItem
            // 
            this.SendMsgMenuItem.Name = "SendMsgMenuItem";
            this.SendMsgMenuItem.Size = new System.Drawing.Size(185, 24);
            this.SendMsgMenuItem.Text = "发送消息";
            this.SendMsgMenuItem.Click += new System.EventHandler(this.SendMsgMenuItem_Click);
            // 
            // SendScriptMenuItem
            // 
            this.SendScriptMenuItem.Name = "SendScriptMenuItem";
            this.SendScriptMenuItem.Size = new System.Drawing.Size(185, 24);
            this.SendScriptMenuItem.Text = "发送脚本";
            this.SendScriptMenuItem.Click += new System.EventHandler(this.SendScriptMenuItem_Click);
            // 
            // Seperator1MenuItem
            // 
            this.Seperator1MenuItem.Name = "Seperator1MenuItem";
            this.Seperator1MenuItem.Size = new System.Drawing.Size(182, 6);
            // 
            // ShutdownMenuItem
            // 
            this.ShutdownMenuItem.Name = "ShutdownMenuItem";
            this.ShutdownMenuItem.Size = new System.Drawing.Size(185, 24);
            this.ShutdownMenuItem.Text = "远程关机";
            this.ShutdownMenuItem.Click += new System.EventHandler(this.ShutdownMenuItem_Click);
            // 
            // RebootMenuItem
            // 
            this.RebootMenuItem.Name = "RebootMenuItem";
            this.RebootMenuItem.Size = new System.Drawing.Size(185, 24);
            this.RebootMenuItem.Text = "远程重启";
            this.RebootMenuItem.Click += new System.EventHandler(this.RebootMenuItem_Click);
            // 
            // MagicCommandMenuItem
            // 
            this.MagicCommandMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BluescreenMenuItem,
            this.RevShellMenuItem});
            this.MagicCommandMenuItem.Name = "MagicCommandMenuItem";
            this.MagicCommandMenuItem.Size = new System.Drawing.Size(185, 24);
            this.MagicCommandMenuItem.Text = "魔法指令";
            // 
            // BluescreenMenuItem
            // 
            this.BluescreenMenuItem.Name = "BluescreenMenuItem";
            this.BluescreenMenuItem.Size = new System.Drawing.Size(317, 26);
            this.BluescreenMenuItem.Text = "快速蓝屏";
            this.BluescreenMenuItem.Click += new System.EventHandler(this.BluescreenMenuItem_Click);
            // 
            // RevShellMenuItem
            // 
            this.RevShellMenuItem.Name = "RevShellMenuItem";
            this.RevShellMenuItem.Size = new System.Drawing.Size(317, 26);
            this.RevShellMenuItem.Text = "终端反射（非常危险，不要手欠）";
            this.RevShellMenuItem.Click += new System.EventHandler(this.RevShellMenuItem_Click);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PortLabel.Location = new System.Drawing.Point(386, 20);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(14, 23);
            this.PortLabel.TabIndex = 46;
            this.PortLabel.Text = ":";
            // 
            // IPButton
            // 
            this.IPButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IPButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.IPButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPButton.Location = new System.Drawing.Point(16, 12);
            this.IPButton.Name = "IPButton";
            this.IPButton.Size = new System.Drawing.Size(130, 31);
            this.IPButton.TabIndex = 44;
            this.IPButton.Text = "IP 地址";
            this.IPButton.UseVisualStyleBackColor = true;
            this.IPButton.Click += new System.EventHandler(this.IPButton_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PortTextBox.Location = new System.Drawing.Point(406, 12);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(97, 29);
            this.PortTextBox.TabIndex = 47;
            this.PortTextBox.Text = "4605";
            this.PortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IPRangeTextBox
            // 
            this.IPRangeTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPRangeTextBox.Location = new System.Drawing.Point(327, 12);
            this.IPRangeTextBox.Name = "IPRangeTextBox";
            this.IPRangeTextBox.Size = new System.Drawing.Size(53, 29);
            this.IPRangeTextBox.TabIndex = 45;
            this.IPRangeTextBox.Text = "1";
            this.IPRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IPLabel3
            // 
            this.IPLabel3.AutoSize = true;
            this.IPLabel3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPLabel3.Location = new System.Drawing.Point(304, 20);
            this.IPLabel3.Name = "IPLabel3";
            this.IPLabel3.Size = new System.Drawing.Size(17, 23);
            this.IPLabel3.TabIndex = 43;
            this.IPLabel3.Text = "-";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPTextBox.Location = new System.Drawing.Point(152, 13);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(146, 29);
            this.IPTextBox.TabIndex = 42;
            this.IPTextBox.Text = "192.168.0.1";
            this.IPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScanButton
            // 
            this.ScanButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScanButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ScanButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScanButton.Location = new System.Drawing.Point(509, 12);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(113, 31);
            this.ScanButton.TabIndex = 48;
            this.ScanButton.Text = "扫描";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // DisableHostNameCheckBox
            // 
            this.DisableHostNameCheckBox.AutoSize = true;
            this.DisableHostNameCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DisableHostNameCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisableHostNameCheckBox.Location = new System.Drawing.Point(16, 441);
            this.DisableHostNameCheckBox.Name = "DisableHostNameCheckBox";
            this.DisableHostNameCheckBox.Size = new System.Drawing.Size(347, 28);
            this.DisableHostNameCheckBox.TabIndex = 49;
            this.DisableHostNameCheckBox.Text = "禁用计算机名扫描（大幅加快扫描速度）";
            this.DisableHostNameCheckBox.UseVisualStyleBackColor = true;
            // 
            // DisableMacAddressCheckBox
            // 
            this.DisableMacAddressCheckBox.AutoSize = true;
            this.DisableMacAddressCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DisableMacAddressCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisableMacAddressCheckBox.Location = new System.Drawing.Point(16, 475);
            this.DisableMacAddressCheckBox.Name = "DisableMacAddressCheckBox";
            this.DisableMacAddressCheckBox.Size = new System.Drawing.Size(329, 28);
            this.DisableMacAddressCheckBox.TabIndex = 50;
            this.DisableMacAddressCheckBox.Text = "禁用 MAC 地址扫描（加快扫描速度）";
            this.DisableMacAddressCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConvertHostNameIPLabel
            // 
            this.ConvertHostNameIPLabel.AutoSize = true;
            this.ConvertHostNameIPLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertHostNameIPLabel.Location = new System.Drawing.Point(16, 512);
            this.ConvertHostNameIPLabel.Name = "ConvertHostNameIPLabel";
            this.ConvertHostNameIPLabel.Size = new System.Drawing.Size(78, 23);
            this.ConvertHostNameIPLabel.TabIndex = 53;
            this.ConvertHostNameIPLabel.Text = "计算机名";
            // 
            // HostNameTextBox
            // 
            this.HostNameTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HostNameTextBox.Location = new System.Drawing.Point(100, 509);
            this.HostNameTextBox.Name = "HostNameTextBox";
            this.HostNameTextBox.Size = new System.Drawing.Size(387, 29);
            this.HostNameTextBox.TabIndex = 52;
            this.HostNameTextBox.Text = "在此填入计算机名";
            // 
            // ConvertHostNameIPButton
            // 
            this.ConvertHostNameIPButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConvertHostNameIPButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ConvertHostNameIPButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConvertHostNameIPButton.Location = new System.Drawing.Point(493, 508);
            this.ConvertHostNameIPButton.Name = "ConvertHostNameIPButton";
            this.ConvertHostNameIPButton.Size = new System.Drawing.Size(129, 31);
            this.ConvertHostNameIPButton.TabIndex = 51;
            this.ConvertHostNameIPButton.Text = "-> IP 地址";
            this.ConvertHostNameIPButton.UseVisualStyleBackColor = true;
            this.ConvertHostNameIPButton.Click += new System.EventHandler(this.ConvertHostNameIPButton_Click);
            // 
            // FormDeviceManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 550);
            this.Controls.Add(this.ConvertHostNameIPLabel);
            this.Controls.Add(this.HostNameTextBox);
            this.Controls.Add(this.ConvertHostNameIPButton);
            this.Controls.Add(this.DisableMacAddressCheckBox);
            this.Controls.Add(this.DisableHostNameCheckBox);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IPButton);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.IPRangeTextBox);
            this.Controls.Add(this.IPLabel3);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.DeviceList);
            this.Name = "FormDeviceManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备管理器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDeviceManage_FormClosing);
            this.DeviceContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView DeviceList;
        private System.Windows.Forms.ColumnHeader IPColumn;
        private System.Windows.Forms.ColumnHeader MacColumn;
        private System.Windows.Forms.ContextMenuStrip DeviceContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SendCmdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SendMsgMenuItem;
        private System.Windows.Forms.ToolStripSeparator Seperator1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShutdownMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RebootMenuItem;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button IPButton;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.TextBox IPRangeTextBox;
        private System.Windows.Forms.Label IPLabel3;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.ColumnHeader HostNameColumn;
        private System.Windows.Forms.CheckBox DisableHostNameCheckBox;
        private System.Windows.Forms.CheckBox DisableMacAddressCheckBox;
        private System.Windows.Forms.ToolStripMenuItem SendScriptMenuItem;
        private System.Windows.Forms.Label ConvertHostNameIPLabel;
        private System.Windows.Forms.TextBox HostNameTextBox;
        private System.Windows.Forms.Button ConvertHostNameIPButton;
        private System.Windows.Forms.ToolStripMenuItem MagicCommandMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BluescreenMenuItem;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.ToolStripMenuItem RevShellMenuItem;
    }
}