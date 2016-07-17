using CNodeUwp.Common;
using CNodeUwp.Services.User.Version1;
using GalaSoft.MvvmLight;
using System.Threading;
using System.Threading.Tasks;

namespace CNodeUwp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _isLogin;
        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                Set(nameof(IsLogin), ref _isLogin, value);
            }
        }


        public LoginViewModel()
        {
            HasValidToken();
        }


        private async Task HasValidToken(CancellationToken cancellationToken = default(CancellationToken))
        {
            IsLogin = await UserService.ValidateTokenAsync(TokenHelper.GetToken(), cancellationToken);
        }
    }
}
