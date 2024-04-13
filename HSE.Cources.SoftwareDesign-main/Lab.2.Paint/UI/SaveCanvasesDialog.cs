using System;
using System.Windows.Forms;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.UI
{
    public partial class SaveCanvasesDialog : Form
    {
        private readonly IWorkspace Parent;

        public SaveCanvasesDialog(string[] names, IWorkspace parent) {
            InitializeComponent();
            cblistProjects.Items.AddRange(names);
            Parent = parent;
        }

        private void CancelAction(object sender, EventArgs e) {
            Close();
        }

        private void AcceptAction(object sender, EventArgs e) {
            var i = 0;
            Parent.Indexes = new int[cblistProjects.CheckedIndices.Count];
            foreach (var index in cblistProjects.CheckedIndices) Parent.Indexes[i++] = (int) index;
        }
    }
}