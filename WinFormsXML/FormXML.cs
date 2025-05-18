using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsXML
{
    /// <summary>
    /// Represents a Windows Forms form that integrates XML-based functionality.
    /// </summary>
    /// <remarks>
    /// The <see cref="FormXML"/> class extends the standard <see cref="Form"/> class to provide
    /// additional XML-related capabilities. It includes an <see cref="Adapter"/> property that facilitates interaction
    /// with XML data through a <see cref="ControlXMLAdapter"/>.
    /// </remarks>
    [DesignerCategory("WinForms XML")]
    [DefaultEvent("Load")]
    [InitializationEvent("Load")]
    public class FormXML : Form
    {
        ControlXMLAdapter adapter;

        /// <summary>
        /// Gets the XML adapter associated with the control.
        /// </summary>
        [Category("WinForms XML")]
        public ControlXMLAdapter Adapter
        {
            get
            {
                if (adapter == null)
                {
                    adapter = new ControlXMLAdapter();
                    adapter.BeginInit();
                    adapter.Target = this;
                    adapter.EndInit();
                }
                return adapter;
            }
        }
    }
}
