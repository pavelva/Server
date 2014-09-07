using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using website;
namespace webTest
{
    public partial class default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyServer.run();
            ThreadPool.QueueUserWorkItem(txt);
        }

        private void txt(Object o)
        {
            while (true)
                lbl.Text = MyServer.message;
        }
    }
}