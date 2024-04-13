using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using Provider;
using Provider.Handlers;
using SmartHouse_Control_Web.Repositories.Partial_Repositories;

namespace SmartHouse_Control_Web.Repositories
{
    public sealed class WebSiteRepository : IPersonality
    {
        #region IPersonality

        #region Authentication

        public CurrentProvider Authentication
        (
            string login,
            string password
        )
        {
            CurrentProvider provider = new CurrentProvider();
            Tuple<object, bool, CurrentProvider> isExist = provider.CheckAccess
                (
                 new AccessHandler
                     (
                      login,
                      password
                     )
                );
            return isExist.Item2
                       ? isExist.Item3
                       : null;
        }

        #endregion

        #endregion
    }
}