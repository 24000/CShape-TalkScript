
using System.Drawing;
using System.Windows.Forms;

namespace TalkScript.Viewer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.HeaderPnl = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.FontSizeCombo = new System.Windows.Forms.ComboBox();
            this.HeaderPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderPnl
            // 
            this.HeaderPnl.ColumnCount = 2;
            this.HeaderPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.HeaderPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 844F));
            this.HeaderPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.HeaderPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.HeaderPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.HeaderPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.HeaderPnl.Controls.Add(this.label3, 0, 0);
            this.HeaderPnl.Controls.Add(this.FontSizeCombo, 1, 0);
            this.HeaderPnl.Location = new System.Drawing.Point(6, 6);
            this.HeaderPnl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HeaderPnl.MaximumSize = new System.Drawing.Size(900, 133);
            this.HeaderPnl.Name = "HeaderPnl";
            this.HeaderPnl.RowCount = 1;
            this.HeaderPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.HeaderPnl.Size = new System.Drawing.Size(196, 43);
            this.HeaderPnl.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 43);
            this.label3.TabIndex = 1;
            this.label3.Text = "Font\r\nSize";
            // 
            // FontSizeCombo
            // 
            this.FontSizeCombo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FontSizeCombo.FormattingEnabled = true;
            this.FontSizeCombo.Location = new System.Drawing.Point(60, 5);
            this.FontSizeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FontSizeCombo.Name = "FontSizeCombo";
            this.FontSizeCombo.Size = new System.Drawing.Size(89, 31);
            this.FontSizeCombo.TabIndex = 2;
            this.FontSizeCombo.SelectedIndexChanged += new System.EventHandler(this.FontSizeCombo_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(578, 544);
            this.Controls.Add(this.HeaderPnl);
            this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Location = new System.Drawing.Point(6, 4);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.HeaderPnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel HeaderPnl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FontSizeCombo;
    }
}

