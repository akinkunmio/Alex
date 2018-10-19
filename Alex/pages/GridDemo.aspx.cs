using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alex.pages
{
    public partial class GridDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable("Authors");
                DataColumn dc1 = new DataColumn("AuthorId", typeof(int));
                DataColumn dc2 = new DataColumn("Name", typeof(string));
                DataColumn dc3 = new DataColumn("Points", typeof(int));
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                DataRow dr1 = dt.NewRow();
                dr1[0] = 100;
                dr1[1] = "Krishna Garad";
                dr1[2] = 40;
                dt.Rows.Add(dr1);
                DataRow dr2 = dt.NewRow();
                dr2[0] = 110;
                dr2[1] = "Vulpes";
                dr2[2] = 265;
                dt.Rows.Add(dr2);
                DataRow dr3 = dt.NewRow();
                dr3[0] = 120;
                dr3[1] = "Sp Nayak";
                dr3[2] = 165;
                dt.Rows.Add(dr3);
                gdvauthors.DataSource = dt;
                gdvauthors.DataBind();
            }
        }
        protected void imgbtn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            lblID.Text = gvrow.Cells[0].Text;
            txtname.Text = gvrow.Cells[1].Text;
            txtpoints.Text = gvrow.Cells[2].Text;
            this.ModalPopupExtender1.Show();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update the record here
            this.ModalPopupExtender1.Hide();
        }
    }
}