namespace WinFormsNetFW
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.controlXMLAdapter1 = new WinFormsXML.ControlXMLAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.controlXMLAdapter1)).BeginInit();
            this.SuspendLayout();
            // 
            // controlXMLAdapter1
            // 
            this.controlXMLAdapter1.Target = this;
            this.controlXMLAdapter1.XMLContent = resources.GetString("controlXMLAdapter1.XMLContent");
            this.controlXMLAdapter1.XMLFilePath = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.controlXMLAdapter1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormsXML.ControlXMLAdapter controlXMLAdapter1;
    }
}