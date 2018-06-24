using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace CircularProgressView.iOS.Code
{
    public class CircleProgressView : UIView
    {
        private const float RingWidth = 5f;
        private const float RingPadding = 5f;

        private readonly CAShapeLayer _backgroundRingLayer;
        private readonly CAShapeLayer _ringLayer;
        private readonly UIView _container;
        private readonly UILabel _titleLabel;
        private readonly UILabel _subTitleLabel;

        public CircleProgressView(UIColor backgroundColor, UIColor foregroundColor)
        {
            _backgroundRingLayer = new CAShapeLayer
            {
                StrokeColor = backgroundColor.CGColor,
                FillColor = UIColor.Clear.CGColor,
                LineWidth = RingWidth
            };

            Layer.AddSublayer(_backgroundRingLayer);

            _ringLayer = new CAShapeLayer
            {
                StrokeColor = foregroundColor.CGColor,
                FillColor = UIColor.Clear.CGColor,
                LineWidth = RingWidth
            };

            Layer.AddSublayer(_ringLayer);

            _container = new UIView();
            _titleLabel = CreateLabel(string.Empty, UITextAlignment.Center, true);
            _subTitleLabel = CreateLabel(string.Empty, UITextAlignment.Center);

            AddSubview(_container);
            _container.AddSubviews(_titleLabel, _subTitleLabel);
        }

        private nfloat _max;
        public nfloat Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                SetNeedsLayout();
            }
        }

        private nfloat _progress;
        public nfloat Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                SetNeedsLayout();
            }
        }

        public string Title
        {
            get
            {
                return _titleLabel.Text;
            }
            set
            {
                _titleLabel.Text = value;
            }
        }

        public string SubTitle
        {
            get
            {
                return _subTitleLabel.Text;
            }
            set
            {
                _subTitleLabel.Text = value;
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _titleLabel.SizeToFit();
            _subTitleLabel.SizeToFit();

            _container.Frame = new CGRect(0f, (Bounds.Height - 50f) / 2f, Bounds.Width, _titleLabel.Frame.Height + _subTitleLabel.Frame.Height);
            _titleLabel.Frame = new CGRect(5f, 5f, _container.Frame.Width - 10f, _titleLabel.Frame.Height);
            _subTitleLabel.Frame = new CGRect(5f, _titleLabel.Frame.Bottom, _container.Frame.Width - 10f, _subTitleLabel.Frame.Height);

            var padding = new UIEdgeInsets(RingPadding, RingPadding, RingPadding, RingPadding);

            _backgroundRingLayer.Path = UIBezierPath.FromOval(padding.InsetRect(Bounds)).CGPath;

            if (Progress > 0 && Max > 0)
            {
                _ringLayer.Path = UIBezierPath.FromArc(new CGPoint(Bounds.Size.Width / 2f, 
                                                                   Bounds.Size.Height / 2f), Bounds.Height / 2f - RingWidth - RingPadding + RingWidth,
                                                       (nfloat)(-Math.PI / 2f), 
                                                       (nfloat)((Math.PI * 2f * Progress / Max) - (Math.PI / 2f)),true).CGPath;
            }
            else
            {
                _ringLayer.Path = null;
            }
        }

        public UILabel CreateLabel(string title = "", UITextAlignment textAlignment = UITextAlignment.Left, bool multiLine = false)
        {
            return new UILabel
            {
                Text = title,
                TextColor = UIColor.Black,
                TextAlignment = textAlignment,
                Lines = multiLine ? 0 : 1
            };
        }
    }
}