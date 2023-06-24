namespace ITClassHelper
{
    partial class FormCastControl
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
            this.FormControllerLabel = new System.Windows.Forms.Label();
            this.HideCastButton = new System.Windows.Forms.Button();
            this.MinimizeCastButton = new System.Windows.Forms.Button();
            this.ShowCastButton = new System.Windows.Forms.Button();
            this.HideControllerButton = new System.Windows.Forms.Button();
            this.SetCastTitleBarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FormControllerLabel
            // 
            this.FormControllerLabel.AutoSize = true;
            this.FormControllerLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormControllerLabel.Location = new System.Drawing.Point(12, 9);
            this.FormControllerLabel.Name = "FormControllerLabel";
            this.FormControllerLabel.Size = new System.Drawing.Size(95, 23);
            this.FormControllerLabel.TabIndex = 8;
            this.FormControllerLabel.Text = "广播控制器";
            // 
            // HideCastButton
            // 
            this.HideCastButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideCastButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HideCastButton.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideCastButton.Location = new System.Drawing.Point(13, 36);
            this.HideCastButton.Name = "HideCastButton";
            this.HideCastButton.Size = new System.Drawing.Size(100, 66);
            this.HideCastButton.TabIndex = 9;
            this.HideCastButton.Text = "隐藏\r\n广播";
            this.HideCastButton.UseVisualStyleBackColor = true;
            this.HideCastButton.Click += new System.EventHandler(this.HideCastButton_Click);
            // 
            // MinimizeCastButton
            // 
            this.MinimizeCastButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeCastButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MinimizeCastButton.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimizeCastButton.Location = new System.Drawing.Point(119, 36);
            this.MinimizeCastButton.Name = "MinimizeCastButton";
            this.MinimizeCastButton.Size = new System.Drawing.Size(100, 66);
            this.MinimizeCastButton.TabIndex = 10;
            this.MinimizeCastButton.Text = "缩小\r\n广播";
            this.MinimizeCastButton.UseVisualStyleBackColor = true;
            this.MinimizeCastButton.Click += new System.EventHandler(this.MinimizeCastButton_Click);
            // 
            // ShowCastButton
            // 
            this.ShowCastButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowCastButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ShowCastButton.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ShowCastButton.Location = new System.Drawing.Point(225, 36);
            this.ShowCastButton.Name = "ShowCastButton";
            this.ShowCastButton.Size = new System.Drawing.Size(100, 66);
            this.ShowCastButton.TabIndex = 11;
            this.ShowCastButton.Text = "恢复\r\n广播";
            this.ShowCastButton.UseVisualStyleBackColor = true;
            this.ShowCastButton.Click += new System.EventHandler(this.ShowCastButton_Click);
            // 
            // HideControllerButton
            // 
            this.HideControllerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideControllerButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HideControllerButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideControllerButton.Location = new System.Drawing.Point(399, 11);
            this.HideControllerButton.Name = "HideControllerButton";
            this.HideControllerButton.Size = new System.Drawing.Size(26, 19);
            this.HideControllerButton.TabIndex = 13;
            this.HideControllerButton.Text = "-";
            this.HideControllerButton.UseVisualStyleBackColor = true;
            this.HideControllerButton.Click += new System.EventHandler(this.HideControllerButton_Click);
            // 
            // SetCastTitleBarButton
            // 
            this.SetCastTitleBarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SetCastTitleBarButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetCastTitleBarButton.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SetCastTitleBarButton.Location = new System.Drawing.Point(331, 36);
            this.SetCastTitleBarButton.Name = "SetCastTitleBarButton";
            this.SetCastTitleBarButton.Size = new System.Drawing.Size(100, 66);
            this.SetCastTitleBarButton.TabIndex = 14;
            this.SetCastTitleBarButton.Text = "显示\r\n标题栏";
            this.SetCastTitleBarButton.UseVisualStyleBackColor = true;
            this.SetCastTitleBarButton.Click += new System.EventHandler(this.SetCastTitleBarButton_Click);
            // 
            // FormCastControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(437, 114);
            this.ControlBox = false;
            this.Controls.Add(this.SetCastTitleBarButton);
            this.Controls.Add(this.HideControllerButton);
            this.Controls.Add(this.ShowCastButton);
            this.Controls.Add(this.MinimizeCastButton);
            this.Controls.Add(this.HideCastButton);
            this.Controls.Add(this.FormControllerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCastControl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormCastControl";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FormControllerLabel;
        private System.Windows.Forms.Button HideCastButton;
        private System.Windows.Forms.Button MinimizeCastButton;
        private System.Windows.Forms.Button ShowCastButton;
        private System.Windows.Forms.Button HideControllerButton;
        private System.Windows.Forms.Button SetCastTitleBarButton;
    }
}