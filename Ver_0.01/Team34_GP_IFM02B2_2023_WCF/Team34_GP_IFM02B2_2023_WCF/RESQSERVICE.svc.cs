﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Team34_GP_IFM02B2_2023_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RESQSERVICE" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RESQSERVICE.svc or RESQSERVICE.svc.cs at the Solution Explorer and start debugging.
        // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
        // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
        public class RESQSERVICE : IRESQSERVICE
        {
            public string DeleteUser(UserRecord user)
            {
                throw new NotImplementedException();
            }

            public string GetData(int value)
            {
                return string.Format("You entered: {0}", value);
            }

            public List<UserRecord> GetEmployeeRecords()
            {
                throw new NotImplementedException();
            }

            private bool checkValid(String email)
            {
                bool valid = true;
                String con = "Data Source = DESKTOP - 69RGCI5; Initial Catalog = RESQFOODS; Integrated Security = True";
                String query = "SELECT Count(USER_EMAIL) FROM USERS WHERE USER_EMAIL = @userEmail ";
                SqlConnection conn = new SqlConnection(con);
                SqlCommand sCom = new SqlCommand(query, conn);
                sCom.Parameters.AddWithValue("@userEmail", email);
                try
                {
                    conn.Open();
                    Object count = sCom.ExecuteScalar();
                    Int32 var;
                    if (Int32.TryParse(count.ToString(), out var))
                    {
                        if (var != 0)
                        {
                            valid = false;
                        }
                    }
                }
                catch (SqlException)
                {
                    Console.WriteLine("SQL ERROR");
                }
                finally
                {
                    conn.Close();
                }
                return valid;
            }

            public bool loginUser(String user, String pass)
            {
                bool valid = false;
                String con = "Data Source = DESKTOP - 69RGCI5; Initial Catalog = RESQFOODS; Integrated Security = True";
                String query = "SELECT Count(USER_EMAIL) FROM USERS WHERE USER_EMAIL = @userEmail AND USER_PASS = @userPass";
                SqlConnection conn = new SqlConnection(con);
                SqlCommand sCom = new SqlCommand(query, conn);
                sCom.Parameters.AddWithValue("@userEmail", user);
                sCom.Parameters.AddWithValue("@userPass", pass);
                try
                {
                    conn.Open();
                    Object count = sCom.ExecuteScalar();
                    Int32 var;
                    if (Int32.TryParse(count.ToString(), out var))
                    {
                        if (var == 1)
                        {
                            valid = true;
                        }
                    }
                }
                catch (SqlException)
                {
                    Console.WriteLine("SQL ERROR");
                }
                finally
                {
                    conn.Close();
                }
                return valid;
            }

            public bool RegUser(UserRecord rec)
            {
                if (checkValid(rec.userEmail))
                {
                    String con = "Data Source = DESKTOP - 69RGCI5; Initial Catalog = RESQFOODS; Integrated Security = True";
                    String query = "INSERT INTO USERS(USER_EMAIL, USER_TYPE, USER_PASS, USER_REG, USER_ACTIVE) VALUES(@param1, @param2, @param3, @param4, @param5 )";
                    SqlConnection conn = new SqlConnection(con);
                    SqlCommand sCom = new SqlCommand(query, conn);
                    sCom.Parameters.AddWithValue("@param1", rec.userEmail);
                    sCom.Parameters.AddWithValue("@param2", rec.userType);
                    sCom.Parameters.AddWithValue("@param3", rec.userPass);
                    sCom.Parameters.AddWithValue("@param4", rec.userReg);
                    sCom.Parameters.AddWithValue("@param5", rec.userActive);
                    try
                    {
                        conn.Open();
                        int recordsAffected = sCom.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
                return false;
            }

            public List<UserRecord> SearchEmployeeRecord(UserRecord user)
            {
                throw new NotImplementedException();
            }

            public string UpdateEmployeeContact(UserRecord user)
            {
                throw new NotImplementedException();
            }
        }
    }

