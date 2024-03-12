using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
//namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class NotificationAndroid
    {
        #region Private Member

        private int _NotId;
        private String _NotType;
        private String _UserId;
        private String _Action;
        private String _MessageTitle;
        private String _Message;
        private String _DateTime;
        private int _ReadFlag;
        private String _ImageUrl;
        private String _FacultyName;
        private String _CourseName;
        private String _SlotName;
        private String _AttClassType;

        #endregion Private Member

        #region Public Properties

        public String FacultyName
        {
            get { return _FacultyName; }
            set { _FacultyName = value; }
        }

        public String CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }

        public String SlotName
        {
            get { return _SlotName; }
            set { _SlotName = value; }
        }

        public String AttClassType
        {
            get { return _AttClassType; }
            set { _AttClassType = value; }
        }

        public int NotId
        {
            get { return _NotId; }
            set { _NotId = value; }
        }

        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        public String NotType
        {
            get { return _NotType; }
            set { _NotType = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public String Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        public String MessageTitle
        {
            get { return _MessageTitle; }
            set { _MessageTitle = value; }
        }

        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public String DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }

        public int ReadFlag
        {
            get { return _ReadFlag; }
            set { _ReadFlag = value; }
        }

        #endregion Public Properties
    }
}