using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string s="　上記の業務委託について、ああああああと受注者(株)あいうえお建設は、各々の対等な立場における合意に基づいて、別添のうううううううううううによって委託契約を締結し、信義に従って誠実にこれを履行するものとする。";
            //s=ToJustifyText(s);
            List<Microsoft.Reporting.WinForms.ReportParameter> list = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            foreach (Microsoft.Reporting.WinForms.ReportParameterInfo rpi in this.reportViewer1.LocalReport.GetParameters())
            {
                Microsoft.Reporting.WinForms.ReportParameter rp = new Microsoft.Reporting.WinForms.ReportParameter(rpi.Name, s);
                this.reportViewer1.LocalReport.SetParameters(rp);
            }

            foreach (string name in this.reportViewer1.LocalReport.GetDataSourceNames())
            {
                DataSet1.DataTable1DataTable dt = new DataSet1.DataTable1DataTable();
                var row=dt.NewDataTable1Row();
                row.DataColumn1 = s;
                dt.Rows.Add(row);

                row = dt.NewDataTable1Row();
                row.DataColumn1 =ToJustifyText( s);
                dt.Rows.Add(row);

                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(name, (System.Collections.IEnumerable)dt));

            }
            
            var x=this.reportViewer1.LocalReport.Render("PDF");
            this.reportViewer1.RefreshReport();
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
    }
}