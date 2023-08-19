using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Team34_GP_IFM02B2_2023_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRESQSERVICE
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        bool RegUser(UserRecord rec);

        [OperationContract]
        bool loginUser(String user, String pass);

        [OperationContract]
        List<UserRecord> GetEmployeeRecords();

        [OperationContract]
        string DeleteUser(UserRecord user);

        [OperationContract]
        List<UserRecord> SearchEmployeeRecord(UserRecord user);

        [OperationContract]
        string UpdateEmployeeContact(UserRecord user);
    }


    [DataContract]
    public class UserRecord
    {

        string _userID = "";
        string _userEmail = "";
        string _userPass = "";
        string _userReg = "";
        char _userType = 'N';
        bool _userActive;

        [DataMember]
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        [DataMember]
        public string userEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        [DataMember]
        public string userPass
        {
            get { return _userPass; }
            set { _userPass = value; }
        }

        public string userReg
        {
            get { return _userReg; }
            set { _userPass = value; }
        }

        [DataMember]
        public char userType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        [DataMember]
        public bool userActive
        {
            get { return _userActive; }
            set { _userActive = value; }
        }
    }
}

