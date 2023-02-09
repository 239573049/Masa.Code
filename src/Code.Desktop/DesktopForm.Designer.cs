namespace Code.Desktop;

partial class DesktopForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        blazorWeb = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
        SuspendLayout();
        // 
        // blazorWeb
        // 
        blazorWeb.Dock = DockStyle.Fill;
        blazorWeb.Location = new Point(0, 0);
        blazorWeb.Name = "blazorWeb";
        blazorWeb.Size = new Size(1006, 705);
        blazorWeb.TabIndex = 0;
        blazorWeb.Text = "blazorWeb";
        // 
        // DesktopForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1006, 705);
        ControlBox = false;
        Controls.Add(blazorWeb);
        Name = "DesktopForm";
        ShowIcon = false;
        ResumeLayout(false);
    }

    #endregion

    private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWeb;
}
