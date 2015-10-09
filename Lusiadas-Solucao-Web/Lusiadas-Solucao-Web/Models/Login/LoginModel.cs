using LDF.Authentication;
using LusiadasDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LusiadasSolucaoWeb.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Utilizador")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get{ return _password;}  set { _password = GetMyPassword(value); } }

        [Required]
        [Display(Name = "Unidade")]
        public string LocalConnection { get; set; }


        private string _password;

        private string GetMyPassword(string str)
        {
            return str;
        }
        /// <summary>
        /// Check if user is valid and exists on sistem
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool IsValid()
        {
            bool autoLogin = (ConfigurationManager.AppSettings[Constants.GLB_Login_ByPass] == "1" ? true: false);
            bool isAuthed = false;
            LDFAuthentication auth = new LDFAuthentication(ConfigurationManager.AppSettings["LDAPServer"], ConfigurationManager.AppSettings["Domain"], "DBGeneral", true);

            if (autoLogin && _password == ConfigurationManager.AppSettings[Constants.GLB_Login_Key])
                isAuthed = auth.ValidateUser(ConfigurationManager.AppSettings[Constants.GLB_Login_ADUser], ConfigurationManager.AppSettings[Constants.GLB_Login_ADPass], UserName);
            else
                isAuthed = auth.ValidateUser(UserName, _password);

            if (isAuthed)
                HttpContext.Current.Session[Constants.SS_AUTH] = auth;

            return isAuthed;
        }

        public UserInfo GetUserInfo()
        {
            UserInfo                uinfo       = new UserInfo();
            DALSDServ               dal         = new DALSDServ();
            List<TblSDPessHosp>     listPess    = dal.GetInfoPessHosp(UserName);
            
            if (listPess != null && listPess.Count > 0)
            {
                TblSDPessHosp pess = listPess.First();

                uinfo.numMecan          = pess.N_MECAN;
                uinfo.nome              = pess.ABR;
                uinfo.titulo            = pess.TITULO;
                uinfo.userID            = UserName;
                uinfo.catProfissional   = pess.T_PESS_HOSP;
                //uinfo.cedProfissional   = "MED";
                foreach(TblSDPessHosp item in listPess.Where(q => q.COD_SERV != null).ToList())
                {
                    uinfo.listCodServ.Add(item.COD_SERV);
                }
            }

            return uinfo;
        }
    }


    public class UserInfo
    {
        public string numMecan { get; set; }
        public string titulo { get; set; }
        public string nome { get; set; }
        public string userID { get; set; }
        public List<string> listCodServ { get; set; }

        public string catProfissional { get;  set; }
        public string cedProfissional { get; set; }

		
		public string getcedProfissional()
		{
			if(this.catProfissional == "MED" || this.catProfissional == "ENF")
                return cedProfissional;
			else
				return "";
		}

        public string getcatProfissional()
		{
			switch (this.catProfissional)
			{
				case "MED": return "Médico/a"; 
				case "ADM": return "Administrativo/a"; 
				case "ENF": return "Enfermeiro/a"; 
				case "AUX": return "Auxiliar"; 
                default: return this.catProfissional;
			}
		}

        public UserInfo()
        {
            listCodServ = new List<string>();
        }
    }

}