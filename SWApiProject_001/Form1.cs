using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SWApiProject_001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_drw_Click(object sender, EventArgs e)
        {
            SldWorks swApp = new SldWorks();

            ModelDoc2 swModel;
            Feature swFeature;
            Configuration config;
            CustomPropertyManager cusPropMgr;


            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top","PLANE",0,0,0,false,0,null,0 );

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(0, 0, 0, 0.5);
            swModel.CreateCircleByRadius2(0,0,0,0.2);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "PipeSketch";

            status = swModel.Extension.SelectByID2("PipeSketch","SKETCH",0,0,0,false,0,null,0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, 0.8, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "PipeModel";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description",(int)swCustomInfoType_e.swCustomInfoText,"Pipe / Boru",(int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, "800mm.", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

        }

    }
    
}
