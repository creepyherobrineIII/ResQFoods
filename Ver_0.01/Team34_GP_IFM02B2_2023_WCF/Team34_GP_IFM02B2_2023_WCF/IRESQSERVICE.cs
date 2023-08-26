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
        int loginUser(String user, String pass);

        [OperationContract]
        bool regAdmin(String uEmail, String uPass);

        [OperationContract]
        bool regCust(String uEmail, String uPass, String fName, String lName, DateTime BDate, bool grant);

        [OperationContract]
        bool regStore(String uEmail, String uPass, String comp, String name, String icoPath, String loc, String type);

        [OperationContract]
        UserRecord getAdmin(String uEmail);

        [OperationContract]
        CustomerRecord getCustomer(String uEmail);

        [OperationContract]
        StoreRecord getStore(String uEmail);

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

        [OperationContract]
        List<ProductRecord> SearchProducts(String name);

        [OperationContract]
        bool AddProduct(int sID, String name, String desc, double price, String picPath, DateTime date, bool enabled);


    }


    [DataContract]
    public class UserRecord
    {
        int _userId;
        String _userEmail;
        DateTime _userReg;
        int _userType;
        bool _enabled;


        [DataMember]
        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        [DataMember]
        public string userEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        [DataMember]
        public DateTime userReg
        {
            get { return _userReg; }
            set { _userReg = value; }
        }

        [DataMember]
        public int userType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        [DataMember]
        public bool enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
    }


    [DataContract]
    public class CustomerRecord
    {
        UserRecord _u;
        String _fName;
        String _lName;
        DateTime _birthDate;
        bool _grantRec;


        [DataMember]
        public UserRecord u
        {
            get { return _u; }
            set { _u = value; }
        }

        [DataMember]
        public string fName
        {
            get { return _fName; }
            set { _fName = value; }
        }
        [DataMember]
        public string lName
        {
            get { return _lName; }
            set { _lName = value; }
        }

        [DataMember]
        public DateTime birthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        [DataMember]
        public bool grantRec
        {
            get { return _grantRec; }
            set { _grantRec = value; }
        }

    }

    [DataContract]
    public class StoreRecord
    {
        UserRecord _u;
        String _company;
        String _name;
        String _logo;
        String _location;
        String _storeType;

        [DataMember]
        public UserRecord u
        {
            get { return _u; }
            set { _u = value; }
        }

        [DataMember]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember]
        public string logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        [DataMember]
        public string location
        {
            get { return _location; }
            set { _location = value; }
        }
        [DataMember]
        public string company
        {
            get { return _company; }
            set { _company = value; }
        }

        [DataMember]
        public string sType
        {
            get { return _storeType; }
            set { _storeType = value; }
        }
    }



    [DataContract]
    public class ProductRecord
    {
        int _storeId;
        int _prodId;
        String _prodName;
        String _prodDesc;
        String _prodPic;
        double _prodPrice;
        DateTime _prodDate;
        bool _enabled;

        [DataMember]
        public int prodId
        {
            get { return _prodId; }
            set { _prodId = value; }
        }


        [DataMember]
        public int storeId
        {
            get { return _storeId; }
            set { _storeId = value; }
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
        public DateTime prodDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        public bool enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
    }
}

