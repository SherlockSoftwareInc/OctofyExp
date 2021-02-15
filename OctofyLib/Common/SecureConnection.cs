using System;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class SecureConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public SecureConnection()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securedConnectionString"></param>
        public SecureConnection(string securedConnectionString)
        {
            SecureConnectionString = securedConnectionString;
        }

        private string _serverName = "";

        /// <summary>
        /// 
        /// </summary>
        public string ServerName
        {
            get
            {
                return _serverName;
            }

            set
            {
                _serverName = value.Trim();
            }
        }

        private string _database = "";

        /// <summary>
        /// 
        /// </summary>
        public string Database
        {
            get
            {
                return _database;
            }

            set
            {
                _database = value.Trim();
            }
        }

        private string _userName = "";

        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value.Trim();
            }
        }

        private string _password = "";

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IntegratedSecurity { get; set; }

        public override string ToString()
        {
            if (Database.Length > 0)
            {
                return Database;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return BuildConnectionString();
            }

            set
            {
                if (value != null)
                {
                    Parse(value);
                }
                else
                {
                    Parse("");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SecureConnectionString
        {
            get
            {
                return BuildSecureConnectionString();
            }

            set
            {
                if (value != null)
                {
                    ParseSecureConnectionString(value);
                }
                else
                {
                    Clear();
                }
            }
        }

        private string BuildConnectionString()
        {
            string connString = "";
            if (ServerName.Length > 0 & Database.Length > 0)
            {
                if (IntegratedSecurity)
                {
                    connString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", ServerName, Database);
                }
                else
                {
                    connString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", ServerName, Database, UserName, Password);
                }
            }

            return connString;
        }

        private void Parse(string connString)
        {
            Clear();
            if (connString.Length > 0)
            {
                var sections = connString.Split(';');
                foreach (string item in sections)
                {
                    string name = "";
                    string value = "";
                    GetNameAndValue(item, ref name, ref value);
                    if (name.Length > 0)
                    {
                        switch (name.ToLower())
                        {
                            case "data source":
                                {
                                    ServerName = value;
                                    break;
                                }

                            case "initial catalog":
                                {
                                    Database = value;
                                    break;
                                }

                            case "user id":
                                {
                                    UserName = value;
                                    break;
                                }

                            case "password":
                                {
                                    Password = value;
                                    break;
                                }

                            case "integrated security":
                                {
                                    if (string.Compare(value, "True", true) == 0)
                                    {
                                        IntegratedSecurity = true;
                                    }

                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void GetNameAndValue(string valueString, ref string name, ref string value)
        {
            name = "";
            value = "";
            if (valueString.IndexOf("=") > 0)
            {
                var values = valueString.Split('=');
                name = values[0].Trim();
                value = values[1].Trim();
            }
        }

        private void Clear()
        {
            ServerName = "";
            Database = "";
            UserName = "";
            Password = "";
            IntegratedSecurity = false;
        }

        private string BuildSecureConnectionString()
        {
            try
            {
                var wrapper = new Encryption("gH1zdmxu73bSF_Q%:>4FWHHotpJ<%QUfEd>E6fK%0i_sIrEi2[wMH1GUJLU@iEdo");
                string cipherText = wrapper.EncryptData(BuildConnectionString());
                return cipherText;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void ParseSecureConnectionString(string connString)
        {
            try
            {
                var wrapper = new Encryption("gH1zdmxu73bSF_Q%:>4FWHHotpJ<%QUfEd>E6fK%0i_sIrEi2[wMH1GUJLU@iEdo");
                Parse(wrapper.DecryptData(connString));
            }
            catch (Exception)
            {
            }
        }

    }
}
