using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityGenerator
{
    public partial class Default : System.Web.UI.Page
    {
        JsonClassGenerator gen;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                return;
        }

        protected void btnLoadMapping_Click(object sender, EventArgs e)
        {
            //MobileSystem.System = cboSystem.Text;

            gen = new JsonClassGenerator();
            gen.JsonString = StringHelper.GetCleanText(txtJsonString.Text.Trim());
            gen.MainClass = @"MainClass";
            gen.MappingList = new List<MappingInfo>();

            try
            {
                gen.PrepareCSharpGridView();
                //if (ddlSystem.SelectedValue == "WP7")
                //    gen.PrepareCSharpGridView();
                //else if (ddlSystem.SelectedValue == "Android")
                //    gen.PrepareJavaGridView();
                //else if (ddlSystem.SelectedValue == "iOS")
                //    gen.PrepareOCGridView();

                BindGridView();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, "Unable to generate the code: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //MobileSystem.System = cboSystem.Text;

            gen = new JsonClassGenerator();
            gen.Namespace = txtVariable.Text.Trim();
            gen.MobileAPI = txtMobileAPI.Text.Trim();

            gen.MappingList = new List<MappingInfo>();
            for (int i = 0; i < gvMappingList.Rows.Count; i++)
            {
                if ((gvMappingList.Rows[i].Cells[1].FindControl("EntityName") as TextBox).Text == ""
                    || (gvMappingList.Rows[i].Cells[1].FindControl("JsonFromField") as TextBox).Text == ""
                    || (gvMappingList.Rows[i].Cells[1].FindControl("JsonToField") as TextBox).Text == ""
                    || (gvMappingList.Rows[i].Cells[1].FindControl("JsonToType") as TextBox).Text == "")
                    continue;

                gen.MappingList.Add(
                    new MappingInfo()
                    {
                        Level = Convert.ToInt16(gvMappingList.Rows[i].Cells[0].Text),
                        EntityName = (gvMappingList.Rows[i].Cells[1].FindControl("EntityName") as TextBox).Text,
                        JsonFromField = (gvMappingList.Rows[i].Cells[1].FindControl("JsonFromField") as TextBox).Text,
                        JsonToField = (gvMappingList.Rows[i].Cells[1].FindControl("JsonToField") as TextBox).Text,
                        JsonToType = HttpUtility.HtmlDecode((gvMappingList.Rows[i].Cells[1].FindControl("JsonToType") as TextBox).Text),
                    });
            }

            var list = gen.MappingList.GroupBy(x => x.EntityName);
            Response.Clear();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";

            var fileName = gen.MappingList.Where(x=>x.Level==1).ToList()[0].EntityName;
            Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".txt;");

            foreach (var g in list)
            {
                
                WriteLine("// JSON C# Class Generator");
                WriteLine("// Written by Bruce Bao");
                WriteLine("// Used for API: {0}", gen.MobileAPI);
                WriteLine("using System;");
                WriteLine("using System.Collections.Generic;");
                WriteLine("using System.Reflection;");
                WriteLine("using System.Runtime.Serialization;");
                WriteLine();

                WriteLine("namespace {0}", gen.Namespace);
                WriteLine("{");

                WriteLine("    [DataContract]");
                WriteLine("    public partial class {0}", g.Key);
                WriteLine("    {");

                var prefix = "        ";

                foreach (var field in g)
                {
                    WriteLine();
                    WriteLine(prefix + "[DataMember(Name=\"{0}\")]", field.JsonFromField);
                    WriteLine(prefix + "public {0} {1} {{ get; set; }}", field.JsonToType, field.JsonToField);
                }

                WriteLine("    }");
                WriteLine("}");
                WriteLine();

            }
            Response.Flush();
            Response.End();

            Response.Write("<scrit>alert('哦yeah，成功啦!');</script>");
        }

        public void BindGridView()
        {
            gen.MappingList = gen.MappingList.OrderBy(x => x.Level).ToList();
            gvMappingList.DataSource = gen.MappingList;
            gvMappingList.DataBind();
        }

        #region Response WriteLine
        void WriteLine()
        {
            Response.Write("\r\n");
        }

        void WriteLine(string str)
        {
            Response.Write(str);
            Response.Write("\r\n");
        }

        void WriteLine(string str, string arg0)
        {
            var newStr = String.Format(str, arg0);
            Response.Write(newStr);
            Response.Write("\r\n");
        }

        void WriteLine(string str, string arg0, string arg1)
        {
            var newStr = String.Format(str, arg0, arg1);
            Response.Write(newStr);
            Response.Write("\r\n");
        }
        #endregion
    }
}