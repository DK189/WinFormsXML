using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsXML.Design
{
    internal class ControlXMLAdapterDesigner : System.ComponentModel.Design.ComponentDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
        }

        public override void InitializeExistingComponent(IDictionary defaultValues)
        {
            base.InitializeExistingComponent(defaultValues);
        }

        public override void DoDefaultAction()
        {
            MessageBox.Show("ControlXMLAdapterDesigner.DoDefaultAction() called.");
            base.DoDefaultAction();
        }

        public override DesignerVerbCollection Verbs
            => new DesignerVerbCollection([
                new DesignerVerb("Edit XMLContent", new EventHandler(myExampleVerpHandle)),
                new DesignerVerb("Choose XMLFilePath", new EventHandler(myExampleVerpHandle)),
            ]);

        private void myExampleVerpHandle(object sender, EventArgs e)
        {
            MessageBox.Show("My example verb handler called.");
        }
    }
}
