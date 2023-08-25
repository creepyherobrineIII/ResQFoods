using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Team34_GP_IFM02B2_2023_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRESQSERVICE
    {

        [OperationContract]
        bool RegUser(String uEmail, char uType, String uPass, String uReg);

        [OperationContract]
        bool loginUser(String uEmail, String pass);

        [OperationContract]
        List<UserRecord> GetEmployeeRecords(String uEmail);

        [OperationContract]
        string DeleteUser(String uEmail);

        [OperationContract]
        List<UserRecord> SearchUser(String uEmail);

        [OperationContract]
        string UpdateUser(String uEmail);

        [OperationContract]
        List<ProductRecord> getAllProducts();


    }


    [DataContract]
    public class UserRecord
    {

        int _userID;
        string _userEmail;
        string _userPass;
        string _userReg;
        char _userType = 'N';
        bool _userActive;

        [DataMember]
        public int userID
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



    [DataContract]
    public class ProductRecord
    {
        int _prodID;
        int _userID;
        String _prodName;
        String _prodDesc;
        Image _prodPic;
        String _prodPrice;
        String _prodAdd;
        bool _enabled;

        [DataMember]
        public int prodID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        [DataMember]
        public int userID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        [DataMember]
        public string prodName
        {
            get { return _prodName; }
            set { _prodName = value; }
        }
        [DataMember]
        public string prodDesc
        {
            get { return _prodDesc; }
            set { _prodDesc = value; }
        }

        public Image prodPic 
        {
            get { return _prodPic; }
            set { _prodPic = value; }
        }

        [DataMember]
        public String prodPrice
        {
            get { return _prodPrice; }
            set { _prodPrice = value; }
        }

        [DataMember]
        public bool enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
    }
}

