namespace Appointed2_GUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webview1 = new Appointed2_GUI.Controls.Webview();
            this.SuspendLayout();
            // 
            // webview1
            // 
            this.webview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webview1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.webview1.Location = new System.Drawing.Point(0, 0);
            this.webview1.Margin = new System.Windows.Forms.Padding(0);
            this.webview1.Name = "webview1";
            this.webview1.ShowCommandBar = true;
            this.webview1.Size = new System.Drawing.Size(684, 23);
            this.webview1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(684, 23);
            this.Controls.Add(this.webview1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Appointed2 - GUI程序";
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.Webview webview1;
    }
}

