using System.Drawing;
using System.Windows.Forms;

namespace AutoTest
{
    class HtmlToBitmapConverter
    {
        private const int SleepTimeMiliseconds = 5000;

        //Used to start a new thread to render an image.
        //Needed because the webBrowser does not like to be created in a thread with mta(multi-thread apartment)
        public static Image start(string html, Size size)
        {
            return new HtmlToBitmapConverter().Render(html, size);
        }

        //Renders html into an image
        public Bitmap Render(string html, Size size)
        {
            var browser = CreateBrowser(size);

            browser.Navigate("about:blank");
            browser.Document.Write(html);

            return GetBitmapFromControl(browser, size);
        }

        //Created the browser that will provide a bitmap
        private WebBrowser CreateBrowser(Size size)
        {
            return
                new WebBrowser
                {
                    ScrollBarsEnabled = false,
                    ScriptErrorsSuppressed = true,
                    Size = size
                };
        }

        //Gets the bitmap from the WebBrowser control
        private Bitmap GetBitmapFromControl(WebBrowser browser, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);

            NativeMethods.GetImage(browser.Document.DomDocument, bitmap, Color.White);
            return bitmap;
        }
    }
}
