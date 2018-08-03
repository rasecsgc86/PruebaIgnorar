using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AM45Secure.Commons.Recursos;
using Zero.Exceptions;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public abstract class AbstractModel
    {
        public string Tkn { set; get; }

        public string GetNombreUsuarioSesion()
        {
            try
            {
                return GetDataSesion(ClaimTypes.UserData);
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_12, e);
            }
        }

        public string GetUsuarioSesion()
        {
            try
            {
                return GetDataSesion("unique_name");
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_13, e);
            }
        }

        public int GetIdUsuarioSesion()
        {
            try
            {
                return int.Parse(GetDataSesion("nameid"));
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_14, e);
            }
        }

        public int GetIdPerfilUsuarioSesion()
        {
            try
            {
                return int.Parse(GetDataSesion("role"));
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_15, e);
            }
        }

        private string GetDataSesion(string key)
        {
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();            
            JwtSecurityToken jwt = tokenHandler.ReadToken(Tkn) as JwtSecurityToken;
            return jwt.Claims.FirstOrDefault(x => x.Type == key)?.Value;
        }
    }
}
