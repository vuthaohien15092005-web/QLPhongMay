namespace QLPhongMay.GUI.Forms.Catalog
{
    partial class frmQLCaHoc
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

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.DataGridViewButtonColumn ViewAction;
        private System.Windows.Forms.DataGridViewButtonColumn EditAction;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Actions;
        private System.Windows.Forms.Panel pnlPaging;
    }
}