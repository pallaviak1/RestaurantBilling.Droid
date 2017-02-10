using CoreLib.Models;
using MvvmCross.Plugins.Messenger;

namespace CoreLib.Message
{
    public class MyAlertMessage : MvxMessage
    {
        public MyAlertMessage(object sender) : base(sender)
        {
            MyMessage = "Welcome to the page!";
        }

        public string MyMessage { get; set; }
    }
}
