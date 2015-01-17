using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
namespace ReportsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IEnumerable<ReportViewer> GetViewers()
        {
            return new[] { reportViewer1, reportViewer2, reportViewer3, reportViewer4, reportViewer5 };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (ReportViewer viewer in GetViewers())
            {
                string s = "　上記の業務委託について、ああああああと受注者(株)あいうえお建設は、各々の対等な立場における合意に基づいて、別添のうううううううううううによって委託契約を締結し、信義に従って誠実にこれを履行するものとする。";
                s = ToJustifyText(s);
                List<ReportParameter> list = new List<ReportParameter>();
                foreach (ReportParameterInfo rpi in viewer.LocalReport.GetParameters())
                {
                    ReportParameter rp = new ReportParameter(rpi.Name, s);
                    this.reportViewer1.LocalReport.SetParameters(rp);
                }

                foreach (string name in viewer.LocalReport.GetDataSourceNames())
                {
                    DataSet1.DataTable1DataTable dt = new DataSet1.DataTable1DataTable();
                    for (int code = 0x2000; code <= 0x206F; code++)
                    {
                        var row = dt.NewDataTable1Row();
                        row.code = code;
                        dt.Rows.Add(row);
                    }
                    viewer.LocalReport.DataSources.Add(new ReportDataSource(name, (System.Collections.IEnumerable)dt));

                }
                viewer.RefreshReport();
            }
        }

        public string ToJustifyText(string tgt)
        {
            char ZERO_WIDTH_SPACE = Convert.ToChar(0x200B);
            StringBuilder ret = new StringBuilder();

            foreach (char buf in tgt)
            {
                if (ret.Length > 0)
                {
                    ret.Append(ZERO_WIDTH_SPACE);
                }
                ret.Append(buf);
            }

            return ret.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        { 
            try
            {
                var renderext = this.reportViewer1.LocalReport.ListRenderingExtensions().First(x => x.LocalizedName == "PDF");
                foreach (ReportViewer viewer in GetViewers())
                {
                    while (!viewer.CurrentStatus.CanExport)
                    {
                        Application.DoEvents();
                    }
                    if (viewer.ExportDialog(renderext) == DialogResult.Cancel)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}