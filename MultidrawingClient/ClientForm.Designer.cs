namespace MultidrawingClient
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            Canva = new Panel();
            toolStrip1 = new ToolStrip();
            btPen = new ToolStripButton();
            btErase = new ToolStripButton();
            Canva.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Canva
            // 
            Canva.Controls.Add(toolStrip1);
            Canva.Dock = DockStyle.Fill;
            Canva.Location = new Point(0, 0);
            Canva.Name = "Canva";
            Canva.Size = new Size(733, 471);
            Canva.TabIndex = 0;
            Canva.Paint += Canva_Paint;
            Canva.MouseDown += Canva_MouseDown;
            Canva.MouseMove += Canva_MouseMove;
            Canva.MouseUp += Canva_MouseUp;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btPen, btErase });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(733, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btPen
            // 
            btPen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btPen.Image = (Image)resources.GetObject("btPen.Image");
            btPen.ImageTransparentColor = Color.Magenta;
            btPen.Name = "btPen";
            btPen.Size = new Size(29, 24);
            btPen.Text = "btPen";
            btPen.Click += btPen_Click;
            // 
            // btErase
            // 
            btErase.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btErase.Image = (Image)resources.GetObject("btErase.Image");
            btErase.ImageTransparentColor = Color.Magenta;
            btErase.Name = "btErase";
            btErase.Size = new Size(29, 24);
            btErase.Text = "brErase";
            btErase.Click += btErase_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(733, 471);
            Controls.Add(Canva);
            Name = "ClientForm";
            Text = "Multidrawing";
            Canva.ResumeLayout(false);
            Canva.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel Canva;
        private ToolStrip toolStrip1;
        private ToolStripButton btPen;
        private ToolStripButton btErase;
    }
}
