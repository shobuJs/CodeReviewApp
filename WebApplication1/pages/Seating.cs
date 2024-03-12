namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Seating
            {
                //Private Member
                private int _exDtNo = 0;

                private int _status = 0;

                //Public Property Feilds
                public int ExdtNo
                {
                    get
                    {
                        return _exDtNo;
                    }
                    set
                    {
                        _exDtNo = value;
                    }
                }

                public int Status
                {
                    get { return _status; }
                    set { _status = value; }
                }
            }
        }
    }
}