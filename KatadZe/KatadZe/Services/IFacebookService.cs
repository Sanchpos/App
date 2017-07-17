using KatadZe.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatadZe.Services
{
    public interface IFacebookService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
