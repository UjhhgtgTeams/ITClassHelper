namespace ITClassHelper
{
    partial class CastController
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
            this.CastControllerLabel = new System.Windows.Forms.Label();
            this.HideCastButton = new System.Windows.Forms.Button();
            this.HideTimeCastButton = new System.Windows.Forms.Button();
            this.ShowCastButton = new System.Windows.Forms.Button();
            this.CloseAppButton = new System.Windows.Forms.Button();
            this.HideControllerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CastControllerLabel
            // 
            this.CastControllerLabel.AutoSize = true;
            this.CastControllerLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CastControllerLabel.Location = new System.Drawing.Point(12, 9);
            this.CastControllerLabel.Name = "CastControllerLabel";
            this.CastControllerLabel.Size = new System.Drawing.Size(95, 23);
            this.CastControllerLabel.TabIndex = 8;
            this.CastControllerLabel.Text = "广播控制器";
            // 
            // HideCastButton
            // 
            this.HideCastButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideCastButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HideCastButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideCastButton.Location = new System.Drawing.Point(13, 36);
            this.HideCastButton.Name = "HideCastButton";
            this.HideCastButton.Size = new System.Drawing.Size(100, 66);
            this.HideCastButton.TabIndex = 9;
            this.HideCastButton.Text = "最小化\r\n广播";
            this.HideCastButton.UseVisualStyleBackColor = true;
            this.HideCastButton.Click += new System.EventHandler(this.HideCastButton_Click);
            // 
            // HideTimeCastButton
            // 
            this.HideTimeCastButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideTimeCastButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HideTimeCastButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideTimeCastButton.Location = new System.Drawing.Point(119, 36);
            this.HideTimeCastButton.Name = "HideTimeCastButton";
            this.HideTimeCastButton.Size = new System.Drawing.Size(100, 66);
            this.HideTimeCastButton.TabIndex = 10;
            this.HideTimeCastButton.Text = "隐藏5秒\r\n后恢复";
            this.HideTimeCastButton.UseVisualStyleBackColor = true;
            this.HideTimeCastButton.Click += new System.EventHandler(this.HideTimeCastButton_Click);
            // 
            // ShowCastButton
            // 
            this.ShowCastButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowCastButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ShowCastButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ShowCastButton.Location = new System.Drawing.Point(225, 36);
            this.ShowCastButton.Name = "ShowCastButton";
            this.ShowCastButton.Size = new System.Drawing.Size(100, 66);
            this.ShowCastButton.TabIndex = 11;
            this.ShowCastButton.Text = "恢复\r\n广播";
            this.ShowCastButton.UseVisualStyleBackColor = true;
            this.ShowCastButton.Click += new System.EventHandler(this.ShowCastButton_Click);
            // 
            // CloseAppButton
            // 
            this.CloseAppButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseAppButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CloseAppButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseAppButton.Location = new System.Drawing.Point(331, 36);
            this.CloseAppButton.Name = "CloseAppButton";
            this.CloseAppButton.Size = new System.Drawing.Size(100, 66);
            this.CloseAppButton.TabIndex = 12;
            this.CloseAppButton.Text = "关闭\r\n极域";
            this.CloseAppButton.UseVisualStyleBackColor = true;
            this.CloseAppButton.Click += new System.EventHandler(this.CloseAppButton_Click);
            // 
            // HideControllerButton
            // 
            this.HideControllerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideControllerButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HideControllerButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideControllerButton.Location = new System.Drawing.Point(405, 9);
            this.HideControllerButton.Name = "HideControllerButton";
            this.HideControllerButton.Size = new System.Drawing.Size(26, 19);
            this.HideControllerButton.TabIndex = 13;
            this.HideControllerButton.Text = "-";
            this.HideControllerButton.UseVisualStyleBackColor = true;
            this.HideControllerButton.Click += new System.EventHandler(this.HideControllerButton_Click);
            // 
            // CastController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(443, 114);
            this.ControlBox = false;
            this.Controls.Add(this.HideControllerButton);
            this.Controls.Add(this.CloseAppButton);
            this.Controls.Add(this.ShowCastButton);
            this.Controls.Add(this.HideTimeCastButton);
            this.Controls.Add(this.HideCastButton);
            this.Controls.Add(this.CastControllerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CastController";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CastController";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CastController_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CastControllerLabel;
        private System.Windows.Forms.Button HideCastButton;
        private System.Windows.Forms.Button HideTimeCastButton;
        private System.Windows.Forms.Button ShowCastButton;
        private System.Windows.Forms.Button CloseAppButton;
        private System.Windows.Forms.Button HideControllerButton;
    }
}