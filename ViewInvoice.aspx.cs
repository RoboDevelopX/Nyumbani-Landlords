using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nyumbani_Landlords
{
    public partial class ViewInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["LandLordInfo"] == null)
            {
                Response.Redirect("login.aspx", false);
                return;
            }


            DataTable dtInvoice = new DataTable();
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("guz-KE");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("guz-KE");

                if (Session["LandLordInfo"] != null)
                {
                    int landlordId = Convert.ToInt32(Session["LandLordID"]);

                    dtInvoice = ClassLibrary_PropertyManager.Controller.cInvoice.GetInvoiceByLandLordID(landlordId);


                    {
                        GridView1.DataSource = dtInvoice;
                        GridView1.DataBind();

                        Session["dtInvoice"] = dtInvoice;
                    }

                }
            }
            catch (Exception ioExp)
            {

                using (StreamWriter sw = new StreamWriter(Global.ErrorFilePath, true))
                { // If file exists, text will be appended ; otherwise a new file will be created
                    sw.Write(string.Format("Message: {0}<br />{1}StackTrace :{2}{1}Date :{3}{1}-----------------------------------------------------------------------------{1}",
                      ioExp.Message, Environment.NewLine, ioExp.StackTrace, DateTime.Now.ToString()));
                }
                divMsgError.Visible = true;
            }
            finally
            {
                dtInvoice.Dispose();

            }
        }

        public string ShowStatus(string _status)
        {

            string sreturnStatusMsg = "";

            if (_status == "0")
            {
                sreturnStatusMsg = "<span class='badge badge-danger' >Pending  </span>";
            }
            else
            {
                sreturnStatusMsg = "<span class='badge badge-success'>Cleared</span>"; ;
            }


            return sreturnStatusMsg;
        }

        public string ShowDate(DateTime _date)
        {
            string newDate = _date.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

            return newDate;
        }
    }
}