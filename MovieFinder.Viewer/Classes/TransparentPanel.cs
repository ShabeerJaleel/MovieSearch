using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BlueTube.Viewer
{
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
        }

        /// <summary>
        /// Gets the creation parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createparams = base.CreateParams;
                createparams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createparams;
            }
        }

        /// <summary>
        /// Skips painting the background.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do nothing
            //.OnPaintBackground(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

           
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
