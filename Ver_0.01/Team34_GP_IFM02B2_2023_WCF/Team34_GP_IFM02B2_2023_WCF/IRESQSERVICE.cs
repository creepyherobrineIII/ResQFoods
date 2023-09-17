using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.ServiceModel;

//FOR USERTYPE IN USERTABLE (Customer = 0) (Store = 1) (Admin = 2)
namespace Team34_GP_IFM02B2_2023_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRESQSERVICE
    {
        //LOGIN AND REGISTER
        [OperationContract]
        int loginUser(String user, String pass); //Login User

        [OperationContract]
        bool regAdmin(String uEmail, String uPass); //Register Admin (Remember logic is on client side)

        [OperationContract]
        bool regCust(String uEmail, String uPass, String fName, String lName, DateTime BDate, bool grant); //Register customer 

        [OperationContract]
        bool regStore(String uEmail, String uPass, String comp, String name, String icoPath, String loc, String type); //Register Store


        //CRU FUNCTIONS

        //GET FUNCTIONS  //Delete this comment when done, i just renamed some of the functions to be consistent. like all "gets" are lowercase now and stuff like getEmployeeRecords -> getAllEmployees

        [OperationContract]
        UserTable getUser(String uEmail); //gets called in both getAdmin or getCustomer or getStore since it is the base type

        [OperationContract]
        UserTable getAdmin(String uEmail);

        [OperationContract]
        Customer getCustomer(String uEmail);

        [OperationContract]
        Store getStore(String uEmail);

        [OperationContract]
        List<CartItem> getCartItems(int uId); //Cart needs userID (from session variable on client side) then retreives items  C
        
        [OperationContract]
        List<Product> getCartProducts(int uId); //This function is meant to call the getcartItems. What it will do is look at the cartitems and "connect" it to products (because we need the product information for display) 

        [OperationContract]
        List<Product> searchProduct(string name);


        //ADD FUNCTIONS
        [OperationContract]
        bool addProduct(int sID, String name, String desc, double price, String picPath, DateTime date, bool enabled);

        [OperationContract]
        bool addToCart(int pID, int uID, DateTime added); // removed boolean attribute paramater since it should be enabled when added

        
        //UPDATE FUNCTIONS
        //[OperationContract]
        //bool updateUser(String uEmail);

        bool updateCart(int CartID); //update the cart to be enabled or disabled



        //Commented Due to being taught not to delete? rather just update to "inactive/null" in database C
        //[OperationContract]
        //string DeleteUser(String uEmail);
    }

}

