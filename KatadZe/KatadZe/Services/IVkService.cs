using KatadZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatadZe.Services
{
    public interface IVkService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
