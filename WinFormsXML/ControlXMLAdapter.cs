using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsXML
{
    /// <summary>
    /// 
    /// </summary>
    [DesignerCategory("WinForms XML")]
    [Designer(typeof(WinFormsXML.Design.ControlXMLAdapterDesigner))]
    public sealed partial class ControlXMLAdapter
    {
        /// <summary>
        /// The target control that this adapter is associated with.
        /// </summary>
        [Category("WinForms XML")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public partial Control Target { get; set; }

        /// <summary>
        /// The XML content that defines the UI for the control.
        /// </summary>
        [Category("WinForms XML")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public partial string XMLContent { get; set; }

        /// <summary>
        /// The file path to the XML file that defines the UI for the control.
        /// </summary>
        [Category("WinForms XML")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public partial string XMLFilePath { get; set; }
    }

    partial class ControlXMLAdapter : Component, ISupportInitialize, ISupportInitializeNotification
    {
        private static readonly object EVENT_INITIALIZED = new object();

        private bool initalizing = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlXMLAdapter"/> class.
        /// </summary>
        public bool IsInitialized => initalizing;

        /// <summary>
        /// Event that is raised when the component is initialized.
        /// </summary>
        public event EventHandler Initialized
        {
            add
            {
                base.Events.AddHandler(EVENT_INITIALIZED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_INITIALIZED, value);
            }
        }

        /// <summary>
        /// Begins the initialization of a component. This method is called when the component is being initialized.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void BeginInit()
        {
            initalizing = true;
        }

        /// <summary>
        /// Ends the initialization of a component.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void EndInit()
        {
            initalizing = false;
            InitializeComponent();
        }

        /// <summary>
        /// Disposes the component and releases its resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    partial class ControlXMLAdapter
    {
        private Control _target;
        private string _xmlContent;
        private string _xmlFilePath;

        public partial Control Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;
                _target = value;
                InitializeComponent();
            }
        }

        public partial string XMLContent
        {
            get => _xmlContent;
            set
            {
                if (_xmlContent == value)
                    return;
                _xmlContent = value;
                InitializeComponent();
            }
        }

        public partial string XMLFilePath
        {
            get => _xmlFilePath;
            set
            {
                if (_xmlFilePath == value)
                    return;
                _xmlFilePath = value;
                InitializeComponent();
            }
        }

        private XDocument xDoc;

        private void InitializeComponent()
        {
            if(initalizing)
                return;

            if (!string.IsNullOrEmpty(_xmlFilePath))
            {
                if (!System.IO.File.Exists(_xmlFilePath))
                    throw new System.IO.FileNotFoundException("XML file not found.", _xmlFilePath);
                _xmlContent = System.IO.File.ReadAllText(XMLFilePath);
            }

            if (string.IsNullOrEmpty(_xmlContent))
            {
                return;
            }

            var _xDoc = XDocument.Parse(_xmlContent);

            var assemblies = _xDoc.Nodes().OfType<XProcessingInstruction>()
                .Where(x => x.Target.ToLower() == "assembly")
                .Select(x => x.Data)
                .Distinct()
                .ToArray();

            foreach (var ass in assemblies)
            {
                try
                {
                    Helpers.ControlHelper.LoadAssembly(ass);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Assembly `{ass}` could not load: {ex}");
                }
            }

            xDoc = _xDoc;


        }
    }
}
