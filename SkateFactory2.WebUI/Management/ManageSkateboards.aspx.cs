using SkateFactory2.WebUI.SkateboardService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SkateFactory2.WebUI.Management
{
    public partial class ManageSkateboards : Page
    {
        #region Support methods
        private void DisplayListOfSkateboards()
        {
            var listOfSkateboards = new List<Skateboard>();

            ESkateboardCriteria criteria;
            if (rbDisplayAll.Checked)
                criteria = ESkateboardCriteria.All;
            else if (rbDisplayElectric.Checked)
                criteria = ESkateboardCriteria.Electric;
            else
                criteria = ESkateboardCriteria.Regular;

            try
            {
                //listOfSkateboards = GetListOfSkateboards_LocalDB(criteria);
                listOfSkateboards = GetListOfSkateboards_WS(criteria);
            }
            catch (Exception ex)
            {
                StaticMethods.DisplayMessage(ex.Message, this);
            }

            dgSkateboards.DataSource = listOfSkateboards;
            dgSkateboards.DataBind();
        }

        private void DisplaySkateboard(Skateboard skate)
        {
            txtName.Text = skate.Name;
            txtWeight.Text = skate.Weight.ToString();
            ddlColor.SelectedIndex = (int)skate.Color;
            ddlSkateType.SelectedIndex = (int)skate.SkateType;
            ddlShapeType.SelectedIndex = (int)skate.ShapeType;
            ddlBrakeType.SelectedIndex = (int)skate.BrakeType;
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtWeight.Text = "";
            ddlSkateType.SelectedIndex = 0;
            ddlShapeType.SelectedIndex = 0;
            ddlBrakeType.SelectedIndex = 0;
            ddlColor.SelectedIndex = 0;
        }

        private int FindColumn(string name)
        {
            int result = -1;
            for (int i = 0; i < dgSkateboards.Columns.Count; i++)
            {
                if (dgSkateboards.Columns[i] is BoundColumn)
                {
                    var boundColumn = (BoundColumn)dgSkateboards.Columns[i];
                    if (boundColumn.DataField == name)
                    {
                        result = i;
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region WS proxy methods vs Data proxy methods

        //private List<Skateboard> GetListOfSkateboards_LocalDB(ESkateboardCriteria criteria)
        //{
        //    return SkateboardData.GetList(criteria, GetConnectionString());
        //}

        private List<Skateboard> GetListOfSkateboards_WS(ESkateboardCriteria criteria)
        {
            var wsClient = new SkateboardServiceSoapClient();
            var wsResult = wsClient.GetList(criteria);
            return wsResult.ToList();
        }


        //private void Insert_LocalDB(Skateboard skate)
        //{
        //    SkateboardData.Insert(skate, GetConnectionString());
        //}

        private void Insert_WS(Skateboard skate)
        {
            var wsClient = new SkateboardServiceSoapClient();
            wsClient.Insert(skate);
        }


        //private void Update_LocalDB(Skateboard skate)
        //{
        //    SkateboardData.Update(skate, GetConnectionString());
        //}

        private void Update_WS(Skateboard skate)
        {
            var wsClient = new SkateboardServiceSoapClient();
            wsClient.Update(skate);
        }


        //private void SearchById_LocalDB(int skateboardId)
        //{
        //    var skate = SkateboardData.SearchById(skateboardId, GetConnectionString());
        //    DisplaySkateboard(skate);
        //}

        private void SearchById_WS(int skateboardId)
        {
            var wsClient = new SkateboardServiceSoapClient();
            var skate = wsClient.SearchById(skateboardId);
            DisplaySkateboard(skate);
        }


        //private void Delete_LocalDB(int skateboardId)
        //{
        //    SkateboardData.Delete(skateboardId, GetConnectionString());
        //}

        private void Delete_WS(int skateboardId)
        {
            var wsClient = new SkateboardServiceSoapClient();
            wsClient.Delete(skateboardId);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                DisplayListOfSkateboards();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var skate = new Skateboard();
            skate.Name = txtName.Text;
            skate.Weight = Convert.ToSingle(txtWeight.Text);
            skate.Color = (EColor)ddlColor.SelectedIndex;
            skate.SkateType = (ESkateType)ddlSkateType.SelectedIndex;
            skate.ShapeType = (EShapeType)ddlShapeType.SelectedIndex;
            skate.BrakeType = (EBrakeType)ddlBrakeType.SelectedIndex;

            try
            {
                //Insert_LocalDB(skate);
                Insert_WS(skate);
                ClearFields();
            }
            catch (Exception ex)
            {
                StaticMethods.DisplayMessage(ex.Message, this);
            }

            DisplayListOfSkateboards();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (dgSkateboards.SelectedIndex < 0)
            {
                StaticMethods.DisplayMessage("Please select a skateboard", this);
                return;
            }

            try
            {
                var skate = new Skateboard();
                skate.Id = Convert.ToInt32(dgSkateboards.SelectedItem.Cells[FindColumn("Id")].Text);
                skate.Name = txtName.Text;
                skate.Weight = Convert.ToSingle(txtWeight.Text);
                skate.Color = (EColor)ddlColor.SelectedIndex;
                skate.SkateType = (ESkateType)ddlSkateType.SelectedIndex;
                skate.ShapeType = (EShapeType)ddlShapeType.SelectedIndex;
                skate.BrakeType = (EBrakeType)ddlBrakeType.SelectedIndex;
                //Update_LocalDB(skate);
                Update_WS(skate);
                dgSkateboards.SelectedItem.Cells[FindColumn("Name")].Text = txtName.Text;
                dgSkateboards.SelectedItem.Cells[FindColumn("Weight")].Text = txtWeight.Text;
                dgSkateboards.SelectedItem.Cells[FindColumn("Color")].Text = ddlColor.Text;
                dgSkateboards.SelectedItem.Cells[FindColumn("SkateType")].Text = ddlSkateType.Text;
                dgSkateboards.SelectedItem.Cells[FindColumn("ShapeType")].Text = ddlShapeType.Text;
                dgSkateboards.SelectedItem.Cells[FindColumn("BrakeType")].Text = ddlBrakeType.Text;
            }
            catch (Exception ex)
            {
                StaticMethods.DisplayMessage(ex.Message, this);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool found = false;

            for (int i = 0; i < dgSkateboards.Items.Count; i++)
            {
                if (dgSkateboards.Items[i].Cells[1].Text == txtSearchById.Text)
                {
                    dgSkateboards.SelectedIndex = i;
                    dgSkateboards_SelectedIndexChanged(sender, e);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                StaticMethods.DisplayMessage("Skate not found", this);
                return;
            }
        }

        protected void cvSkateType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlSkateType.SelectedIndex > 0;
        }

        protected void cvWeight_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = float.TryParse(txtWeight.Text, out _);
        }

        protected void cvColor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlColor.SelectedIndex > 0;
        }

        protected void cvBrakeType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlBrakeType.SelectedIndex > 0;
        }

        protected void cvShapeType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlShapeType.SelectedIndex > 0;
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgSkateboards.SelectedIndex < 0)
            {
                StaticMethods.DisplayMessage("Please select a skateboard", this);
                return;
            }

            try
            {
                int skateboardId = Convert.ToInt32(dgSkateboards.SelectedItem.Cells[1].Text);
                //Delete_LocalDB(skateboardId);
                Delete_WS(skateboardId);
                DisplayListOfSkateboards();
                ClearFields();
            }
            catch (Exception ex)
            {
                StaticMethods.DisplayMessage(ex.Message, this);
            }
        }

        protected void rbDisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            DisplayListOfSkateboards();
        }

        protected void rbDisplayRegular_CheckedChanged(object sender, EventArgs e)
        {
            DisplayListOfSkateboards();
        }

        protected void rbDisplayElectric_CheckedChanged(object sender, EventArgs e)
        {
            DisplayListOfSkateboards();
        }

        protected void lkbSignOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/Default.aspx");
        }

        protected void dgSkateboards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgSkateboards.SelectedIndex < 0)
                return;

            int skateboardId = Convert.ToInt32(dgSkateboards.SelectedItem.Cells[1].Text);
            //SearchById_LocalDB(skateboardId);
            SearchById_WS(skateboardId);
        }

     
    }
}