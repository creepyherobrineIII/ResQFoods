﻿using System;
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
        UserTable getAdmin(String uEmail);

        [OperationContract]
        UserTable getUser(String uEmail, int type);

        [OperationContract]
        Customer getCustomer(String uEmail);

        [OperationContract]
        Store getStore(String uEmail);

        [OperationContract]
        List<Store> getStores();

        [OperationContract]
        List<UserTable> GetEmployeeRecords(String uEmail);

        [OperationContract]
        List<Product> getAllProducts();

        [OperationContract]
        List<Product> SearchProducts(String name);

        [OperationContract]
        bool AddProduct(int sID, String name, String desc, int quant, double price, String picPath, DateTime date, bool enabled);

        [OperationContract]
        bool AddToCart(int pID, int uID, DateTime added, bool enabled);

        [OperationContract]
        List<CartItem> GetCart(int UID);

        [OperationContract]
        bool editProduct(Product P);

        [OperationContract]
        bool editUser(UserTable U);

        [OperationContract]
        bool editCustomer(Customer C);

        [OperationContract]
        bool editStore(Store S);

        [OperationContract]
        bool addInvoice(int UID, double price, DateTime TOS, List<CartItem> cart);

        [OperationContract]
        bool addInvoiceItem(int ID, CartItem c);

        [OperationContract]
        List<Product> getFilteredList(String name, double P1, double P2, int tag, int manu);

        [OperationContract]
        List<Invoice> getInvoices(int UID);

        [OperationContract]
        Invoice getInvoice(int IID);

        [OperationContract]
        List<InvoiceItem> getInvoiceItems(int IID);

        [OperationContract]
        Product getProduct(int pID);

        [OperationContract]
        List<Tag> getTags();

    }


}

