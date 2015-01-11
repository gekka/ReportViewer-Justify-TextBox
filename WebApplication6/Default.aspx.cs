using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication6
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //// pdf形式で出力を行う
            //Byte[] bytes = ReportViewer1.LocalReport.Render("PDF");
            //Response.ContentType = "Application/pdf";
            //Response.BinaryWrite(bytes);
            //Response.End();

            //// png形式で出力を行う
            //Byte[] bytes = ReportViewer1.LocalReport.Render("image", "<DeviceInfo><OutputFormat>PNG</OutputFormat></DeviceInfo>");
            //Response.ContentType = "image/png";
            //Response.BinaryWrite(bytes);
            //Response.End();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //Microsoft.Reporting.WebForms.ReportViewer reportViewer1 =  //(Microsoft.Reporting.WebForms.ReportViewer)sender;
            Microsoft.Reporting.WebForms.ReportViewer reportViewer1 = this.ReportViewer1;
            string s = "　上記の業務委託について、ああああああと受注者(株)あいうえお建設は、各々の対等な立場における合意に基づいて、別添のうううううううううううによって委託契約を締結し、信義に従って誠実にこれを履行するものとする。";
            s = ToJustifyText(s);
            List<Microsoft.Reporting.WebForms.ReportParameter> list = new List<Microsoft.Reporting.WebForms.ReportParameter>();
            foreach (Microsoft.Reporting.WebForms.ReportParameterInfo rpi in reportViewer1.LocalReport.GetParameters())
            {
                Microsoft.Reporting.WebForms.ReportParameter rp = new Microsoft.Reporting.WebForms.ReportParameter(rpi.Name, s);
                list.Add(rp);
            }

            foreach (string name in this.ReportViewer1.LocalReport.GetDataSourceNames())
            {
                DataSet1.DataTable1DataTable dt = new DataSet1.DataTable1DataTable();
                var row = dt.NewDataTable1Row();
                row.DataColumn1 = s;
                dt.Rows.Add(row);

                row = dt.NewDataTable1Row();
                row.DataColumn1 = ToJustifyText(s);
                dt.Rows.Add(row);

                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(name, (System.Collections.IEnumerable)dt));

            }

            reportViewer1.DataBind();
            reportViewer1.LocalReport.SetParameters(list);
        }



        public string ToJustifyText(string tgt)
        {
            char ZERO_WIDTH_SPACE = Convert.ToChar(0x200B);
            System.Text.StringBuilder ret = new System.Text.StringBuilder();

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
