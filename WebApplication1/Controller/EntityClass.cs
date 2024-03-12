/// <summary>
/// Summary description for EntityClass
/// </summary>
public class EntityClass
{
    #region Private Member

    private string _StudName = string.Empty;
    private string _StudRollno = string.Empty;
    private string _StudEnrolNO = string.Empty;
    private string _StudDegree = string.Empty;
    private string _StudBranch = string.Empty;
    private string _StudAddress = string.Empty;
    private string _StudMobileNo = string.Empty;
    private string _StudPincode = string.Empty;
    private string _StudEmailID = string.Empty;
    private string _StudPaymentNo = string.Empty;
    private string _StudPayDate = string.Empty;
    private int _SrNo = 0;
    private string _StudeRegMobileNO = string.Empty;
    private string _StudFatherName = string.Empty;
    private string _StudMotherName = string.Empty;
    private string _StudFeeStatus = string.Empty;
    private string _StudMaxMark = string.Empty;
    private string _StudOBTMark = string.Empty;
    private string _StudDivision = string.Empty;
    private string _StudPassSession = string.Empty;
    private string _StudType = string.Empty;
    private string _AadhaarNo = string.Empty;
    private string _City = string.Empty;
    private string _State = string.Empty;
    private string _HouseNo = string.Empty;
    private string _Amount = string.Empty;
    private string _District = string.Empty;

    //add private member for provisional degree --25092017-- dipali
    private string _StudSeesion = string.Empty;

    private string _Studcity = string.Empty;
    private string _StudPaymentTyp = string.Empty;
    private string _StudAadhaarNo = string.Empty;
    private string _StudCAddress = string.Empty;
    private string _StudCPincode = string.Empty;
    private string _StudCcity = string.Empty;
    private string _StudAmount = string.Empty;

    //------check activty ----- 19112017

    private string _StudCounter = string.Empty;
    private string _StudReceptCode = string.Empty;
    private string _StudReceptNo = string.Empty;

    //----
    //-----29112017
    private string _StudCNID = string.Empty;

    //--- 14052018

    private string _studNameHindi = string.Empty;
    private string _studContactNo = string.Empty;

    //---02102018
    private int _ProvisionalPay = 0;

    //--25102018

    private int _CTC = 0;
    private string _Compname = string.Empty;
    private string _EmpStatus = string.Empty;
    private string _Heighrstudy = string.Empty;
    private string _InstitueName = string.Empty;

    #endregion Private Member

    #region Public Member

    //----25102018
    public int CTC
    {
        get { return _CTC; }
        set { _CTC = value; }
    }

    public string Compname
    {
        get { return _Compname; }
        set { _Compname = value; }
    }

    public string Empstatus
    {
        get { return _EmpStatus; }
        set { _EmpStatus = value; }
    }

    public string Heighrstudy
    {
        get { return _Heighrstudy; }
        set { _Heighrstudy = value; }
    }

    public string InstitueName
    {
        get { return _InstitueName; }
        set { _InstitueName = value; }
    }

    //------ end --------------25102018------ ///

    //----02102018
    public int ProvisionalPay
    {
        get { return _ProvisionalPay; }
        set { _ProvisionalPay = value; }
    }

    public string District
    {
        get { return _District; }
        set { _District = value; }
    }

    public string Amount
    {
        get { return _Amount; }
        set { _Amount = value; }
    }

    public string City
    {
        get { return _City; }
        set { _City = value; }
    }

    public string State
    {
        get { return _State; }
        set { _State = value; }
    }

    public string HouseNo
    {
        get { return _HouseNo; }
        set { _HouseNo = value; }
    }

    public string AadhaarNo
    {
        get { return _AadhaarNo; }
        set { _AadhaarNo = value; }
    }

    public string StudType
    {
        get { return _StudType; }
        set { _StudType = value; }
    }

    public string StudPassSession
    {
        get { return _StudPassSession; }
        set { _StudPassSession = value; }
    }

