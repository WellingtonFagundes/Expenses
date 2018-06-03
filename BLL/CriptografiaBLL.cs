using MLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CriptografiaBLL
    {
        public CriptografiaBLL()
        {

        }


        public string CriptografaID(int id)
        {
            MD5Crypt cript = new MD5Crypt();


            try
            {

                return cript.Criptografar(id.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public int DescriptografaID(string id)
        {
            MD5Crypt cript = new MD5Crypt();

            try
            {
                return Int32.Parse(cript.Descriptografar(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
