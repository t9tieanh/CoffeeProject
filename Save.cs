using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaffeeShop
{
    public interface Save
    {
        /// <summary>
        /// đẩy dữ liệu của đối tượng xuống database 
        /// </summary>
        void SaveData();
    }
}