    public string StudDivision
    {
        get { return _StudDivision; }
        set { _StudDivision = value; }
    }

    public string StudOBTMark
    {
        get { return _StudOBTMark; }
        set { _StudOBTMark = value; }
    }

    public string StudMaxMark
    {
        get { return _StudMaxMark; }
        set { _StudMaxMark = value; }
    }

    public string StudFeeStatus
    {
        get { return _StudFeeStatus; }
        set { _StudFeeStatus = value; }
    }

    public string StudMotherName
    {
        get { return _StudMotherName; }
        set { _StudMotherName = value; }
    }

    public string StudFatherName
    {
        get { return _StudFatherName; }
        set { _StudFatherName = value; }
    }

    public string StudeRegMobileNO
    {
        get { return _StudeRegMobileNO; }
        set { _StudeRegMobileNO = value; }
    }

    public int SrNo
    {
        get { return _SrNo; }
        set { _SrNo = value; }
    }

    public string StudName
    {
        get { return _StudName; }
        set { _StudName = value; }
    }

    public string StudRollno
    {
        get { return _StudRollno; }
        set { _StudRollno = value; }
    }

    public string StudEnrolNO
    {
        get { return _StudEnrolNO; }
        set { _StudEnrolNO = value; }
    }

    public string StudDegree
    {
        get { return _StudDegree; }
        set { _StudDegree = value; }
    }

    public string StudBranch
    {
        get { return _StudBranch; }
        set { _StudBranch = value; }
    }

    public string StudAddress
    {
        get { return _StudAddress; }
        set { _StudAddress = value; }
    }

    public string StudMobileNo
    {
        get { return _StudMobileNo; }
        set { _StudMobileNo = value; }
    }

    public string StudPincode
    {
        get { return _StudPincode; }
        set { _StudPincode = value; }
    }

    public string StudEmailID
    {
        get { return _StudEmailID; }
        set { _StudEmailID = value; }
    }

    public string StudPaymentNo
    {
        get { return _StudPaymentNo; }
        set { _StudPaymentNo = value; }
    }

    public string StudPayDate
    {
        get { return _StudPayDate; }
        set { _StudPayDate = value; }
    }

    //---added 25/09/2017
    public string StudSeesion
    {
        get { return _StudSeesion; }
        set { _StudSeesion = value; }
    }

    public string Studcity
    {
        get { return _Studcity; }
        set { _Studcity = value; }
    }

    public string StudPaymentTyp
    {
        get { return _StudPaymentTyp; }
        set { _StudPaymentTyp = value; }
    }

    public string StudAadhaarNo
    {
        get { return _StudAadhaarNo; }
        set { _StudAadhaarNo = value; }
    }

    public string StudCAddress
    {
        get { return _StudCAddress; }
        set { _StudCAddress = value; }
    }

    public string StudCPincode
    {
        get { return _StudCPincode; }
        set { _StudCPincode = value; }
    }

    public string StudCcity
    {
        get { return _StudCcity; }
        set { _StudCcity = value; }
    }

    public string StudAmount
    {
        get { return _StudAmount; }
        set { _StudAmount = value; }
    }

    //----Payment----------------------22112017
    public string StudCounter
    {
        get { return _StudCounter; }
        set { _StudCounter = value; }
    }

    public string StudReceptCode
    {
        get { return _StudReceptCode; }
        set { _StudReceptCode = value; }
    }

    public string StudReceptNo
    {
        get { return _StudReceptNo; }
        set { _StudReceptNo = value; }
    }

    //-------------------------------------
    //----29112017
    public string StudCNID
    {
        get { return _StudCNID; }
        set { _StudCNID = value; }
    }

    //--14052018
    public string StudNameHindi
    {
        get { return _studNameHindi; }
        set { _studNameHindi = value; }
    }

    public string StudContactNo
    {
        get { return _studContactNo; }
        set { _studContactNo = value; }
    }

    #endregion Public Member
}