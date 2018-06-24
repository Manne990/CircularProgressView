using CoreGraphics;
using UIKit;

namespace CircularProgressView.iOS.Code
{
    public class TestViewController : UIViewController
    {
        private CircleProgressView _progressView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            _progressView = new CircleProgressView(UIColor.Gray, UIColor.Blue)
            {
                Max = 100,
                Progress = 25,
                Title = "Hello!",
                SubTitle = "25%"
            };

            View.AddSubview(_progressView);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _progressView.Frame = new CGRect(100, 100, 100, 100);
        }
    }
}