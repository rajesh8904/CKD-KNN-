﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

namespace diseasePrediction.Doctor
{
    public partial class _CMKNN : System.Web.UI.Page
    {
        Dictionary<string, double> testData = new Dictionary<string, double>();

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                _ConfusionMatrix();

                base.OnLoad(e);

                if (!IsPostBack)
                {
                    // bind chart type names to ddl
                    ddlChartType.DataSource = Enum.GetNames(typeof(SeriesChartType));
                    ddlChartType.DataBind();

                    //cbUse3D.Checked = true;
                }

                DataBind();

            }
            catch
            {

            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            testData.Clear();


            testData.Add("KNN_TP", _KNN_TP);
            testData.Add("KNN_TN", _KNN_TN);
            testData.Add("KNN_FP", _KNN_FP);
            testData.Add("KNN_FN", _KNN_FN);

            ccTestChart.Series["Testing"].Points.DataBind(testData, "Key", "Value", string.Empty);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // update chart rendering           
            ccTestChart.Series["Testing"].ChartTypeName = "Pie";

            ccTestChart.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
            ccTestChart.ChartAreas[0].Area3DStyle.Inclination = Convert.ToInt32(rblInclinationAngle.SelectedValue);

            ccTestChart.Visible = true;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ccTestChart.Visible = true;

            OnDataBinding(e);
            OnPreRender(e);
        }

        static double _KNN_TP, _KNN_TN, _KNN_FP, _KNN_FN;

        private void _ConfusionMatrix()
        {
            _KNN_TP = double.Parse(Request.QueryString["KNN_TP"].ToString());
            _KNN_TN = double.Parse(Request.QueryString["KNN_TN"].ToString());
            _KNN_FP = double.Parse(Request.QueryString["KNN_FP"].ToString());
            _KNN_FN = double.Parse(Request.QueryString["KNN_FN"].ToString());

            Table3.Rows.Clear();

            Table3.BorderStyle = BorderStyle.Double;
            Table3.GridLines = GridLines.Both;
            Table3.BorderColor = System.Drawing.Color.DarkGray;

            //mainrow
            TableRow mainrow = new TableRow();
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;
            mainrow.BackColor = System.Drawing.Color.DarkGoldenrod;

            TableCell cell1 = new TableCell();
            cell1.Width = 350;
            cell1.Text = "<b>TP</b>";
            mainrow.Controls.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Width = 200;
            cell2.Text = "<b>TN</b>";
            mainrow.Controls.Add(cell2);

            //TableCell cell4 = new TableCell();
            //cell4.Width = 200;
            //cell4.Text = "<b>KNN Algorithm</b>";
            //mainrow.Controls.Add(cell4);

            TableCell cell5 = new TableCell();
            cell5.Width = 200;
            cell5.Text = "<b>FP</b>";
            mainrow.Controls.Add(cell5);

            TableCell cell6 = new TableCell();
            cell6.Width = 200;
            cell6.Text = "<b>FN</b>";
            mainrow.Controls.Add(cell6);

            Table3.Controls.Add(mainrow);

            //1st row
            TableRow row1 = new TableRow();

            TableCell cellAcc = new TableCell();
            cellAcc.Text = _KNN_TP + "%";
            row1.Controls.Add(cellAcc);

            TableCell cellAccLVQ = new TableCell();
            cellAccLVQ.Text = _KNN_TN + "%";
            row1.Controls.Add(cellAccLVQ);


            TableCell cellAccDT = new TableCell();
            //cal           
            cellAccDT.Text = _KNN_FP + "%";
            row1.Controls.Add(cellAccDT);


            TableCell cellAccRF = new TableCell();
            //cal           
            cellAccRF.Text = _KNN_FN + "%";
            row1.Controls.Add(cellAccRF);

            Table3.Controls.Add(row1);


        }
    }
}