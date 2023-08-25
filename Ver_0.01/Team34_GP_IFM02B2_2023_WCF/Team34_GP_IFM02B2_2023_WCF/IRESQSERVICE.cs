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
        bool RegUser(String fName, String lName, String uEmail,  int uType, String uPass, String phone,  String uReg);

        [OperationContract]
        UserRecord loginUser(String uEmail, String pass);

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

        String _userEmail;
        String _fName;
        String _lName;
        String _userReg;
        String _userPhone;
        int _userType;

        [DataMember]
        public String fName
        {
            get { return _fName; }
            set { _fName = value; }
        }

        [DataMember]
        public String lName
        {
            get { return _lName; }
            set { _lName = value; }
        }

        [DataMember]
        public string userEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        [DataMember]
        public string userReg
        {
            get { return _userReg; }
            set { _userReg = value; }
        }

        [DataMember]
        public string userPhone
        {
            get { return _userPhone; }
            set { _userPhone = value; }
        }

        [DataMember]
        public int userType
        {
            get { return _userType; }
            set { _userType = value; }
        }
    }



    [DataContract]
    public class ProductRecord
    {
        int _prodID;
        int _userID;
        String _prodName;
        String _prodDesc;
        String _prodPic;
        double _prodPrice;
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

        public String prodPic 
        {
            get { return _prodPic; }
            set { _prodPic = value; }
        }

        [DataMember]
        public double prodPrice
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

