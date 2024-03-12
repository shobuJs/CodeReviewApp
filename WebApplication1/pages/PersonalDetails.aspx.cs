using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_PersonalDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                ViewState["usertype"] = Session["usertype"];
                this.FillDropDown();
                ShowStudentDetails();
                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetails.Visible = false;
                    divhome.Visible = false;
                    divtxtidno.Visible = false;
                    txtIDNo.Visible = false;
                    txtStudMobile.Enabled = false;
                    txtStudentEmail.Enabled = false;
                    txtStudFullname.Enabled = false;
                }
                else
                {
                    txtIDNo.Visible = true;
                    divtxtidno.Visible = true;
                    divadmissiondetails.Visible = true;
                    divhome.Visible = true;
                    txtEnrollno.Enabled = true;
                    txtStudMobile.Enabled = true;
                    txtStudentEmail.Enabled = true;
                    txtStudFullname.Enabled = true;
                }
            }

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0", "NATIONALITYNO");
            ddlNationality.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0", "RELIGION");
            objCommon.FillDropDownList(ddlOccupationNo, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCNAME");
            objCommon.FillDropDownList(ddlMotherOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCNAME");
            objCommon.FillDropDownList(ddlCasteCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
            objCommon.FillDropDownList(ddlClaimedcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
            objCommon.FillDropDownList(ddlCaste, "ACD_CASTE", "CASTENO", "CASTE", "CASTENO>0", "CASTE");
            objCommon.FillDropDownList(ddlHandicap, "ACD_PHYSICAL_HANDICAPPED", "HANDICAP_NO", "HANDICAP_NAME", "HANDICAP_NO>0", "HANDICAP_NO");
            objCommon.FillDropDownList(ddlBloodGroupNo, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNAME");
            ddlHandicap.SelectedValue = "1";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataTableReader dtr = null;
            if (ViewState["usertype"].ToString() == "2")
            {
                dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
                txtStudentName.ReadOnly = false;
                txtStudentName.Visible = true;
                ddlHandicap.Enabled = true;
            }
            else
            {
                dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
                ddlHandicap.Enabled = true;
            }
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    Session["stuinfoenrollno"] = dtr["REGNO"].ToString();

                    txtIDNo.Text = dtr["IDNO"].ToString();
                    txtIDNo.ToolTip = dtr["REGNO"].ToString();
                    txtRegNo.ToolTip = dtr["REGNO"].ToString();

                    txtEnrollno.ToolTip = dtr["ENROLLNO"].ToString();
                    txtEnrollno.Text = dtr["ENROLLNO"].ToString();
                    txtsrno.Text = dtr["ENROLLNO"].ToString();
                    txtsrno.ToolTip = dtr["ENROLLNO"].ToString();
                    txtRegNo.Text = dtr["REGNO"].ToString();
                    txtStudFullname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    txtStudentName.Text = dtr["STUDFIRSTNAME"] == null ? string.Empty : dtr["STUDFIRSTNAME"].ToString();
                    txtStudMiddleName.Text = dtr["STUDMIDDLENAME"] == null ? string.Empty : dtr["STUDMIDDLENAME"].ToString();
                    txtStudLastName.Text = dtr["STUDLASTNAME"] == null ? string.Empty : dtr["STUDLASTNAME"].ToString();
                    txtFatherName.Text = dtr["FATHERFIRSTNAME"] == null ? string.Empty : dtr["FATHERFIRSTNAME"].ToString().ToUpper();
                    txtFatherFullName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                    txtFatherMiddleName.Text = dtr["FATHERMIDDLENAME"] == null ? string.Empty : dtr["FATHERMIDDLENAME"].ToString().ToUpper();
                    txtFatherLastName.Text = dtr["FATHERLASTNAME"] == null ? string.Empty : dtr["FATHERLASTNAME"].ToString().ToUpper();
                    txtFatherMobile.Text = dtr["FATHERMOBILE"] == null ? string.Empty : dtr["FATHERMOBILE"].ToString();
                    txtAnnualIncome.Text = dtr["ANNUAL_INCOME"] == null ? string.Empty : dtr["ANNUAL_INCOME"].ToString();
                    txtFathersOfficeNo.Text = dtr["FATHEROFFICENO"] == null ? string.Empty : dtr["FATHEROFFICENO"].ToString();
                    txtMotherName.Text = dtr["MOTHERNAME"] == null ? string.Empty : dtr["MOTHERNAME"].ToString().ToUpper();
                    txtMotherMobile.Text = dtr["MOTHERMOBILE"] == null ? string.Empty : dtr["MOTHERMOBILE"].ToString();
                    txtMothersOfficeNo.Text = dtr["MOTHEROFFICENO"] == null ? string.Empty : dtr["MOTHEROFFICENO"].ToString();
                    txtDateOfBirth.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    txtStudentEmail.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                    txtInstituteEmail.Text = dtr["EMAILID_INS"] == null ? string.Empty : dtr["EMAILID_INS"].ToString();
                    txtStudMobile.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                    txtSubCaste.Text = dtr["SUB_CASTE"] == null ? string.Empty : dtr["SUB_CASTE"].ToString();

                    rdobtn_Gender.SelectedValue = dtr["SEX"].ToString();//*****************

                    rdobtn_marital.SelectedValue = dtr["MARRIED"].ToString();//****************

                    ddlBloodGroupNo.SelectedValue = dtr["BLOODGRPNO"] == null ? "0" : dtr["BLOODGRPNO"].ToString();

                    ddlNationality.SelectedValue = dtr["NATIONALITYNO"] == null ? "0" : dtr["NATIONALITYNO"].ToString();
                    if (Convert.ToInt32(ddlNationality.SelectedValue) == 0)
                    {
                        ddlNationality.SelectedIndex = 1;
                    }

                    ddlCaste.SelectedValue = dtr["CASTE"] == null ? "0" : dtr["CASTE"].ToString();

                    if (dtr["CATEGORYNO"].ToString() == "0" || dtr["CATEGORYNO"].ToString() == null)
                    {
                        objCommon.FillDropDownList(ddlCasteCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
                        ddlCasteCategory.Enabled = true;
                    }
                    else
                    {
                        ddlCasteCategory.SelectedValue = dtr["CATEGORYNO"] == null ? "0" : dtr["CATEGORYNO"].ToString();
                        if (ViewState["usertype"].ToString() == "2")
                        {
                            ddlCasteCategory.Enabled = false;
                        }
                        else if (ViewState["usertype"].ToString() == "1")
                        {
                            ddlCasteCategory.Enabled = true;
                        }
                    }
                    if (dtr["PAYTYPENO"].ToString() == "0" || dtr["PAYTYPENO"].ToString() == null)
                    {
                        objCommon.FillDropDownList(ddlClaimedcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
                        ddlClaimedcategory.Enabled = true;
                    }
                    else
                    {
                        ddlClaimedcategory.SelectedValue = dtr["CLAIMID"] == null ? "0" : dtr["CLAIMID"].ToString();
                        if (ViewState["usertype"].ToString() == "2")
                        {
                            ddlClaimedcategory.Enabled = false;
                        }
                        else if (ViewState["usertype"].ToString() == "1")
                        {
                            ddlClaimedcategory.Enabled = true;
                        }
                    }

                    ddlReligion.SelectedValue = dtr["RELIGIONNO"] == null ? "0" : dtr["RELIGIONNO"].ToString();

                    ddlHandicap.SelectedValue = dtr["TYPE_OF_PHYSICALLY_HANDICAP"] == null ? "0" : dtr["TYPE_OF_PHYSICALLY_HANDICAP"].ToString();
                    if (ddlHandicap.SelectedValue == "0")
                    {
                        ddlHandicap.SelectedValue = "1";
                    }
                    txtAddharCardNo.Text = dtr["ADDHARCARDNO"] == null ? "0" : dtr["ADDHARCARDNO"].ToString();
                    txtFatherDesignation.Text = dtr["FATHER_DESIG"] == null ? string.Empty : dtr["FATHER_DESIG"].ToString();
                    ddlOccupationNo.SelectedValue = dtr["OCCUPATIONNO"] == null ? "0" : dtr["OCCUPATIONNO"].ToString();
                    ddlMotherOccupation.SelectedValue = dtr["MOTHER_OCCUPATIONNO"] == null ? "0" : dtr["MOTHER_OCCUPATIONNO"].ToString();
                    txtMotherDesignation.Text = dtr["MOTHER_DESIG"] == null ? string.Empty : dtr["MOTHER_DESIG"].ToString();

                    string idno = objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
                    if (idno != "")
                    {
                        //string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));       //Commented by Irfan Shaikh on 20190611 
                        //string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));  //Commented by Irfan Shaikh on 20190611 

                        string imgphoto = objCommon.LookUp("[DB_SVCE_IMAGES].[dbo].[ACD_STUD_PHOTO]", "photo", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));       //Added by Irfan Shaikh on 20190611 
                        string signphoto = objCommon.LookUp("[DB_SVCE_IMAGES].[dbo].[ACD_STUD_PHOTO]", "stud_sign", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));  //Added by Irfan Shaikh on 20190611 
                        if (imgphoto == string.Empty)
                        {
                            imgPhoto.ImageUrl = "~/images/nophoto.jpg";
                        }
                        else
                        {
                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                        }

                        if (signphoto == string.Empty)
                        {

                            ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
                        }
                        else
                        {
                            ImgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=studentsign";
                        }
                    }
                    else
                    {
                        imgPhoto.ImageUrl = null;
                        ImgSign.ImageUrl = null;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        string IndusEmail = string.Empty;
        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {
                objS.IdNo = Convert.ToInt32(txtIDNo.Text);
                objS.EnrollNo = txtEnrollno.Text.Trim();
                objS.RegNo = txtRegNo.Text.Trim();
                if (!txtStudFullname.Text.Trim().Equals(string.Empty)) objS.StudName = txtStudFullname.Text.Trim();
                if (!txtStudentName.Text.Trim().Equals(string.Empty)) objS.firstName = txtStudentName.Text.Trim();
                if (!txtStudMiddleName.Text.Trim().Equals(string.Empty)) objS.MiddleName = txtStudMiddleName.Text.Trim();
                if (!txtStudLastName.Text.Trim().Equals(string.Empty)) objS.LastName = txtStudLastName.Text.Trim();
                if (!txtFatherFullName.Text.Trim().Equals(string.Empty)) objS.FatherName = txtFatherFullName.Text.Trim();
                if (!txtFatherName.Text.Trim().Equals(string.Empty)) objS.fatherfirstName = txtFatherName.Text.Trim();
                if (!txtFatherMiddleName.Text.Trim().Equals(string.Empty)) objS.FatherMiddleName = txtFatherMiddleName.Text.Trim();

                if (!txtFatherLastName.Text.Trim().Equals(string.Empty)) objS.FatherLastName = txtFatherLastName.Text.Trim();
                if (!txtFatherMobile.Text.Trim().Equals(string.Empty)) objS.FatherMobile = txtFatherMobile.Text.Trim();
                if (!txtFathersOfficeNo.Text.Trim().Equals(string.Empty)) objS.FatherOfficeNo = txtFathersOfficeNo.Text.Trim();
                if (!txtFatherDesignation.Text.Trim().Equals(string.Empty)) objSAddress.FATHER_DESIG = txtFatherDesignation.Text.Trim();//Father Qualification
                objSAddress.OCCUPATION = Convert.ToInt32(ddlOccupationNo.SelectedValue);

                if (!txtfatheremailid.Text.Trim().Equals(string.Empty)) objS.Fatheremail = txtfatheremailid.Text.Trim();
                if (!txtMotherName.Text.Trim().Equals(string.Empty)) objS.MotherName = txtMotherName.Text.Trim();
                string MotherMobile = txtMotherMobile.Text.Trim();
                if (!txtmotheremailid.Text.Trim().Equals(string.Empty)) objS.Motheremail = txtmotheremailid.Text.Trim();
                if (!txtMotherDesignation.Text.Trim().Equals(string.Empty)) objSAddress.MOTHERDESIGNATION = txtMotherDesignation.Text.Trim();

                string MotherOfficeNo = txtMothersOfficeNo.Text.Trim();
                objSAddress.MOTHEROCCUPATION = Convert.ToInt32(ddlMotherOccupation.SelectedValue);
                objS.Caste = Convert.ToInt32(ddlCaste.SelectedValue);
                objS.Subcaste = txtSubCaste.Text.Trim();
                if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
                objS.Annual_income = txtAnnualIncome.Text.Trim();
                objS.BloodGroupNo = Convert.ToInt32(ddlBloodGroupNo.SelectedValue);

                objS.ClaimType = Convert.ToInt32(ddlClaimedcategory.SelectedValue);//for student we are showing claimed category

                objS.ReligionNo = Convert.ToInt32(ddlReligion.SelectedValue);
                objS.NationalityNo = Convert.ToInt32(ddlNationality.SelectedValue);
                objS.CategoryNo = Convert.ToInt32(ddlCasteCategory.SelectedValue);

                objS.Married = Convert.ToChar(rdobtn_marital.SelectedValue);
                if (!txtAddharCardNo.Text.Trim().Equals(string.Empty)) objS.AddharcardNo = txtAddharCardNo.Text.Trim();
                objS.Physical_Handicap = Convert.ToInt32(ddlHandicap.SelectedValue);
                objS.Sex = Convert.ToChar(rdobtn_Gender.SelectedValue);
                if (!txtInstituteEmail.Text.Trim().Equals(string.Empty)) IndusEmail = txtInstituteEmail.Text.Trim();

                if (!txtStudMobile.Text.Trim().Equals(string.Empty)) objS.StudentMobile = txtStudMobile.Text.Trim();
                if (!txtStudentEmail.Text.Trim().Equals(string.Empty)) objS.EmailID = txtStudentEmail.Text.Trim();
                objS.Age = "";

                if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
                {
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    objS.Uano = 0;
                }

                if (fuPhotoUpload.HasFile)
                {
                    objSPhoto.Photo1 = this.ResizePhoto(fuPhotoUpload);
                }
                else
                {
                    objSPhoto.Photo1 = null;
                }
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentPersonalInformation(objS, objSAddress, objSPhoto, objSQualExam, MotherMobile, MotherOfficeNo, IndusEmail, Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();
                    divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Student Personal Information Updated Successfully.'); </script>";
                    Response.Redirect("~/academic/AddressDetails.aspx", false);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "Error Occured While Updating Personal Information!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpersonalinformation, "You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnGohome_Click(object sender, EventArgs e)
    {

    }

    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }

    protected void btnPhotoUpload_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            string ext = System.IO.Path.GetExtension(fuPhotoUpload.PostedFile.FileName);

            if (fuPhotoUpload.HasFile)
            {
                if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG" || ext.ToUpper().Trim() == ".GIF")
                {

                    if (fuPhotoUpload.PostedFile.ContentLength < 40960)
                    {

                        byte[] resizephoto = ResizePhoto(fuPhotoUpload);

                        if (resizephoto.LongLength >= 40960)
                        {
                            objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 40kb", this.Page);
                            return;
                        }
                        else
                        {
                            objstud.StudPhoto = this.ResizePhoto(fuPhotoUpload);
                            objstud.IdNo = Convert.ToInt32(txtIDNo.Text);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 40kb", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "Only JPG,JPEG,PNG files are allowed!", this.Page);
                    return;
                }
            }
            else
            {
                System.IO.FileStream ff = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/logo.gif"), System.IO.FileMode.Open);
                int ImageSize = (int)ff.Length;
                byte[] ImageContent = new byte[ff.Length];
                ff.Read(ImageContent, 0, ImageSize);
                ff.Close();
                ff.Dispose();
                objstud.StudSign = ImageContent;
                objCommon.DisplayMessage(this.updpersonalinformation, "Please select file!", this.Page);
            }

            CustomStatus cs = (CustomStatus)objEc.UpdateStudPhoto(objstud);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updpersonalinformation, "Student Photo upload Successfully!!", this.Page);
                showstudentphoto();
            }
            else
                objCommon.DisplayMessage("Error!!", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "studentinfo.btnUpload_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void showstudentphoto()
    {
        string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
        if (idno != "")
        {
            string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));

            if (imgphoto == string.Empty)
            {
                imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            }
            else
            {
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + txtIDNo.Text.Trim().ToString() + "&type=student";
            }
        }
        else
        {
            imgPhoto.ImageUrl = null;
        }
    }

    private void showstudentsignature()
    {
        string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
        if (idno != "")
        {
            string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));

            if (signphoto == string.Empty)
            {
                ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
            }
            else
            {
                ImgSign.ImageUrl = "~/showimage.aspx?id=" + txtIDNo.Text.Trim().ToString() + "&type=studentsign";
            }
        }
        else
        {
            ImgSign.ImageUrl = null;
        }
    }

    protected void btnSignUpload_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            string ext = System.IO.Path.GetExtension(this.fuSignUpload.PostedFile.FileName);
            if (fuSignUpload.HasFile)
            {
                if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG")
                {
                    if (fuSignUpload.PostedFile.ContentLength < 40960)
                    {

                        byte[] resizephoto = ResizePhoto(fuSignUpload);

                        if (resizephoto.LongLength >= 40960)
                        {
                            objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 40kb", this.Page);
                            return;
                        }
                        else
                        {
                            objstud.StudSign = this.ResizePhoto(fuSignUpload);
                            objstud.IdNo = Convert.ToInt32(txtIDNo.Text);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 40kb", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "Only JPG,JPEG,PNG files are allowed!", this.Page);
                    return;
                }
            }
            else
            {
                System.IO.FileStream ff = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/logo.gif"), System.IO.FileMode.Open);
                int ImageSize = (int)ff.Length;
                byte[] ImageContent = new byte[ff.Length];
                ff.Read(ImageContent, 0, ImageSize);
                ff.Close();
                ff.Dispose();
                objstud.StudSign = ImageContent;
                objCommon.DisplayMessage(this.updpersonalinformation, "Please select file!", this.Page);
            }

            CustomStatus cs = (CustomStatus)objEc.UpdateStudSign(objstud);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updpersonalinformation, "Student Sign upload Successfully!!", this.Page);
                showstudentsignature();
            }
            else
                objCommon.DisplayMessage("Error!!", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "studentinfo.btnUpload_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/PersonalDetails.aspx");
    }

    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AddressDetails.aspx");
    }

    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AdmissionDetails.aspx");
    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }

    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }

    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/OtherInformation.aspx");
    }

    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Admission Form Report " + txtRegNo.Text;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt16(txtIDNo.Text.Trim().ToString()) + "";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpersonalinformation, this.updpersonalinformation.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            Session["admidstatus"] = 1;
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
        }
        else
        {
            Session["admidstatus"] = 0;
        }
        Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
    }
}