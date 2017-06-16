using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace AssistantApp
{
    public class ContinuationManager
    {
        internal void Continue(IContinuationActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                var page = MainPage.Current as IWebAuthenticationContinuable;
                if (page != null)
                {
                    page.ContinueWebAuthentication(
                        args as WebAuthenticationBrokerContinuationEventArgs);
                }
            }
        }
    }
    interface IWebAuthenticationContinuable
    {
        /// <summary>
        /// This method is invoked when the web authentication broker returns with the authentication result.
        /// </summary>
        /// <param name="args">Activated event args object that contains returned authentication token.</param>
        void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args);
    }
}
