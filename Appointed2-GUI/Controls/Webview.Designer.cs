namespace Appointed2_GUI.Controls
{
    partial class Webview
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.webBrowserEx1 = new Appointed2_GUI.Controls.WebBrowserEx();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(612, 27);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // webBrowserEx1
            // 
            this.webBrowserEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserEx1.Location = new System.Drawing.Point(0, 27);
            this.webBrowserEx1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserEx1.Name = "webBrowserEx1";
            this.webBrowserEx1.Size = new System.Drawing.Size(612, 545);
            this.webBrowserEx1.TabIndex = 1;
            this.webBrowserEx1.NewWindow3 += new System.EventHandler<Appointed2_GUI.Controls.WebBrowserNewWindowEventArgs>(this.webBrowser_NewWindow);
            this.webBrowserEx1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentLoaded);
            // 
            // Webview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowserEx1);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Webview";
            this.Size = new System.Drawing.Size(612, 572);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private Controls.WebBrowserEx webBrowserEx1;
    }
}
